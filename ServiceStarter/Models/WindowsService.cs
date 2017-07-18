using System;
using System.ComponentModel;
using System.ServiceProcess;

namespace ServiceStarter.Models
{
    public class WindowsService : IWindowsService, INotifyPropertyChanged
    {
        public ServiceControllerStatus CurrentStatus { get; private set; }
        
        public string DisplayName { get; private set; }

        public string ServiceName { get; private set; }

        public ServiceController Service { get; private set; }

        public WindowsService(ServiceController service)
        {
            Service = new ServiceController(service.ServiceName);
            DisplayName = Service.DisplayName;
            ServiceName = Service.ServiceName;
            CurrentStatus = Service.Status;
        }

        public void HandleStatus()
        {
            var timeout = TimeSpan.FromSeconds(10);
            if (Service.Status == ServiceControllerStatus.Stopped)
            {
                Service.Start();
                Service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            } else if (Service.Status == ServiceControllerStatus.Running)
            {
                Service.Stop();
                Service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            CurrentStatus = Service.Status;
            RaisePropertyChanged("CurrentStatus");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
