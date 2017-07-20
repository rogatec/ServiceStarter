using System.Collections.Generic;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Windows;

namespace ServiceStarter.Models
{
    public class WindowsServiceProvider : IWindowsServiceProvider
    {
        private List<IWindowsService> _list;

        public WindowsServiceProvider() { }

        public List<IWindowsService> GetSQLServices()
        {
            if (_list != null) return _list;

            _list = new List<IWindowsService>();
            AddServices();

            if (_list.Count != 2)
                ShutdownIfNoServicesFound();

            return _list;
        }

        private void AddServices()
        {
            ServiceController[] scServices = ServiceController.GetServices();

            foreach (ServiceController scTemp in scServices)
            {
                Match match = Regex.Match(scTemp.ServiceName, @"MSSQL\$");
                if (match.Success || scTemp.ServiceName == "SQLBrowser")
                {
                    var wrapper = new ServiceControllerWrapper(new ServiceController(scTemp.ServiceName));
                    _list.Add(new WindowsService(wrapper));
                }
            }
        }

        private void ShutdownIfNoServicesFound()
        {
            MessageBoxResult boxResult = MessageBox.
                Show("Es wurden keine SQL Server Instanzen gefunden. Bitte in der 'ServiceStarter.exe.config' pflegen",
                "SQL Server nicht gefunden",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            if (boxResult == MessageBoxResult.OK)
                Application.Current.Shutdown();

        }
    }    
}
