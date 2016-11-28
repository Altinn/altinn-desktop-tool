using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

***REMOVED***

using RestClient.Resources;

namespace RestClient.Test
***REMOVED***
***REMOVED***
    /// Test class for unit tests of the RestClientException class.
***REMOVED***
***REMOVED***
    public class RestClientExceptionTest
    ***REMOVED***
    ***REMOVED***
        /// Gets or sets the test context for the current test.
    ***REMOVED***
        public TestContext TestContext ***REMOVED*** get; set; ***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Instantiate a new instance of the RestClientException class with a message.
***REMOVED***
        ///   A new RestClientException instance is created.
***REMOVED***
        ///   The Message property of the RestClientException is correctly set.
    ***REMOVED***
***REMOVED***
***REMOVED***
        public void RestClientExceptionTest_InstantiationWithMessage_MessageCorrect()
        ***REMOVED***
***REMOVED***
            const string ExpectedMessage = "Message";

***REMOVED***
            RestClientException target = new RestClientException(ExpectedMessage);

***REMOVED***
            Assert.AreEqual(ExpectedMessage, target.Message);
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Instantiate a new instance of the RestClientException class with a message and error code.
***REMOVED***
        ///   A new RestClientException instance is created.
***REMOVED***
        ///   The Message and the ErrorCode properties of the RestClientException is correctly set.
    ***REMOVED***
***REMOVED***
***REMOVED***
        public void RestClientExceptionTest_InstantiationWithMessageAndErrorCode_MessageAndErrorCodeCorrect()
        ***REMOVED***
***REMOVED***
            const string ExpectedMessage = "Message";
            const string ExpectedErrorCode = "ErrorCode";

***REMOVED***
            RestClientException target = new RestClientException(ExpectedMessage, ExpectedErrorCode);

***REMOVED***
            Assert.AreEqual(ExpectedMessage, target.Message);
            Assert.AreEqual(ExpectedErrorCode, target.ErrorCode);
***REMOVED***

    ***REMOVED***
***REMOVED***
        ///   Serialize and deserialize an instance of the RestClientException class.
***REMOVED***
        ///   The RestClientException instance is serialized and deserialized successfully.
***REMOVED***
        ///   The new instance of the RestClientException class is identical to the original before serialization and deserialization.
    ***REMOVED***
***REMOVED***
***REMOVED***
        public void RestClientExceptionTest_SerializationDeserialization_InputEqualOutput()
        ***REMOVED***
***REMOVED***
            IOException innerException = new IOException("IOException");
            RestClientException expected = new RestClientException("Message", "ErrorCode", innerException);

***REMOVED***
            RestClientException actual = Deserialize<RestClientException>(Serialize(expected));

***REMOVED***
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ErrorCode, actual.ErrorCode);

            Assert.IsNotNull(expected.InnerException);
            Assert.IsNotNull(actual.InnerException);
            Assert.AreEqual(expected.InnerException.Message, actual.InnerException.Message);
***REMOVED***

        #region Private helper methods

        private static Stream Serialize(object source)
        ***REMOVED***
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            return stream;
***REMOVED***

        private static T Deserialize<T>(Stream stream)
        ***REMOVED***
            IFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
***REMOVED***

        #endregion
***REMOVED***
***REMOVED***
