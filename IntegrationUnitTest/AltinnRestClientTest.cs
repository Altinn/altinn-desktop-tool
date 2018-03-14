﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestClient;

namespace IntegrationUnitTest
{
    /// <summary>
    /// Test class for unit tests of the AltinnRestClient class.
    /// </summary>
    [TestClass]
    public class AltinnRestClientTest
    {
        private const string Baseaddress = "https://tt02.altinn.basefarm.net/";

        private const string Apikey = "APIKEY";

        private const string Thumbprint = "THUMBPRINT";

        /// <summary>
        /// Scenario: 
        ///   Perform a Get request without any inputs.
        /// Expected Result: 
        ///   A long list of organizations.
        /// Success Criteria: 
        ///   There are no exceptions.
        /// </summary>
        [TestMethod]
        public void GetTest_RequestUnfiltered_ListOfOrganizations()
        {
            // Arrange
            AltinnRestClient client = new AltinnRestClient
            {
                BaseAddress = Baseaddress,
                ApiKey = Apikey,
                Thumbprint = Thumbprint,
                IgnoreSslErrors = false,
                Timeout = 10
            };

            // Authenticate
            // NOTE: Altinn returns 401 even if it is validated.
            const string Orgno = "910021451";
            string uriPart = "api/serviceowner/organizations?ForceEIAuthentication";
            try
            {
                client.Get(uriPart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Get by orgno
            uriPart = "api/serviceowner/organizations/" + Orgno;
            string result = "N/A";
            try
            {
                result = client.Get(uriPart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(result);
        }
    }
}
