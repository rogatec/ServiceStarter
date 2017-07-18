using System.Collections.ObjectModel;
using ServiceStarter.Models;
using System.Windows.Input;

namespace ServiceStarter.ViewModels
{
    class ServicesViewModel : IServicesVieModel
    {
        public ServicesViewModel()
        {
            IWindowsServiceProvider serviceProvider = new WindowsServiceProvider();
            Services = new ObservableCollection<IWindowsService>(serviceProvider.GetSQLServices());
        }

        public ObservableCollection<IWindowsService> Services
        {
            get; private set;
        }

        public IWindowsService SelectedService { get; set; }

        private ICommand _StartStop;
        public ICommand StartStop
        {
            get
            {
                if (_StartStop == null)
                {
                    _StartStop = new StartStopCommand<IWindowsService>(StartStopExecute);
                }

                return _StartStop;
            }
        }

        private void StartStopExecute(object parameter)
        {
            SelectedService.HandleStatus();
        }
    }
}
