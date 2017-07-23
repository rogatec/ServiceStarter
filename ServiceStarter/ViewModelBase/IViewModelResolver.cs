namespace ServiceStarter.ViewModelBase {
    public interface IViewModelResolver {
        object Resolve(string viewModelName);
    }
}