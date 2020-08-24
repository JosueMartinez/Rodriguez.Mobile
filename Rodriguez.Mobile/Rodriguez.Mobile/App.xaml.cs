using Xamarin.Forms;
using Rodriguez.Mobile.Services;
using Rodriguez.Mobile.Views;
using Rodriguez.Mobile.Views.Usuario;

namespace Rodriguez.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            Current = this;

            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") && (bool)Properties["IsLoggedIn"];
            var cliente = Properties.ContainsKey("cliente") ? Properties["cliente"] : null;

            if(isLoggedIn && cliente != null)
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false; // only gets set to 'true' on the LoginPage
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
