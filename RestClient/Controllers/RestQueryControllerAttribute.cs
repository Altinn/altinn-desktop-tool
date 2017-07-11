using System;

namespace RestClient.Controllers
{
    /// <summary>
    /// Each class implementing IRestQueryController must have this attribute to identify it as a RestQueryController and define its name.
    /// The name will be used as part of the URL to identify the controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RestQueryControllerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the RestQueryControllerAttribute class with name and type set to null.
        /// </summary>
        public RestQueryControllerAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RestQueryControllerAttribute class with a given name and supported type.
        /// </summary>
        /// <param name="name">The name of the controller.</param>
        /// <param name="supportedType">The type supported by the controller.</param>
        public RestQueryControllerAttribute(string name, Type supportedType)
        {
            this.Name = name;
            this.SupportedType = supportedType;
        }

        /// <summary>
        /// Gets or sets the name identifying the controller
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the supported type for a controller.
        /// </summary>
        public Type SupportedType { get; set; }

        /// <summary>
        /// Gets or sets the controller type.
        /// </summary>
        internal Type ControllerType { get; set; }
    }
}
