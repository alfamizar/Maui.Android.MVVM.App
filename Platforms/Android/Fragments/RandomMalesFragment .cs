using static Maui.Android.MVVM.App.Platforms.Android.Adapters.UsersAdapter;

namespace Maui.Android.MVVM.App.Platfroms.Android.Fragments
{
    public class RandomMalesFragment : RandomGenderFragment, IItemClickListener
    {
        override protected async void FabOnClick(object sender, EventArgs e)
        {
            await _viewModel.OnLoadUsersButtonClicked("male", 25);
        }
    }
}
