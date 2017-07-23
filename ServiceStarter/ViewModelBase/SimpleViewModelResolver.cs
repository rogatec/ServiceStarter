using System;
using System.Linq;

namespace ServiceStarter.ViewModelBase {
    internal class SimpleViewModelResolver : IViewModelResolver {
        public object Resolve(string viewModelName) {
            var foundType = GetType().Assembly.GetTypes().FirstOrDefault(type => type.Name == viewModelName);
            return foundType == null ? null : Activator.CreateInstance(foundType);
        }
    }
}