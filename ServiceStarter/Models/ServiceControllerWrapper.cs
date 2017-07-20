using System;
using System.ServiceProcess;

namespace ServiceStarter.Models
{
    class ServiceControllerWrapper : IServiceControllerWrapper
    {
        private ServiceController _serviceController;
        public ServiceControllerWrapper(ServiceController serviceController)
        {
            _serviceController = serviceController;
        }
        public string ServiceName { get { return _serviceController.ServiceName;  } }

        public string DisplayName { get { return _serviceController.DisplayName; } }

        public ServiceControllerStatus Status { get { return _serviceController.Status; } }
        public void StatusWait(ServiceControllerStatus serviceStatus)
        {
            var timeout = TimeSpan.FromSeconds(10);

            _serviceController.WaitForStatus(serviceStatus, timeout);
        }

        public void StartService()
        {
            _serviceController.Start();
        }

        public void StopService()
        {
            _serviceController.Stop();
        }
    }
}
