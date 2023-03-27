using Maui.Android.MVVM.App.Platforms.Android.Repository.WebService;
using Maui.Android.MVVM.App.Repository;
using Microsoft.Extensions.Logging;


namespace Maui.Android.MVVM.App.Platfroms.Android.Activities;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<Application>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
            })
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            }).Services.AddSingleton<IRepository, WebRepository>();

#if DEBUG
        //builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}