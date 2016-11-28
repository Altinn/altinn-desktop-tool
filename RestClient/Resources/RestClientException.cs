***REMOVED***
using System.Runtime.Serialization;

namespace RestClient.Resources
***REMOVED***
***REMOVED***
    /// The exception that is thrown by the RestClient when there is any technical issues.
***REMOVED***
    [Serializable]
    public class RestClientException : Exception
    ***REMOVED***
    ***REMOVED***
        /// Initializes a new instance of the RestClientException class with its message set to empty string, its HRESULT set to COR_E_IO, 
        /// and inner exception set to a null reference.
    ***REMOVED***
        public RestClientException()
        ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Initializes a new instance of the RestClientException class with its message string set to message, its HRESULT set to COR_E_IO,
        /// and its inner exception set to null.
    ***REMOVED***
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public RestClientException(string message) : base(message)
        ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Initializes a new instance of the RestClientException class with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
    ***REMOVED***
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public RestClientException(string message, Exception inner) : base(message, inner)
        ***REMOVED***
***REMOVED***

    ***REMOVED***
        /// Initializes a new instance of the RestClientException class with a specified error message and error code. The inner exception is
        /// set to null reference.
    ***REMOVED***
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code indicating a more detailed cause of the exception.</param>
        public RestClientException(string message, string errorCode) : base(message)
        ***REMOVED***
            this.ErrorCode = errorCode;
***REMOVED***

    ***REMOVED***
        /// Initializes a new instance of the RestClientException class with a specified error message, error code and a reference to the
        /// inner exception that is the cause of this exception.
    ***REMOVED***
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code indicating a more detailed cause of the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public RestClientException(string message, string errorCode, Exception inner) : base(message, inner)
        ***REMOVED***
            this.ErrorCode = errorCode;
***REMOVED***

    ***REMOVED***
        /// Initializes a new instance of the RestClientException class with serialized data.
    ***REMOVED***
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected RestClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        ***REMOVED***
            this.ErrorCode = info.GetString("ErrorCode");
***REMOVED***

    ***REMOVED***
        /// Gets an error code indicating a more detailed cause of the exception.
    ***REMOVED***
        public string ErrorCode ***REMOVED*** get; ***REMOVED***

    ***REMOVED***
        /// Populates a SerializationInfo with the data needed to serialize the target object.
    ***REMOVED***
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination <see cref="StreamingContext"/> for this serialization.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        ***REMOVED***
            if (info == null)
            ***REMOVED***
                throw new ArgumentNullException(nameof(info));
    ***REMOVED***

            info.AddValue("ErrorCode", this.ErrorCode);

            base.GetObjectData(info, context);
***REMOVED***
***REMOVED***
***REMOVED***
