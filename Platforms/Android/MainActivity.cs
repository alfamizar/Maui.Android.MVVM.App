using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using Maui.Android.MVVM.App.Platforms.Android.ViewModels;
using AndroidX.Lifecycle;
using Maui.Android.MVVM.App.Platforms.Android.Adapters;
using AndroidX.RecyclerView.Widget;
using Maui.Android.MVVM.App.Platforms.Android.Models;
using System.ComponentModel;
using System.Diagnostics;
using Google.Android.Material.FloatingActionButton;
using Debug = System.Diagnostics.Debug;
using static Maui.Android.MVVM.App.Platforms.Android.Adapters.UsersAdapter;
using Google.Android.Material.ProgressIndicator;
using Android.Views;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Maui.Android.MVVM.App;

[Activity(Theme = "@style/Theme.MyApplication.NoActionBar", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : AppCompatActivity, IItemClickListener
{
    private UsersAdapter _itemsAdapter;
    private RecyclerView _recyclerView;
    private FloatingActionButton _floatingActionButton;
    private CircularProgressIndicator _isBusyProgressBar;
    private MainActivityUsersViewModel _viewModel;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.ActivityMain);

        _viewModel = new ViewModelProvider(this)
            .Get(Java.Lang.Class.FromType(typeof(MainActivityUsersViewModel))) as MainActivityUsersViewModel;

        _viewModel.UsersList.CollectionChanged += UsersViewModelCollectionChanged;

        _viewModel.PropertyChanged += UsersViewModelPropertyChanged;

        InitViews();
    }

    private void InitViews()
    {
        _isBusyProgressBar = FindViewById<CircularProgressIndicator>(Resource.Id.IsBusyProgressBar);
        InitFab();
        InitRecycler();
    }

    private void InitFab()
    {
        _floatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.FetchUsersButton);
        _floatingActionButton.Click += async (o, s) =>
        {
            await _viewModel.FetchUsersList("female", 25);
        };
    }

    private void InitRecycler()
    {
        _itemsAdapter = new UsersAdapter();
        _itemsAdapter.SetOnClickListener(this);
        _itemsAdapter.UsersList = new List<User>(_viewModel.UsersList);
        _recyclerView = FindViewById<RecyclerView>(Resource.Id.UsersRecyclerView);
        _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
        _recyclerView.SetAdapter(_itemsAdapter);
    }

    private void UsersViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "IsBusy":
                if (_viewModel.IsBusy)
                {
                    ChangeProgressBarVisibility(ViewStates.Visible);
                }
                else
                {
                    ChangeProgressBarVisibility(ViewStates.Invisible);
                    _recyclerView.SmoothScrollToPosition(_recyclerView.GetAdapter().ItemCount - 1);
                }
                Debug.WriteLine("IsBusyChanged");
                break;
            default:
                Debug.WriteLine("SomePropertyChanged");
                break;
        }
    }

    private void UsersViewModelCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        {
            _itemsAdapter.UsersList.Add(e.NewItems[0] as User);
        }

        new Handler(MainLooper).Post(() =>
        {
            _itemsAdapter.NotifyDataSetChanged();
        });
    }

    private void ChangeProgressBarVisibility(ViewStates viewState)
    {
        new Handler(MainLooper).Post(() =>
        {
            _isBusyProgressBar.Visibility = viewState;
        });
    }

    public void OnItemClicked(global::Android.Views.View view, int position)
    {
        Debug.WriteLine($"Position {position} clicked");
    }
}
