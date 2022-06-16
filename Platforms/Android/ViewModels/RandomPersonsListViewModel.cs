using Maui.Android.MVVM.App.Platforms.Android.Models;
using Maui.Android.MVVM.App.Platfroms.Android.ViewModels;
using Maui.Android.MVVM.App.Repository;
using System.Collections.ObjectModel;

namespace Maui.Android.MVVM.App.Platforms.Android.ViewModels
{
    public class RandomPersonsListViewModel : AndroidBaseViewModel
    {
        private readonly IRepository _repository;
        public ObservableCollection<User> UsersList { get; private set; }

        public RandomPersonsListViewModel()
        {
            _repository = new WebRepository();
            UsersList = new ObservableCollection<User>();
        }

        public async Task OnLoadUsersButtonClicked(string gender, int count)
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
