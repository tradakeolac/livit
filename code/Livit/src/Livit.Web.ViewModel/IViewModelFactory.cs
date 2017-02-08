namespace Livit.Web.ViewModel
{
    public interface IViewModelFactory
    {
        TViewModel Create<TViewModel>(object serviceObject);
    }
}