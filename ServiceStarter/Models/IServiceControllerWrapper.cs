using System.ServiceProcess;

namespace ServiceStarter.Models
{
    public interface IServiceControllerWrapper
    {
        string ServiceName { get; }
        string DisplayName { get; }
        ServiceControllerStatus Status { get; }

        void StartService();

        void StopService();

        void StatusWait(ServiceControllerStatus serviceStatus);
    }
}