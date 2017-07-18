using System;
using System.Linq;

namespace ServiceStarter.ViewModelBase
{
    class SimpleViewModelResolver : IViewModelResolver
    {
        public object Resolve(string viewModelName)
        {
            var foundType = GetType().Assembly.GetTypes().FirstOrDefault(type => type.Name == viewModelName);
            if (foundType == null)
                return null;

            return Activator.CreateInstance(foundType);
        }
    }
}
