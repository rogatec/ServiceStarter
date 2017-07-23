using System.Collections.Generic;
using NUnit.Framework;
using ServiceStarter.Models;

namespace ServiceStarterTests.Models {
    [TestFixture]
    public class WindowsServiceProviderTests {
        private WindowsServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp() {
            _serviceProvider = new WindowsServiceProvider();
        }

        [Test]
        public void WindowsServiceProviderTest() {
            Assert.IsInstanceOf<IWindowsServiceProvider>(_serviceProvider);
        }

        [Test]
        public void GetSQLServices_ProviderIsInstantiated_ReturnsListOfIWindowsServices() {
            Assert.IsInstanceOf<List<IWindowsService>>(_serviceProvider.GetSqlServices());
        }

        [Test]
        public void GETSQLServices_ProvidedIsInstantiated_ListCountIsTwo() {
            Assert.That(_serviceProvider.GetSqlServices().Count, Is.EqualTo(2));
        }
    }
}