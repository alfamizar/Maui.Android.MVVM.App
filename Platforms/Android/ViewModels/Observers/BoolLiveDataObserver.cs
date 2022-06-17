using AndroidX.Lifecycle;

namespace Maui.Android.MVVM.App.Platforms.Android.ViewModels.Observers
{
    public class BoolLiveDataObserver : Java.Lang.Object, IObserver
    {
        private readonly Action<bool> _action;

        public BoolLiveDataObserver(Action<bool> action)
        {
            _action = action;
        }

        public void OnChanged(Java.Lang.Object p0)
        {
            _action?.Invoke((bool)p0);
        }
    }
}
