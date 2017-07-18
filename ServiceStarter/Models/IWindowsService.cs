using System.ServiceProcess;

namespace ServiceStarter.Models
{
    interface IWindowsService
    {
        ServiceControllerStatus CurrentStatus { get; }

        string DisplayName { get; }

        string ServiceName { get; }

        ServiceController Service { get; }

        void HandleStatus();
    }
}
