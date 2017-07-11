using System.Collections.Generic;

namespace AltinnDesktopTool.Utils.PubSub
{
    /// <summary>
    /// Defines the handler
    /// </summary>
    /// <typeparam name="T">The View Type</typeparam>
    /// <param name="sender">The Sender</param>
    /// <param name="e">The Arguments</param>
    public delegate void PubSubEventHandler<T>(object sender, PubSubEventArgs<T> e);

    /// <summary>
    /// Class to register and raise events between views
    /// </summary>
    /// <typeparam name="T">The View Type</typeparam>
    public static class PubSub<T>
    {
        private static Dictionary<string, PubSubEventHandler<T>> events =
                new Dictionary<string, PubSubEventHandler<T>>();

        /// <summary>
        /// Adds a new event
        /// </summary>
        /// <param name="name">The event name</param>
        /// <param name="handler">The event handler</param>
        public static void AddEvent(string name, PubSubEventHandler<T> handler)
        {
            if (!events.ContainsKey(name))
            {
                events.Add(name, handler);
            }
        }

        /// <summary>
        /// Raises a new event
        /// </summary>
        /// <param name="name">The name of the event</param>
        /// <param name="sender">The object sender of the event</param>
        /// <param name="args">The arguments</param>
        public static void RaiseEvent(string name, object sender, PubSubEventArgs<T> args)
        {
            if (events.ContainsKey(name) && events[name] != null)
            {
                events[name](sender, args);
            }
        }

        /// <summary>
        /// Registers an event
        /// </summary>
        /// <param name="name">The event name</param>
        /// <param name="handler">The event handler</param>
        public static void RegisterEvent(string name, PubSubEventHandler<T> handler)
        {
            if (events.ContainsKey(name))
            {
                events[name] += handler;
            }
            else
            {
                events.Add(name, handler);
            }
        }

        /// <summary>
        /// Clears the registered events
        /// </summary>
        public static void ClearEvents()
        {
            events = new Dictionary<string, PubSubEventHandler<T>>();
        }
    }
}
