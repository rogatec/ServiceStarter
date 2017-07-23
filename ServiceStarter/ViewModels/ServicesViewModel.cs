using System.Collections.ObjectModel;
using ServiceStarter.Models;
using System.Windows.Input;

namespace ServiceStarter.ViewModels {
    public class ServicesViewModel : IServicesViewModel {
        public ServicesViewModel() {
            IWindowsServiceProvider serviceProvider = new WindowsServiceProvider();
            Services = new ObservableCollection<IWindowsService>(serviceProvider.GetSqlServices());
        }

        public ObservableCollection<IWindowsService> Services { get; }

        public IWindowsService SelectedService { private get; set; }

        private ICommand _startStop;

        public ICommand StartStop => _startStop ??
                                     (_startStop = new StartStopCommand<IWindowsService>(StartStopExecute));

        private void StartStopExecute(object parameter) {
            SelectedService.HandleStatus();
        }
    }
}