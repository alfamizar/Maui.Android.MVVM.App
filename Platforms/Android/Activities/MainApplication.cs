using Android.App;
using Android.Runtime;

namespace Maui.Android.MVVM.App.Platfroms.Android.Activities;

#if DEBUG
    [Application(Debuggable = true)]
#else
[Application(Debuggable = false)]
#endif
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
        {
        }

    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
