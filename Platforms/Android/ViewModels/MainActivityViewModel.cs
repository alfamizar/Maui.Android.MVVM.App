using AndroidX.Lifecycle;
using Maui.Android.MVVM.App.Platforms.Android.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Android.MVVM.App.Platforms.Android.ViewModels
{
    public class MainActivityViewModel : ViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<User> _usersList;

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        public MainActivityViewModel()
        {
            _usersList = new ObservableCollection<User>();
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/94.jpg",
                FirstName = "Gül",
                LastName = "Akışık"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/women/18.jpg",
                FirstName = "Gökhan",
                LastName = "Tütüncü"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/52.jpg",
                FirstName = "Emma",
                LastName = "Thomsen"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            _usersList.Add(new User
            {
                PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg",
                FirstName = "Brad",
                LastName = "Gibson"
            });
            IsBusy = false;
        }

        public ObservableCollection<User> GetUsersList()
        {
            return _usersList;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
