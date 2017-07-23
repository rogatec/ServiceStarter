using System.Collections.Generic;
using System.ServiceProcess;
using Moq;
using NUnit.Framework;
using ServiceStarter.Models;

namespace ServiceStarterTests.Models {
    [TestFixture]
    public class WindowsServiceTests {
        private Mock<IServiceControllerWrapper> _serviceMock;
        private WindowsService _windowsService;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            // TODO: we need service mock
            _serviceMock = new Mock<IServiceControllerWrapper>();
            _serviceMock.Setup(s => s.ServiceName).Returns("MSSQL$SQLTEC");
            _serviceMock.Setup(s => s.DisplayName).Returns("SQLTEC");
        }

        [SetUp]
        public void Setup() {
            _windowsService = new WindowsService(_serviceMock.Object);
        }

        [Test]
        public void Constructor_ParamIsServiceController_DisplayNamePropertyNotNull() {
            Assert.IsNotNull(_windowsService.DisplayName);
        }

        [Test]
        public void Constructor_ParamIsDisplayName_DisplayName() {
            Assert.That(_windowsService.DisplayName, Is.EqualTo("SQLTEC"));
        }

        [Test]
        public void Constructor_ParamIsServiceName_ServiceName() {
            Assert.That(_windowsService.ServiceName, Is.EqualTo("MSSQL$SQLTEC"));
        }

        [Test]
        public void Constructor_ParamIsStatus_GetStatus() {
            _serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Stopped);
            var stoppedService = new WindowsService(_serviceMock.Object);

            Assert.That(stoppedService.CurrentStatus, Is.EqualTo(ServiceControllerStatus.Stopped));
        }

        [Test]
        public void Constructor_ParamCurrentStatus_IsStopped() {
            _serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Stopped);
            var stoppedService = new WindowsService(_serviceMock.Object);

            Assert.That(stoppedService.CurrentStatus, Is.EqualTo(ServiceControllerStatus.Stopped));
        }

        [Test]
        public void Constructor_ParamCurrentStatus_IsRunning() {
            _serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Running);
            var stoppedService = new WindowsService(_serviceMock.Object);

            Assert.That(stoppedService.CurrentStatus, Is.EqualTo(ServiceControllerStatus.Running));
        }

        [Test]
        public void HandleStatus_ServiceStatusFromStoppedToRunning_CurrentStatusHasChanged() {
            _serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Stopped);
            var stoppedService = new WindowsService(_serviceMock.Object);
            var serviceStatusChanged = new List<string>();

            stoppedService.PropertyChanged += (sender, eventArgs) => serviceStatusChanged.Add(eventArgs.PropertyName);

            stoppedService.HandleStatus();

            Assert.Contains("CurrentStatus", serviceStatusChanged);
            Assert.That(stoppedService.CurrentStatus, Is.Not.EqualTo(ServiceControllerStatus.Running));
        }

        [Test]
        public void HandleStatus_ServiceStatusFromRunningToStopped_CurrentStatusHasChanged() {
            _serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Running);
            var startedService = new WindowsService(_serviceMock.Object);
            var serviceStatusChanged = new List<string>();

            startedService.PropertyChanged += (sender, eventArgs) => serviceStatusChanged.Add(eventArgs.PropertyName);

            startedService.HandleStatus();

            Assert.Contains("CurrentStatus", serviceStatusChanged);
            Assert.That(startedService.CurrentStatus, Is.Not.EqualTo(ServiceControllerStatus.Stopped));
        }
    }
}