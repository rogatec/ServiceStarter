using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.ServiceProcess;

namespace ServiceStarter.Models.Tests
{
    [TestFixture]
    public class WindowsServiceTests
    {
        private Mock<IServiceControllerWrapper> serviceMock;
        private WindowsService windowsService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            serviceMock = new Mock<IServiceControllerWrapper>();
            serviceMock.Setup(s => s.ServiceName).Returns("MSSQL$SQLTEC");
            serviceMock.Setup(s => s.DisplayName).Returns("SQLTEC");
        }

        [SetUp]
        public void Setup()
        {
            windowsService = new WindowsService(serviceMock.Object);
        }

        [Test]
        public void Constructor_ParamIsServiceController_DisplayNamePropertyNotNull()
        {
            Assert.IsNotNull(windowsService.DisplayName);
        }

        [Test]
        public void Constructor_ParamIsDisplayName_DisplayName()
        {
            Assert.AreEqual("SQLTEC", windowsService.DisplayName);
        }

        [Test]
        public void Constructor_ParamIsServiceName_ServiceName()
        {
            Assert.AreEqual("MSSQL$SQLTEC", windowsService.ServiceName);
        }

        [Test]
        public void Constructor_ParamIsStatus_GetStatus()
        {
            serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Stopped);
            var stoppedService = new WindowsService(serviceMock.Object);

            Assert.AreEqual(ServiceControllerStatus.Stopped, stoppedService.CurrentStatus);
        }

        [Test]
        public void Constructor_ParamCurrentStatus_IsStopped()
        {
            serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Stopped);
            var stoppedService = new WindowsService(serviceMock.Object);

            Assert.AreEqual(ServiceControllerStatus.Stopped, stoppedService.CurrentStatus);
        }

        [Test]
        public void Constructor_ParamCurrentStatus_IsRunning()
        {
            serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Running);
            var stoppedService = new WindowsService(serviceMock.Object);

            Assert.AreEqual(ServiceControllerStatus.Running, stoppedService.CurrentStatus);
        }

        [Test]
        public void HandleStatus_ServiceStatusFromStoppedToRunning_CurrentStatusHasChanged()
        {
            serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Stopped);
            var stoppedService = new WindowsService(serviceMock.Object);
            var serviceStatusChanged = new List<string>();

            stoppedService.PropertyChanged += (sender, eventArgs) => serviceStatusChanged.Add(eventArgs.PropertyName);

            stoppedService.HandleStatus();

            Assert.Contains("CurrentStatus", serviceStatusChanged);
            Assert.AreNotEqual(stoppedService.CurrentStatus, ServiceControllerStatus.Running);
        }

        [Test]
        public void HandleStatus_ServiceStatusFromRunningToStopped_CurrentStatusHasChanged()
        {
            serviceMock.Setup(s => s.Status).Returns(ServiceControllerStatus.Running);
            var startedService = new WindowsService(serviceMock.Object);
            var serviceStatusChanged = new List<string>();

            startedService.PropertyChanged += (sender, eventArgs) => serviceStatusChanged.Add(eventArgs.PropertyName);

            startedService.HandleStatus();

            Assert.Contains("CurrentStatus", serviceStatusChanged);
            Assert.AreNotEqual(startedService.CurrentStatus, ServiceControllerStatus.Stopped);
        }
    }
}