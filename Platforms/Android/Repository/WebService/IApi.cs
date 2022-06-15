using Maui.Android.MVVM.App.Platforms.Android.Models;
using Refit;

namespace Maui.Android.MVVM.App.Platforms.Android.Repository.WebService
{
    public interface IApi
    {
        [Get("/api/?gender={gender}&results={count}")]
        Task<UsersResponse> GetRandomUsers(string gender, int count);

        [Get("/api/")]
        Task<UsersResponse> GetRandomUser();
    }
}
