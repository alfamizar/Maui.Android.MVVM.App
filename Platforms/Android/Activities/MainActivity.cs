using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using System.Diagnostics;
using Google.Android.Material.FloatingActionButton;
using Android.Views;
using AndroidX.Navigation.UI;
using AndroidX.Navigation;
using Google.Android.Material.Snackbar;
using View = Android.Views.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.Navigation;
using AndroidX.Navigation.Fragment;

namespace Maui.Android.MVVM.App.Platfroms.Android.Activities;

[Activity(Theme = "@style/Theme.MyApplication.NoActionBar", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : AppCompatActivity
{
    private AppBarConfiguration _appBarConfiguration;
    private FloatingActionButton _floatingActionButton;
    private AndroidX.AppCompat.Widget.Toolbar _toolbar;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.activity_main);

        _toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);

        SetSupportActionBar(_toolbar);

        _floatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.fab);
        _floatingActionButton.Click += FabOnClick;

        DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
        NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        _appBarConfiguration = new AppBarConfiguration.Builder(
                Resource.Id.nav_all, Resource.Id.nav_females, Resource.Id.nav_males)
                .SetOpenableLayout(drawer)
                .Build();
        /*NavController navController = Navigation.FindNavController(this, Resource.Id.nav_host_fragment_content_main);*/
        NavHostFragment navHostFragment = (NavHostFragment) SupportFragmentManager.FindFragmentById(Resource.Id.nav_host_fragment_content_main);
        NavController navController = navHostFragment.NavController;
        NavigationUI.SetupActionBarWithNavController(this, navController, _appBarConfiguration);
        NavigationUI.SetupWithNavController(navigationView, navController);
    }

    private void FabOnClick(object sender, EventArgs e)
    {
        Snackbar.Make((View)sender, "Replace with your own action", BaseTransientBottomBar.LengthLong)
            .SetAction("Action", clickHandler: null).Show();
    }

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
        // Inflate the menu; this adds items to the action bar if it is present.
        MenuInflater.Inflate(Resource.Menu.main, menu);
        return true;
    }

    public override bool OnSupportNavigateUp()
    {
        NavController navController = Navigation.FindNavController(this, Resource.Id.nav_host_fragment_content_main);
        return NavigationUI.NavigateUp(navController, _appBarConfiguration)
                || base.OnSupportNavigateUp();
    }
}
