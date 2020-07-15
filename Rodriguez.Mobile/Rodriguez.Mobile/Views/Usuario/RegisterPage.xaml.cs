using Newtonsoft.Json;
using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rodriguez.Mobile.Views.Usuario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        async void CrearCuenta(object sender, System.EventArgs e)
        {
            await IsRunning((true));
            var c = new Cliente()
            {
                Usuario = Usuario.Text,
                NombreCompleto = NombreCompleto.Text,
                Password = Contrasena.Text,
                ConfirmPassword = ConfirmarContrasena.Text,
                Cedula = Cedula.Text,
                Celular = celular.Text,
                Email = Email.Text
            };

            if (ValidarCliente(c))
            {
                HttpClient httpClient = new HttpClient();
                var response = httpClient.PostAsync(AppSettingsManager.Settings["BaseUrl"] + "account/registerClient",
                                                    new StringContent(
                                                        JsonConvert.SerializeObject(c),
                                                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Exito", "El usuario ha sido creado. Por favor inicie sesión", "Ok");

                }
                else
                {

                    await DisplayAlert("Error", "Ha ocurrido un error.  Por favor intente más tarde", "Ok");
                }

                await IsRunning(false);
                await Navigation.PopModalAsync();

            }

            await IsRunning(false);
        }

        async void GoToLogin_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private bool ValidarCliente(Cliente c)
        {
            if (string.IsNullOrEmpty(c.Usuario) || string.IsNullOrEmpty(c.NombreCompleto) ||
                string.IsNullOrEmpty(c.Password) || string.IsNullOrEmpty(c.ConfirmPassword) ||
                string.IsNullOrEmpty(c.Cedula) || string.IsNullOrEmpty(c.Cedula) ||
                string.IsNullOrEmpty(c.Email))
            {
                DisplayAlert("Error", "Debe llenar todos los campos", "Ok");
                return false;
            }


            if (!c.Password.Equals(c.ConfirmPassword))
            {
                DisplayAlert("Eror", "Las contraseñas no coinciden", "Ok");
                return false;
            }

            //TODO verificar cedula

            //TODO verificar telefono

            //TODO verificar email

            return true;
        }

        private Task IsRunning(bool value)
        {
            ActivityIndicator.IsVisible = value;
            ActivityIndicator.IsRunning = value;
            return Task.FromResult<object>(null);
        }
    }
}