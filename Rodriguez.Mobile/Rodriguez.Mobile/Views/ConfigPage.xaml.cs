using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Views.Bono;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rodriguez.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigPage : ContentPage
    {
        Cliente Cliente { get; set; }

        public ConfigPage()
        {
            InitializeComponent();

            //geting info
            if (Application.Current.Properties.ContainsKey("cliente"))
            {
                GetUserData();
                BindingContext = Cliente;
            }
        }

        public void GetUserData()
        {

            Cliente = (Cliente)Application.Current.Properties["cliente"];
        }

        void addBono(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddBonoPage());
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["IsLoggedIn"] = null;
            Application.Current.Properties["token"] = null;
            Application.Current.Properties["usuario"] = null;
            Application.Current.Properties["cliente"] = null;
            //App.Current.Logout();
        }

        private void Fb_Tapped(object sender, EventArgs e)
        {
            Launcher.TryOpenAsync(new Uri(AppSettingsManager.Settings["FBProfile"]));
        }
        
        private void Twitter_Tapped(object sender, EventArgs e)
        {
            Launcher.TryOpenAsync(new Uri(AppSettingsManager.Settings["TwitterProfile"]));
        }
    }
}