using Maui.Android.MVVM.App.Platforms.Android.Models;

namespace Maui.Android.MVVM.App.Platforms.Android.Repository.WebService
{
    public interface IMobileService
    {
        Task<UsersResponse> GetUsers(string gender, int count);

        Task<UsersResponse> GetUser();
    }
}
