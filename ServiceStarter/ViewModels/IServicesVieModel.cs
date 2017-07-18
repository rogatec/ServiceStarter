using ServiceStarter.Models;
using System.Collections.ObjectModel;

namespace ServiceStarter.ViewModels
{
    interface IServicesVieModel
    {
        ObservableCollection<IWindowsService> Services { get; }
    }
}
