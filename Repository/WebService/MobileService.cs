﻿using Maui.Android.MVVM.App.Platforms.Android.Models;
using Refit;
using System.Diagnostics;

namespace Maui.Android.MVVM.App.Platforms.Android.Repository.WebService
{
    public class MobileService : IMobileService
    {
        private readonly IApi _mobileApi;

        private static MobileService instance = null;

        private MobileService()
        {
            _mobileApi = RestService.For<IApi>(Constants.Constants.BaseUrl);
        }

        static public MobileService GetInstance()
        {
            if (instance == null)
                instance = new MobileService();

            return instance;
        }

        public async Task<UsersResponse> GetUsers(string gender, int count)
        {
            try
            {
                var result = await _mobileApi.GetRandomUsers(gender, count);
                return result;
            }
            catch (ApiException exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }
        }

        public async Task<UsersResponse> GetUser()
        {
            try
            {
                var result = await _mobileApi.GetRandomUser();
                return result;
            }
            catch (ApiException exception)
            {
                Debug.WriteLine(exception.Message);
                return null;
            }
        }
    }
}
