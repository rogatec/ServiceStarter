using System.ServiceProcess;

namespace ServiceStarter.Models
{
    public interface IWindowsService
    {
        IServiceControllerWrapper Controller { get; }

        string ServiceName { get; }

        string DisplayName { get; }

        ServiceControllerStatus CurrentStatus { get; }

        void HandleStatus();
    }
}
