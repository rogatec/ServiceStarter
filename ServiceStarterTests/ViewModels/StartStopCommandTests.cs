using NUnit.Framework;
using ServiceStarter.ViewModels;

namespace ServiceStarterTests.ViewModels {
    [TestFixture]
    public class StartStopCommandTests {
        private IServicesViewModel _vm;

        [OneTimeSetUp]
        public void OneTimeSetup() {
            _vm = new ServicesViewModel();
            _vm.SelectedService = _vm.Services[0];
        }

        [Test]
        public void CanExecute() {
            Assert.IsTrue(_vm.StartStop.CanExecute(null));
        }

        [Test]
        public void Execute() {
            _vm.StartStop.Execute(null);
        }
    }
}