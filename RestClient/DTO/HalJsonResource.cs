***REMOVED***

namespace RestClient.DTO
***REMOVED***
***REMOVED***
    /// Base class for Dto objects - deserializable from HAL+JSON format
***REMOVED***
    public class HalJsonResource
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the <see cref="HalJsonResource"/> class
    ***REMOVED***
        public HalJsonResource()
        ***REMOVED***
            Type t = this.GetType();
            if (t.IsDefined(typeof(PluralNameAttribute), false) == false)
            ***REMOVED***
                throw new InvalidOperationException("Missing Plural name attribute on HalJsonResource class!");
    ***REMOVED***
***REMOVED***
***REMOVED***

***REMOVED***
    /// Attribute for indicating object list name
***REMOVED***
    [AttributeUsage(AttributeTargets.Class)]
    public class PluralNameAttribute : Attribute
    ***REMOVED***
    ***REMOVED***
        /// Gets the plural name
    ***REMOVED***
        public readonly string PluralName;

    ***REMOVED***
        /// Initializes a new instance of the <see cref="PluralNameAttribute"/> class
    ***REMOVED***
        /// <param name="pluralName">Plural name to be set</param>
        public PluralNameAttribute(string pluralName)
        ***REMOVED***
            this.PluralName = pluralName;
***REMOVED***
***REMOVED***
***REMOVED***
