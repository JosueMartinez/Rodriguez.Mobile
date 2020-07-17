using Newtonsoft.Json;
using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rodriguez.Mobile.Views.Usuario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private string authorizationKey;
        HttpClient Client { get; set; }

        public LoginPage()
        {
            InitializeComponent();
        }

        async void IniciarSesion(object sender, System.EventArgs e)
        {

            await IsRunning(true);
            var usuario = txtUsuario.Text;
            var contrasena = txtContrasena.Text;

            if (String.IsNullOrEmpty(usuario) || String.IsNullOrEmpty(contrasena))
            {
                await DisplayAlert("Error", "Proporcione sus credenciales de acceso", "Ok");
                await IsRunning(false);
                return;

            }

            Client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", usuario),
                new KeyValuePair<string, string> ( "password", contrasena )
            };
            var content = new FormUrlEncodedContent(pairs);

            if (string.IsNullOrEmpty(authorizationKey))
            {
                try
                {
                    var response = await Client.PostAsync(AppSettingsManager.Settings["BaseUrl"] + "token", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        // Deserialize the JSON into a Dictionary<string, string>
                        Dictionary<string, string> tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                        Application.Current.Properties["token"] = tokenDictionary["access_token"];
                        Application.Current.Properties["IsLoggedIn"] = true;
                        Application.Current.Properties["usuario"] = usuario;

                        Client = RequestClient.GetClient();
                        var clienteResponse = Client.GetAsync(AppSettingsManager.Settings["BaseUrl"] + "clienteU/" + usuario).Result;
                        var clienteContent = clienteResponse.Content.ReadAsStringAsync().Result;
                        var cliente = JsonConvert.DeserializeObject<Cliente>(clienteContent);

                        if (cliente == null)
                        {  //no es un cliente
                            await DisplayAlert("Error", "Este cliente no existe", "Ok");
                            txtUsuario.Text = ""; txtContrasena.Text = "";   //limpiando campos
                            await IsRunning(false);
                            return;
                        }

                        //// Deserialize the JSON into a Dictionary<string, string>
                        //Dictionary<string, string> tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                        //authorizationKey = tokenDictionary["access_token"];
                        //Application.Current.Properties["IsLoggedIn"] = true;
                        //Application.Current.Properties["token"] = authorizationKey;
                        //Application.Current.Properties["usuario"] = usuario;
                        Application.Current.Properties["cliente"] = cliente;
                        Application.Current.MainPage = new MainPage();

                    }
                    else
                    {
                        await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Ok");
                        Debug.WriteLine(response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Ha ocurrido un error.  Por favor intente más tarde", "Ok");
                }


            }

            await IsRunning(false);
        }

        private async void forgottenLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PasswordRecoverPage());
        }

        private async void newAccountLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }
        private Task IsRunning(bool value)
        {
            ActivityIndicator.IsVisible = value;
            ActivityIndicator.IsRunning = value;
            return Task.FromResult<object>(null);
        }
    }
}