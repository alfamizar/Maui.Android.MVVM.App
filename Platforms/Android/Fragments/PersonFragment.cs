using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.Lifecycle;
using Google.Android.Material.ProgressIndicator;
using Maui.Android.MVVM.App.Platforms.Android.ViewModels;
using View = Android.Views.View;

namespace Maui.Android.MVVM.App.Platfroms.Android.Fragments
{
    public class PersonFragment : Fragment
    {
        protected View _view;
        private CircularProgressIndicator _isBusyProgressBar;
        private TextView _userNameTextView;
        protected PersonViewModel _viewModel;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.fragment_person, container, false);

            _viewModel = new ViewModelProvider(this)
                .Get(Java.Lang.Class.FromType(typeof(PersonViewModel))) as PersonViewModel;

            InitViews();

            _userNameTextView.Text = Arguments.GetString("person");         

            return _view;
        }

        private void InitViews()
        {
            _isBusyProgressBar = _view.FindViewById<CircularProgressIndicator>(Resource.Id.is_busy_progress_indicator);
            _userNameTextView = _view.FindViewById<TextView>(Resource.Id.user_first_name_tv);
        }
    }
}
