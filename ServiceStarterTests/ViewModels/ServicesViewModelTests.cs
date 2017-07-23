using System.Collections.ObjectModel;
using System.Windows.Input;
using NUnit.Framework;
using ServiceStarter.Models;
using ServiceStarter.ViewModels;

namespace ServiceStarterTests.ViewModels {
    [TestFixture]
    public class ServicesViewModelTests {
        private ServicesViewModel _viewModel;

        [SetUp]
        protected void SetUp() {
            _viewModel = new ServicesViewModel();
        }

        [Test]
        public void GetServices_ViewModelIsInstantiated_ReturnsInstanceOfObservableCollection() {
            Assert.IsInstanceOf<ObservableCollection<IWindowsService>>(_viewModel.Services);
        }

        [Test]
        public void GetServices_ViewModelIsInstantiated_NotNull() {
            Assert.IsNotNull(_viewModel.Services);
        }

        [Test]
        public void GetServices_CollectionCount_IsTwo() {
            Assert.That(2, Is.EqualTo(_viewModel.Services.Count));
        }

        [Test]
        public void GetStartStopCommand_ViewModelIsInstantiated_InstanceOfICommand() {
            Assert.IsInstanceOf<ICommand>(_viewModel.StartStop);
        }

        [Test]
        public void GetStartStopCommand_ViewModelIsInstantiated_NotNull() {
            Assert.IsNotNull(_viewModel.StartStop);
        }
    }
}