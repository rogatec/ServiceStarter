using System.Dynamic;

namespace ServiceStarter.ViewModelBase {
    public class ViewModelLocator : DynamicObject {
        public ViewModelLocator() {
            Resolver = new DefaultViewModelResolver();
        }

        public IViewModelResolver Resolver { private get; set; }

        private object this[string viewModelName] => Resolver.Resolve(viewModelName);

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            result = this[binder.Name];
            return true;
        }
    }
}