using AndroidX.Lifecycle;

namespace Maui.Android.MVVM.App.Platfroms.Android.ViewModels
{
    public class AndroidBaseViewModel : ViewModel
    {
        protected MutableLiveData IsBusy;

        public AndroidBaseViewModel()
        {
            IsBusy = new MutableLiveData();
        }
    }
}
