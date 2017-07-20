using NUnit.Framework;
using System.Collections.ObjectModel;
using ServiceStarter.Models;
using System.Windows.Input;

namespace ServiceStarter.ViewModels.Tests
{
    [TestFixture]
    public class ServicesViewModelTests
    {
        private ServicesViewModel viewModel;

        [SetUp]
        protected void SetUp()
        {
            viewModel = new ServicesViewModel();
        }

        [Test]
        public void GetServices_ViewModelIsInstantiated_ReturnsInstanceOfObservableCollection()
        {
            Assert.IsInstanceOf<ObservableCollection<IWindowsService>>(viewModel.Services);
        }

        [Test]
        public void GetServices_ViewModelIsInstantiated_NotNull()
        {
            Assert.IsNotNull(viewModel.Services);
        }

        [Test]
        public void GetServices_CollectionCount_IsTwo()
        {
            Assert.AreEqual(2, viewModel.Services.Count);
        }

        [Test]
        public void GetStartStopCommand_ViewModelIsInstantiated_InstanceOfICommand()
        {
            Assert.IsInstanceOf<ICommand>(viewModel.StartStop);            
        }

        [Test]
        public void GetStartStopCommand_ViewModelIsInstantiated_NotNull()
        {
            Assert.IsNotNull(viewModel.StartStop);
        }
    }
}