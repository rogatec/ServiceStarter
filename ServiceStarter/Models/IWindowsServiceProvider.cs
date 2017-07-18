using System.Collections.Generic;

namespace ServiceStarter.Models
{
    interface IWindowsServiceProvider
    {
        List<IWindowsService> GetSQLServices();
    }
}
