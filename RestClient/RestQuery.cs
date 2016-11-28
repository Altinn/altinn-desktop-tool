using System;
using System.Collections.Generic;
using System.Reflection;

using log4net;

using RestClient.Controllers;
using RestClient.DTO;
using RestClient.Resources;

namespace RestClient
{
    /// <summary>
    /// The Rest Query class implements IRestQuery. 
    /// </summary>
    /// <remarks>
    /// It is a generic implementation passing queries to IRestQueryControllers to perform query by interpreting 
    /// the url and pass this to the server using the AltinnRestClient.
    /// The Controllers are identified by the  [RestQueryController] attribute and must implement IRestQueryController.
    /// Exception Handling:
    /// RestQuery will catch any Exception from Controller, log them and rethrow.
    /// All Exceptions thrown by RestQuery are logged, meaning the caller does not need to log exceptions from RestQuery.
    /// The Controller may log exceptions and errors with caution as RestQuery will log any Exception thrown by the Controller.
    /// </remarks>
    public class RestQuery : IRestQuery
    {
        #region private declarations

        private const string AuthenticateUri = "organizations?ForceEIAuthentication";
        private const string ControllerExceptionText = "The controller threw an Exception";
        private const string ControllerNotFoundForTypeException = "No Controller for type {0}";
        private const string ControllerNotFoundForUrl = "No Controller for url {0}";

        private readonly ILog log;
        private readonly AltinnRestClient restClient;
        private readonly IRestQueryConfig restQueryConfig;
        private readonly List<RestQueryControllerAttribute> controllers = new List<RestQueryControllerAttribute>();

        private bool isAuthenticated;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the RestQuery class by injecting the configuration and log.
        /// </summary>
        /// <param name="restQueryConfig">The configuration needed for connecting</param>
        /// <param name="log">Optional log4net log instance</param>
        /// <remarks>
        /// The configuration is mandatory, whereas the log is not not mandatory.
        /// </remarks>
        public RestQuery(IRestQueryConfig restQueryConfig, ILog log = null)
        {
            this.restQueryConfig = restQueryConfig;
            this.log = log;
            this.restClient = new AltinnRestClient
            {
                BaseAddress = restQueryConfig.BaseAddress,
                ApiKey = restQueryConfig.ApiKey,
                IgnoreSslErrors = restQueryConfig.IgnoreSslErrors,
                Thumbprint = restQueryConfig.ThumbPrint,
                Timeout = restQueryConfig.Timeout
            };

            this.InitControllers();
        }

        #endregion

        #region IRestQuery implementation

        /// <summary>
        /// Fetches an object by providing and id.
        /// </summary>
        /// <typeparam name="T">The object type being retrieved.</typeparam>
        /// <param name="id">The id of the resource to retrieve.</param>
        /// <exception cref="RestClientException">
        /// Any Exception from the controller is logged and thrown as RestClientException with InnerException being the caught Exception.
        /// A RestClientException is also thrown when the controller could not be found supporting type T.
        /// </exception>
        /// <returns>An object of the type of resource that was requested.</returns>
        public T Get<T>(string id) where T : HalJsonResource
        {
            this.EnsureAuthenticated();

            IRestQueryController controller = this.GetControllerByType(typeof(T));

            if (controller == null)
            {
                string err = string.Format(ControllerNotFoundForTypeException, typeof(T));
                this.log.Error(err, null);

                throw new RestClientException(err);
            }

            try
            {
                return controller.Get<T>(id);
            }
            catch (Exception ex)
            {
                this.log.Error(ControllerExceptionText, ex);

                if (ex is RestClientException)
                {
                    throw;
                }

                throw new RestClientException(ControllerExceptionText, ex);
            }
        }

        /// <summary>
        /// Search for a list of objects by filtering on a given name value pair.
        /// </summary>
        /// <typeparam name="T">The object type being retrieved.</typeparam>
        /// <param name="filter">The name and the value of the search parameter to be used in the search.</param>
        /// <exception cref="RestClientException">
        /// Any Exception from the controller is logged and thrown as RestClientException with InnerException being the caught Exception.
        /// A RestClientException is also thrown when the controller could not be found supporting type T.
        /// </exception>
        /// <returns>A list of the identified elements matching the search criteria.</returns>
        public IList<T> Get<T>(KeyValuePair<string, string> filter) where T : HalJsonResource
        {
            this.EnsureAuthenticated();

            IRestQueryController controller = this.GetControllerByType(typeof(T));

            if (controller == null)
            {
                string err = string.Format(ControllerNotFoundForTypeException, typeof(T));
                this.log.Error(err, null);

                throw new RestClientException(err);
            }

            try
            {
                return controller.Get<T>(filter);
            }
            catch (Exception ex)
            {
                this.log.Error(ControllerExceptionText, ex);

                if (ex is RestClientException)
                {
                    throw;
                }

                throw new RestClientException(ControllerExceptionText, ex);
            }
        }

