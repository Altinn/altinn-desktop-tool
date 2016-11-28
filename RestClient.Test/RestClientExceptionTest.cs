using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RestClient.Resources;

namespace RestClient.Test
{
    /// <summary>
    /// Test class for unit tests of the RestClientException class.
    /// </summary>
    [TestClass]
    public class RestClientExceptionTest
    {
        /// <summary>
        /// Gets or sets the test context for the current test.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Scenario: 
        ///   Instantiate a new instance of the RestClientException class with a message.
        /// Expected Result: 
        ///   A new RestClientException instance is created.
        /// Success Criteria: 
        ///   The Message property of the RestClientException is correctly set.
        /// </summary>
        [TestCategory("RestClient")]
        [TestMethod]
        public void RestClientExceptionTest_InstantiationWithMessage_MessageCorrect()
        {
            // Arrange
            const string ExpectedMessage = "Message";

            // Act
            RestClientException target = new RestClientException(ExpectedMessage);

            // Assert
            Assert.AreEqual(ExpectedMessage, target.Message);
        }

        /// <summary>
        /// Scenario: 
        ///   Instantiate a new instance of the RestClientException class with a message and error code.
        /// Expected Result: 
        ///   A new RestClientException instance is created.
        /// Success Criteria: 
        ///   The Message and the ErrorCode properties of the RestClientException is correctly set.
        /// </summary>
        [TestCategory("RestClient")]
        [TestMethod]
        public void RestClientExceptionTest_InstantiationWithMessageAndErrorCode_MessageAndErrorCodeCorrect()
        {
            // Arrange
            const string ExpectedMessage = "Message";
            const string ExpectedErrorCode = "ErrorCode";

            // Act
            RestClientException target = new RestClientException(ExpectedMessage, ExpectedErrorCode);

            // Assert
            Assert.AreEqual(ExpectedMessage, target.Message);
            Assert.AreEqual(ExpectedErrorCode, target.ErrorCode);
        }

        /// <summary>
        /// Scenario: 
        ///   Serialize and deserialize an instance of the RestClientException class.
        /// Expected Result: 
        ///   The RestClientException instance is serialized and deserialized successfully.
        /// Success Criteria: 
        ///   The new instance of the RestClientException class is identical to the original before serialization and deserialization.
        /// </summary>
        [TestCategory("RestClient")]
        [TestMethod]
        public void RestClientExceptionTest_SerializationDeserialization_InputEqualOutput()
        {
            // Arrange
            IOException innerException = new IOException("IOException");
            RestClientException expected = new RestClientException("Message", "ErrorCode", innerException);

            // Act
            RestClientException actual = Deserialize<RestClientException>(Serialize(expected));

            // Assert
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ErrorCode, actual.ErrorCode);

            Assert.IsNotNull(expected.InnerException);
            Assert.IsNotNull(actual.InnerException);
            Assert.AreEqual(expected.InnerException.Message, actual.InnerException.Message);
        }

        #region Private helper methods

        private static Stream Serialize(object source)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            return stream;
        }

        private static T Deserialize<T>(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }

        #endregion
    }
}
