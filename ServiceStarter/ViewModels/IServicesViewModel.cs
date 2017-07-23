using ServiceStarter.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ServiceStarter.ViewModels {
    public interface IServicesViewModel {
        ObservableCollection<IWindowsService> Services { get; }

        IWindowsService SelectedService { set; }

        ICommand StartStop { get; }
    }
}