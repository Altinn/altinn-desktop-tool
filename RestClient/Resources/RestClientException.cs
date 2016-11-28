using System;
using System.Runtime.Serialization;

namespace RestClient.Resources
{
    /// <summary>
    /// The exception that is thrown by the RestClient when there is any technical issues.
    /// </summary>
    [Serializable]
    public class RestClientException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RestClientException class with its message set to empty string, its HRESULT set to COR_E_IO, 
        /// and inner exception set to a null reference.
        /// </summary>
        public RestClientException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RestClientException class with its message string set to message, its HRESULT set to COR_E_IO,
        /// and its inner exception set to null.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public RestClientException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RestClientException class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public RestClientException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RestClientException class with a specified error message and error code. The inner exception is
        /// set to null reference.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code indicating a more detailed cause of the exception.</param>
        public RestClientException(string message, string errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the RestClientException class with a specified error message, error code and a reference to the
        /// inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code indicating a more detailed cause of the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public RestClientException(string message, string errorCode, Exception inner) : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the RestClientException class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected RestClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.ErrorCode = info.GetString("ErrorCode");
        }

        /// <summary>
        /// Gets an error code indicating a more detailed cause of the exception.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination <see cref="StreamingContext"/> for this serialization.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("ErrorCode", this.ErrorCode);

            base.GetObjectData(info, context);
        }
    }
}
