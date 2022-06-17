using Android.Runtime;
using AndroidX.Lifecycle;
using Maui.Android.MVVM.App.Platforms.Android.Models;
using Maui.Android.MVVM.App.Platfroms.Android.ViewModels;
using Maui.Android.MVVM.App.Repository;

namespace Maui.Android.MVVM.App.Platforms.Android.ViewModels
{
    public class RandomPersonsListViewModel : AndroidBaseViewModel
    {
        private readonly IRepository _repository;
        private readonly JavaList<User> _javaUsersList;
        private readonly MutableLiveData _usersList;

        public RandomPersonsListViewModel()
        {
            _repository = new WebRepository();
            _javaUsersList = new JavaList<User>();
            _usersList = new MutableLiveData();
        }

        public LiveData GetUsersList()
        {
            return _usersList;
        }

        public LiveData GetIsBusy()
        {
            return IsBusy;
        }

        public async Task OnLoadUsersButtonClicked(string gender, int count)
        {
            if ((bool)IsBusy.Value) return;

            IsBusy.PostValue(true);

            var users = await _repository.GetUsersList(gender, count);

            foreach (var user in users)
            {
                _javaUsersList.Add(user);
            }

            _usersList.PostValue(_javaUsersList);

            IsBusy.PostValue(false);
        }
    }
}
