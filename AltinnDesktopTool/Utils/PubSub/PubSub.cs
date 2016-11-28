***REMOVED***

namespace AltinnDesktopTool.Utils.PubSub
***REMOVED***
***REMOVED***
    /// Defines the handler
***REMOVED***
    /// <typeparam name="T">The View Type</typeparam>
    /// <param name="sender">The Sender</param>
    /// <param name="e">The Arguments</param>
    public delegate void PubSubEventHandler<T>(object sender, PubSubEventArgs<T> e);

***REMOVED***
    /// Class to register and raise events between views
***REMOVED***
    /// <typeparam name="T">The View Type</typeparam>
    public static class PubSub<T>
    ***REMOVED***
        private static Dictionary<string, PubSubEventHandler<T>> events =
                new Dictionary<string, PubSubEventHandler<T>>();

    ***REMOVED***
        /// Adds a new event
    ***REMOVED***
        /// <param name="name">The event name</param>
        /// <param name="handler">The event handler</param>
        public static void AddEvent(string name, PubSubEventHandler<T> handler)
        ***REMOVED***
            if (!events.ContainsKey(name))
            ***REMOVED***
                events.Add(name, handler);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Raises a new event
    ***REMOVED***
        /// <param name="name">The name of the event</param>
        /// <param name="sender">The object sender of the event</param>
        /// <param name="args">The arguments</param>
        public static void RaiseEvent(string name, object sender, PubSubEventArgs<T> args)
        ***REMOVED***
            if (events.ContainsKey(name) && events[name] != null)
            ***REMOVED***
                events[name](sender, args);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Registers an event
    ***REMOVED***
        /// <param name="name">The event name</param>
        /// <param name="handler">The event handler</param>
        public static void RegisterEvent(string name, PubSubEventHandler<T> handler)
        ***REMOVED***
            if (events.ContainsKey(name))
            ***REMOVED***
                events[name] += handler;
    ***REMOVED***
            else
            ***REMOVED***
                events.Add(name, handler);
    ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Clears the registered events
    ***REMOVED***
        public static void ClearEvents()
        ***REMOVED***
            events = new Dictionary<string, PubSubEventHandler<T>>();
***REMOVED***
***REMOVED***
***REMOVED***
