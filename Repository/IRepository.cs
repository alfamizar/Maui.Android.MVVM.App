using Maui.Android.MVVM.App.Platforms.Android.Models;

namespace Maui.Android.MVVM.App.Repository
{
    public interface IRepository
    {
        Task<List<User>> GetUsersList(string gender, int count);
    }
}
