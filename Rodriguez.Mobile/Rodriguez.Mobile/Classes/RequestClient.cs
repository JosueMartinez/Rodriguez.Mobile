using Rodriguez.Mobile.Models;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace Rodriguez.Mobile.Classes
{
    public static class RequestClient
    {
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AppSettingsManager.Settings["BaseUrl"]);
            if (Application.Current.Properties.ContainsKey("token"))
            {
                var authorizationKey = Convert.ToString(Application.Current.Properties["token"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorizationKey);
                return client;
            }
            return null;
        }
    }
}
