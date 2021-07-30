using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanetaWebApi.Controllers;

namespace PlanetaWebApi.Tests.TestControllers
{
    [TestClass]
    public class TestClientSubnetsController
    {
        private ClientSubnetsController controller =>
            new ClientSubnetsController(new ClientSubnetsRepositoryForTest());

        [TestMethod]
        public void TestImplementation()
        {
            try
            {
                controller.Get(0);
            }
            catch (System.NotImplementedException)
            {
                Assert.Fail();
            }
            catch (System.Exception)
            {
            }
        }
    }
}
