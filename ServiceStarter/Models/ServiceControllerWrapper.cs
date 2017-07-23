using System;
using System.ServiceProcess;

namespace ServiceStarter.Models {
    internal class ServiceControllerWrapper : IServiceControllerWrapper {
        private readonly ServiceController _serviceController;

        public ServiceControllerWrapper(ServiceController serviceController) {
            _serviceController = serviceController;
        }

        public string ServiceName => _serviceController.ServiceName;

        public string DisplayName => _serviceController.DisplayName;

        public ServiceControllerStatus Status => _serviceController.Status;

        public void StatusWait(ServiceControllerStatus serviceStatus) {
            var timeout = TimeSpan.FromSeconds(10);

            _serviceController.WaitForStatus(serviceStatus, timeout);
        }

        public void StartService() {
            _serviceController.Start();
        }

        public void StopService() {
            _serviceController.Stop();
        }
    }
}