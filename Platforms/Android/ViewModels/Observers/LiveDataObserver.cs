using AndroidX.Lifecycle;

namespace Maui.Android.MVVM.App.Platforms.Android.ViewModels.Observers
{
    internal class LiveDataObserver<T> : Java.Lang.Object, IObserver
        where T : class
    {
        private readonly Action<T> _action;

        public LiveDataObserver(Action<T> action)
        {
            _action = action;
        }

        public void OnChanged(Java.Lang.Object p0)
        {
            _action?.Invoke(p0 as T);
        }
    }
}
