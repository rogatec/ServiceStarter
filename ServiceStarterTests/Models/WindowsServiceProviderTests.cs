using NUnit.Framework;
using System.Collections.Generic;

namespace ServiceStarter.Models.Tests
{
    [TestFixture]
    public class WindowsServiceProviderTests
    {
        private WindowsServiceProvider serviceProvider;

        [SetUp]
        public void SetUp()
        {
            serviceProvider = new WindowsServiceProvider();
        }

        [Test]
        public void WindowsServiceProviderTest()
        {
            Assert.IsInstanceOf<IWindowsServiceProvider>(serviceProvider);
        }

        [Test]
        public void GetSQLServices_ProviderIsInstantiated_ReturnsListOfIWindowsServices()
        {
            Assert.IsInstanceOf<List<IWindowsService>>(serviceProvider.GetSQLServices());
        }

        [Test]
        public void GETSQLServices_ProvidedIsInstantiated_ListCountIsTwo()
        {
            Assert.AreEqual(2, serviceProvider.GetSQLServices().Count);
        }
    }
}