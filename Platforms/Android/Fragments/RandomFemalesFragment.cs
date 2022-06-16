using Google.Android.Material.FloatingActionButton;
using static Maui.Android.MVVM.App.Platforms.Android.Adapters.UsersAdapter;

namespace Maui.Android.MVVM.App.Platfroms.Android.Fragments
{
    public class RandomFemalesFragment : RandomGenderFragment, IItemClickListener
    {
        override protected async void FabOnClick(object sender, EventArgs e)
        {            
                await _viewModel.OnLoadUsersButtonClicked("female", 25);
        }
    }
}
