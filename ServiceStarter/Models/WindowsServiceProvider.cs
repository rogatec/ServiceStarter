using System.Collections.Generic;
using System.ServiceProcess;

namespace ServiceStarter.Models
{
    class WindowsServiceProvider : IWindowsServiceProvider
    {
        private List<IWindowsService> _list;

        public WindowsServiceProvider() { }

        public List<IWindowsService> GetSQLServices()
        {
            if (_list != null) return _list;

            _list = new List<IWindowsService>();
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();

            foreach (ServiceController scTemp in scServices)
            {
                //TODO: use it as input parameters
                if (scTemp.ServiceName == "MSSQL$SQLTEC" || scTemp.ServiceName == "SQLBrowser")
                {
                    _list.Add(new WindowsService(scTemp));
                }
            }

            return _list;
        }
    }
}
