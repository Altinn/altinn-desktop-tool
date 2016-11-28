***REMOVED***

namespace AltinnDesktopTool.Utils.PubSub
***REMOVED***
***REMOVED***
    /// Defines the PubSub event arguments
***REMOVED***
    /// <typeparam name="T">The event args contains an Item of this type</typeparam>
    public class PubSubEventArgs<T> : EventArgs
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the <see cref="PubSubEventArgs***REMOVED***T***REMOVED***"/> class. 
    ***REMOVED***
        /// <param name="item">
        /// Mandatory item of the defined type for the PubSubEventArgs
        /// </param>
        public PubSubEventArgs(T item)
        ***REMOVED***
            this.Item = item;
***REMOVED***

    ***REMOVED***
        /// Gets or sets the argument item
    ***REMOVED***
        public T Item ***REMOVED*** get; set; ***REMOVED***
***REMOVED***
***REMOVED***