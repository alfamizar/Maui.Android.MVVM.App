using Android.OS;
using Android.Runtime;
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
using Maui.Android.MVVM.App.Platforms.Android.ViewModels.Observers;
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

            InitViews();

            _viewModel.GetUsersList().Observe(this, new LiveDataObserver<JavaList<User>>(_itemsAdapter.SetData));
            _viewModel.GetIsBusy().Observe(this, new BoolLiveDataObserver(ChangeProgressBarVisibility));

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
            _itemsAdapter = new UsersAdapter(this);
            _recyclerView = _view.FindViewById<RecyclerView>(Resource.Id.users_rv);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(_view.Context));
            _recyclerView.SetAdapter(_itemsAdapter);
        }

        private void ChangeProgressBarVisibility(bool isBusy)
        {
            if (isBusy)
            {
                _isBusyProgressBar.Visibility = ViewStates.Visible;
            }
            else
            {
                _isBusyProgressBar.Visibility = ViewStates.Invisible;
            }
        }

        public void ListItemOnClick(View view, int position)
        {           
            Bundle bundle = new Bundle();
            string userFirstName = ((JavaList<User>)_viewModel.GetUsersList().Value)[position].Name.First;
            bundle.PutString("person", userFirstName);
            Navigation.FindNavController(view).Navigate(Resource.Id.nav_person, bundle);
            Debug.WriteLine($"Position {position} clicked");
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _viewModel.GetUsersList().RemoveObservers(this);
            _viewModel.GetIsBusy().RemoveObservers(this);
            _floatingActionButton.Click -= FabOnClick;
        }
    }
}
