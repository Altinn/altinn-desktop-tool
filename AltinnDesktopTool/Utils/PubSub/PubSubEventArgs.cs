using System;

namespace AltinnDesktopTool.Utils.PubSub
{
    /// <summary>
    /// Defines the PubSub event arguments
    /// </summary>
    /// <typeparam name="T">The event args contains an Item of this type</typeparam>
    public class PubSubEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PubSubEventArgs{T}"/> class. 
        /// </summary>
        /// <param name="item">
        /// Mandatory item of the defined type for the PubSubEventArgs
        /// </param>
        public PubSubEventArgs(T item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Gets or sets the argument item
        /// </summary>
        public T Item { get; set; }
    }
}