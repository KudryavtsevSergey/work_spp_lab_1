using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab1;
using Moq;
using lab1.ServiceNews;

namespace ClientTest
{
    [TestClass]
    public class ClientTest
    {
        /*
        [TestMethod]
        public void Client_CreateNewClient_CreatedNewClient()
        {
            Client client = null;
            try
            {
                client = new Client("https://www.notebook-center.ru/");
                Assert.IsNotNull(client);
            }
            catch (ArgumentException)
            {
                Assert.Fail("No exception shoudn't be thrown");
            }
        }

        [TestMethod]
        public void Client_CreateNewClientWithWrongUri_IsNotCreatedNewClient()
        {
            String expected = "Incorrect uri.";
            Client client = null;
            try
            {
                client = new Client("test");
                Assert.Fail("No exception was thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        public void Client_CreateNewClientWithNullUri_IsNotCreatedNewClient()
        {
            String expected = "Incorrect uri.";
            Client client = null;
            try
            {
                client = new Client(null);
                Assert.Fail("No exception was thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }*/

        [TestMethod]
        public void Client_ClientGetNews_GetNews()
        {
            var mockService= new Mock<IServiceNews>();
            mockService.Setup(s => s.GetNews(It.IsAny<Uri>()));
        }
    }
}
