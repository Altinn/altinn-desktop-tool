using System;

namespace RestClient.DTO
{
    /// <summary>
    /// Base class for Dto objects - deserializable from HAL+JSON format
    /// </summary>
    public class HalJsonResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HalJsonResource"/> class
        /// </summary>
        public HalJsonResource()
        {
            Type t = this.GetType();
            if (t.IsDefined(typeof(PluralNameAttribute), false) == false)
            {
                throw new InvalidOperationException("Missing Plural name attribute on HalJsonResource class!");
            }
        }
    }

    /// <summary>
    /// Attribute for indicating object list name
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PluralNameAttribute : Attribute
    {
        /// <summary>
        /// Gets the plural name
        /// </summary>
        public readonly string PluralName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluralNameAttribute"/> class
        /// </summary>
        /// <param name="pluralName">Plural name to be set</param>
        public PluralNameAttribute(string pluralName)
        {
            this.PluralName = pluralName;
        }
    }
}
