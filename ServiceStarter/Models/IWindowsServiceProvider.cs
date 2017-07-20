using System.Collections.Generic;

namespace ServiceStarter.Models
{
    public interface IWindowsServiceProvider
    {
        List<IWindowsService> GetSQLServices();
    }
}
