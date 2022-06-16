using Android.OS;
using Android.Views;
using AndroidX.Fragment.App;
using AndroidX.Lifecycle;
using AndroidX.Navigation;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.ProgressIndicator;
using Maui.Android.MVVM.App.Platforms.Android.Adapters;
using Maui.Android.MVVM.App.Platforms.Android.Models;
using Maui.Android.MVVM.App.Platforms.Android.ViewModels;
using System.ComponentModel;
using static Maui.Android.MVVM.App.Platforms.Android.Adapters.UsersAdapter;
using Debug = System.Diagnostics.Debug;
using View = Android.Views.View;

namespace Maui.Android.MVVM.App.Platfroms.Android.Fragments
{
    public class RandomGenderFragment : Fragment, IItemClickListener
    {
        protected View _view;
        private UsersAdapter _itemsAdapter;
        private RecyclerView _recyclerView;
        protected FloatingActionButton _floatingActionButton;
        private CircularProgressIndicator _isBusyProgressBar;
        protected RandomPersonsListViewModel _viewModel;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.fragment_persons, container, false);

            _viewModel = new ViewModelProvider(this)
                .Get(Java.Lang.Class.FromType(typeof(RandomPersonsListViewModel))) as RandomPersonsListViewModel;

            _viewModel.UsersList.CollectionChanged += UsersViewModelCollectionChanged;
            _viewModel.PropertyChanged += UsersViewModelPropertyChanged;

            InitViews();

            return _view;
        }

        private void InitViews()
        {
            _isBusyProgressBar = _view.FindViewById<CircularProgressIndicator>(Resource.Id.is_busy_progress_indicator);
            InitFab();
            InitRecycler();
        }

        private void InitFab()
        {
            _floatingActionButton = _view.FindViewById<FloatingActionButton>(Resource.Id.fetch_users_fab);
            _floatingActionButton.Click += FabOnClick;
        }

        protected virtual async void FabOnClick(object sender, EventArgs e)
        {
            await _viewModel.OnLoadUsersButtonClicked("", 25);
        }

        private void InitRecycler()
        {
            _itemsAdapter = new UsersAdapter();
            _itemsAdapter.SetOnClickListener(this);
            _itemsAdapter.UsersList = new List<User>(_viewModel.UsersList);
            _recyclerView = _view.FindViewById<RecyclerView>(Resource.Id.users_rv);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(_view.Context));
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

            new Handler(Looper.MainLooper).Post(() =>
            {
                _itemsAdapter.NotifyDataSetChanged();
            });
        }

        private void ChangeProgressBarVisibility(ViewStates viewState)
        {
            new Handler(Looper.MainLooper).Post(() =>
            {
                _isBusyProgressBar.Visibility = viewState;
            });
        }

        public void OnItemClicked(View view, int position)
        {
            Debug.WriteLine($"Position {position} clicked");

            Bundle bundle = new Bundle();
            string userFirstName = _viewModel.UsersList[position].Name.First;
            bundle.PutString("person", userFirstName);
            Navigation.FindNavController(view).Navigate(Resource.Id.nav_person, bundle);
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _viewModel.UsersList.CollectionChanged -= UsersViewModelCollectionChanged;
            _viewModel.PropertyChanged -= UsersViewModelPropertyChanged;
            _floatingActionButton.Click -= FabOnClick;
        }
    }
}
