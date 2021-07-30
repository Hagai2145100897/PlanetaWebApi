using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanetaWebApi.Controllers;

namespace PlanetaWebApi.Tests.TestControllers
{
    [TestClass]
    public class TestClientSubnetsController
    {
        private ClientSubnetsRepositoryForTest repository => new ClientSubnetsRepositoryForTest();
        private ClientSubnetsController controller => new ClientSubnetsController(repository);

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
        }

        [TestMethod]
        public void TestCorrectResponse()
        {
            var result1 = controller.Get(1);
            var result2 = controller.Get(2);
            var result3 = controller.Get(3);

            CollectionAssert.Equals(result1, repository.Get(1));
            CollectionAssert.Equals(result2, repository.Get(2));
            CollectionAssert.Equals(result3, repository.Get(3));
        }

        [TestMethod]
        // Not Implemented
        public void TestNoResponse()
        {
            var result1 = controller.Get(0);
        }
    }
}
