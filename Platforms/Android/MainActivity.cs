using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using Maui.Android.MVVM.App.Platforms.Android.ViewModels;
using AndroidX.Lifecycle;

using static Maui.Android.MVVM.App.Platforms.Android.Adapters.ItemsAdapter;
using Maui.Android.MVVM.App.Platforms.Android.Adapters;
using AndroidX.RecyclerView.Widget;
using Maui.Android.MVVM.App.Platforms.Android.Models;
using System.ComponentModel;
using System.Diagnostics;
using Debug = System.Diagnostics.Debug;

namespace Maui.Android.MVVM.App;

[Activity(Theme = "@style/Theme.MyApplication.NoActionBar", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : AppCompatActivity, IItemClickListener
{
    private ItemsAdapter _itemsAdapter;
    private RecyclerView _recyclerView;
    private MainActivityViewModel _viewModel;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.ActivityMain);

        _recyclerView = FindViewById<RecyclerView>(Resource.Id.UsersRecyclerView);

        _viewModel = new ViewModelProvider(this)
            .Get(Java.Lang.Class.FromType(typeof(MainActivityViewModel))) as MainActivityViewModel;

        _viewModel.GetUsersList().CollectionChanged += MainActivity_CollectionChanged;

        _viewModel.PropertyChanged += ViewModelPropertyChanged;

        InitRecycler();
    }

    private void InitRecycler()
    {
        _itemsAdapter = new ItemsAdapter();
        _itemsAdapter.SetOnClickListener(this);
        _itemsAdapter.UsersList = new List<User>(_viewModel.GetUsersList());
        _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
        _recyclerView.SetAdapter(_itemsAdapter);
    }

    private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "IsBusy":
                Debug.WriteLine("IsBusyChanged");
                break;
        }
    }

    private void MainActivity_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        _itemsAdapter.NotifyDataSetChanged();
    }

    public void OnItemClicked(global::Android.Views.View view, int position)
    {
        Debug.WriteLine($"Position {position} clicked");
    }
}