        /// <summary>
        /// Fetches a list of objects by a given link (url).
        /// </summary>
        /// <typeparam name="T">The type that is expected to be found at the url.</typeparam>
        /// <param name="url">The url to the specific resource to get.</param>
        /// <exception cref="RestClientException">
        /// Any Exception from the controller is logged and thrown as RestClientException with InnerException being the caught Exception.
        /// A RestClientException is also thrown when the controller could not be found supporting type T.
        /// </exception>
        /// <returns>A list of elements of the specified type.</returns>
        public IList<T> GetByLink<T>(string url) where T : HalJsonResource
        {
            this.EnsureAuthenticated();

            IRestQueryController controller = this.GetControllerByUrl(url);

            if (controller == null)
            {
                string err = string.Format(ControllerNotFoundForUrl, url);
                this.log.Error(err, null);

                throw new RestClientException(err);
            }

            try
            {
                return controller.GetByLink<T>(url);
            }
            catch (Exception ex)
            {
                this.log.Error(ControllerExceptionText, ex);

                if (ex is RestClientException)
                {
                    throw;
                }

                throw new RestClientException(ControllerExceptionText, ex);
            }
        }

        #endregion

        #region private implementation

        /// <summary>
        /// Ensures that this client is authenticated at the server side.
        /// </summary>
        /// <remarks>
        /// This extra call is preformed to avoid an issue with an <code>iRule</code> in BigIP. The current version of the rule can prevent
        /// the enterprise certificate from reaching the REST API authorization logic and result in status code 401.
        /// </remarks>
        private void EnsureAuthenticated()
        {
            if (this.isAuthenticated)
            {
                return;
            }

            try
            {
                this.restClient.Get(AuthenticateUri);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }

            this.isAuthenticated = true;
        }

        /// <summary>
        /// Generates a controller context to be passed to the controller.
        /// </summary>
        /// <param name="attr">The controller's class attribute.</param>
        /// <returns>The ControllerContext to be passed to the controller.</returns>
        private ControllerContext GetControllerContext(RestQueryControllerAttribute attr)
        {
            return new ControllerContext()
            {
                Log = this.log,
                RestClient = this.restClient,
                ControllerBaseAddress = $"{this.restQueryConfig.BaseAddress}{attr.Name}"
            };
        }

        /// <summary>
        /// Fetches the controller which supports Type t, meaning the controller which is registered with [RestQueryController(SupportedType = t)]
        /// </summary>
        /// <param name="t">The type matching SupportedType</param>
        /// <returns>The found query controller or null if not found</returns>
        private IRestQueryController GetControllerByType(Type t)
        {
            IRestQueryController controller = null;
            foreach (RestQueryControllerAttribute item in this.controllers)
            {
                if (item.SupportedType == t)
                {
                    controller = (IRestQueryController)Activator.CreateInstance(item.ControllerType);
                    controller.Context = this.GetControllerContext(item);
                    break;
                }
            }

            return controller;
        }

        /// <summary>
        /// Fetches the controller by its URL and object type. The url controller part contains the name. 
        /// The controller part is the first word after the base address.
        /// It takes into account that the url may contain the base address.
        /// </summary>
        /// <param name="url">Either a full address including base address or a url part starting with controller name.</param>
        /// <returns>The controller, initiated with context</returns>
        private IRestQueryController GetControllerByUrl(string url)
        {
            IRestQueryController controller = null;
            string u = url;

            if (u.StartsWith(this.restQueryConfig.BaseAddress, StringComparison.InvariantCultureIgnoreCase))
            {
                u = u.Substring(this.restQueryConfig.BaseAddress.Length);
            }

            // It should start with controller name, but if it wrongly starts with / that / is removed
            if (u.StartsWith("/") && u.Length > 1)
            {
                u = u.Substring(1);
            }

            string name = u;
            int index = u.IndexOfAny(new[] { '/', '?', '$' });
            if (index > 0)
            {
                name = u.Substring(0, index);
            }

            foreach (RestQueryControllerAttribute item in this.controllers)
            {
                if (!item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                controller = (IRestQueryController)Activator.CreateInstance(item.ControllerType);
                controller.Context = this.GetControllerContext(item);
                break;
            }

            return controller;
        }

        /// <summary>
        /// Initiate the controller list. Use reflection to capture controllers and add them to the dictionary.
        /// A Controller must have the class attribute RestQueryController.
        /// </summary>
        private void InitControllers()
        {
            Assembly[] assarr = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly ass in assarr)
            {
                try
                {
                    Type[] typearr = ass.GetTypes();
                    foreach (Type type in typearr)
                    {
                        if (!type.IsClass)
                        {
                            continue;
                        }

                        IEnumerable<Attribute> attrArr = type.GetCustomAttributes();

                        if (attrArr == null)
                        {
                            continue;
                        }

                        foreach (Attribute attr in attrArr)
                        {
                            RestQueryControllerAttribute item = attr as RestQueryControllerAttribute;

                            if (item == null)
                            {
                                continue;
                            }

                            item.ControllerType = type;
                            this.controllers.Add(item);
                        }
                    }
                }
                catch
                {
                    // In some situation an exception is raised which is not harmfull.
                    this.log.Warn("Error while browsing assemblies for controllers (harmless)");
                }
            }
        }

        #endregion
    }
}
