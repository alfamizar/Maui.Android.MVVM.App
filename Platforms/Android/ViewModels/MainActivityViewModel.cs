using Maui.Android.MVVM.App.Platforms.Android.Models;
using Maui.Android.MVVM.App.Platforms.Android.Repository;
using System.Collections.ObjectModel;

namespace Maui.Android.MVVM.App.Platforms.Android.ViewModels
{
    public class MainActivityUsersViewModel : AndroidBaseViewModel
    {
        private readonly IRepository _repository;
        public ObservableCollection<User> UsersList { get; private set; }

        public MainActivityUsersViewModel()
        {
            _repository = new WebRepository();
            UsersList = new ObservableCollection<User>();
        }

        public async Task FetchUsersList(string gender, int count)
        {
            if (IsBusy) return;

            IsBusy = true;

            var users = await _repository.GetUsersList(gender, count);

            foreach (var user in users)
            {
                UsersList.Add(user);
            }

            IsBusy = false;
        }
    }
}
