using System.ComponentModel;
using System.ServiceProcess;

namespace ServiceStarter.Models
{
    public class WindowsService : IWindowsService, INotifyPropertyChanged
    {
        IServiceControllerWrapper _serviceWrapper;

        public string ServiceName { get { return _serviceWrapper.ServiceName; } }

        public string DisplayName { get { return _serviceWrapper.DisplayName; } }

        public ServiceControllerStatus CurrentStatus
        {
            get { return _serviceWrapper.Status; } private set { }
        }
        public WindowsService(IServiceControllerWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
            CurrentStatus = _serviceWrapper.Status;
        }

        public IServiceControllerWrapper Controller { get { return _serviceWrapper; } }
        
        public void HandleStatus()
        {
            if (_serviceWrapper.Status == ServiceControllerStatus.Stopped)
            {
                _serviceWrapper.StartService();
                _serviceWrapper.StatusWait(ServiceControllerStatus.Running);
            } else if (_serviceWrapper.Status == ServiceControllerStatus.Running)
            {
                _serviceWrapper.StopService();
                _serviceWrapper.StatusWait(ServiceControllerStatus.Stopped);
            }
            CurrentStatus = _serviceWrapper.Status == ServiceControllerStatus.Running ? ServiceControllerStatus.Running : ServiceControllerStatus.Stopped;
            RaisePropertyChanged("CurrentStatus");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
