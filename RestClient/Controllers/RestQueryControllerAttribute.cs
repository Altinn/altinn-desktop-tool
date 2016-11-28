***REMOVED***

namespace RestClient.Controllers
***REMOVED***
***REMOVED***
    /// Each class implementing IRestQueryController must have this attribute to identify it as a RestQueryController and define its name.
    /// The name will be used as part of the URL to identify the controller.
***REMOVED***
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RestQueryControllerAttribute : Attribute
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the RestQueryControllerAttribute class with name and type set to null.
    ***REMOVED***
        public RestQueryControllerAttribute()
        ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Initializes a new instance of the RestQueryControllerAttribute class with a given name and supported type.
    ***REMOVED***
        /// <param name="name">The name of the controller.</param>
        /// <param name="supportedType">The type supported by the controller.</param>
        public RestQueryControllerAttribute(string name, Type supportedType)
        ***REMOVED***
            this.Name = name;
            this.SupportedType = supportedType;
***REMOVED***

    ***REMOVED***
        /// Gets or sets the name identifying the controller
    ***REMOVED***
        public string Name ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the supported type for a controller.
    ***REMOVED***
        public Type SupportedType ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
        /// Gets or sets the controller type.
    ***REMOVED***
        internal Type ControllerType ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***
