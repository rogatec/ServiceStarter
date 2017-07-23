using System;
using System.ComponentModel;
using System.ServiceProcess;

namespace ServiceStarter.Models {
    public class WindowsService : IWindowsService, INotifyPropertyChanged {
        public string ServiceName => Controller.ServiceName;

        public string DisplayName => Controller.DisplayName;

        public ServiceControllerStatus CurrentStatus {
            get => Controller.Status;
            private set {
                if (!Enum.IsDefined(typeof(ServiceControllerStatus), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ServiceControllerStatus));
            }
        }

        public WindowsService(IServiceControllerWrapper serviceWrapper) {
            Controller = serviceWrapper;
            CurrentStatus = Controller.Status == 0 ? ServiceControllerStatus.Stopped : Controller.Status;
        }

        private IServiceControllerWrapper Controller { get; }

        public void HandleStatus() {
            switch (Controller.Status) {
                case ServiceControllerStatus.Stopped:
                    Controller.StartService();
                    Controller.StatusWait(ServiceControllerStatus.Running);
                    break;
                case ServiceControllerStatus.Running:
                    Controller.StopService();
                    Controller.StatusWait(ServiceControllerStatus.Stopped);
                    break;
                default:
                    throw new NotImplementedException();
            }
            CurrentStatus = Controller.Status == ServiceControllerStatus.Running
                ? ServiceControllerStatus.Running
                : ServiceControllerStatus.Stopped;
            RaisePropertyChanged("CurrentStatus");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}