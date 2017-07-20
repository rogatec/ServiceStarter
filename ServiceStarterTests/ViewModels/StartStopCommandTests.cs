using NUnit.Framework;

namespace ServiceStarter.ViewModels.Tests
{
    [TestFixture]
    public class StartStopCommandTests
    {
        private IServicesViewModel vm;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            vm = new ServicesViewModel();
            vm.SelectedService = vm.Services[0];
        }

        [Test]
        public void Execute()
        {
            vm.StartStop.Execute(null);
        }

        [Test]
        public void CanExecute()
        {
            Assert.IsTrue(vm.StartStop.CanExecute(null));
        }
    }
}