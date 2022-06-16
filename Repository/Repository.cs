using Maui.Android.MVVM.App.Platforms.Android.Models;
using Maui.Android.MVVM.App.Platforms.Android.Repository.WebService;

namespace Maui.Android.MVVM.App.Repository
{
    public class WebRepository : IRepository
    {
        private readonly IMobileService _mobileService;

        public WebRepository()
        {
            _mobileService = MobileService.GetInstance();
        }

        public async Task<List<User>> GetUsersList(string gender, int count)
        {
            var response = await _mobileService.GetUsers(gender, count);
            var users = response.Users;
            return (List<User>)users;
        }
    }
}


