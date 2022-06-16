using Maui.Android.MVVM.App.Platforms.Android.Models;
using Maui.Android.MVVM.App.Platfroms.Android.ViewModels;
using Maui.Android.MVVM.App.Repository;
using System.Collections.ObjectModel;

namespace Maui.Android.MVVM.App.Platforms.Android.ViewModels
{
    public class PersonViewModel : AndroidBaseViewModel
    {
        private readonly IRepository _repository;

        public PersonViewModel()
        {
            _repository = new WebRepository();
        }     
    }
}
