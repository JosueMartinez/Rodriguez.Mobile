using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BonoModel = Rodriguez.Mobile.Models.Bono;

namespace Rodriguez.Mobile.Views.Bono
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBonoPage : ContentPage
    {
        IEnumerable<Moneda> monedas;
        readonly MonedasService monedaService;
        readonly BonosService bonosService;
        private Moneda monedaSeleccionada { get; set; }
        private Cliente Cliente { get; set; }
        private double tasaDia { get; set; }
        private double montoRD { get; set; }

        public AddBonoPage()
        {
            monedaService = new MonedasService();
            bonosService = new BonosService();
            GetMonedasAsync();
            InitializeComponent();
            BindingContext = this;
        }

        async void ComprarBono(object sender, System.EventArgs e)
        {
            await IsRunning(true);
            Cliente = (Cliente)Application.Current.Properties["cliente"];
            //btnComprar.IsEnabled = false;
            double montoBono;

            //tasa = cbMoneda.SelectedItem;
            BonoModel b = new BonoModel()
            {
                NombreDestino = txtNombreDestinatario.Text,
                ApellidoDestino = txtApellidoDestinatario.Text,
                CedulaDestino = txtCedula.Text,
                TelefonoDestino = txtCelular.Text,
                //monto = int.Parse(txtMonto.Text),
                FechaCompra = DateTime.Now
            };
            b.ClienteId = Cliente.ClienteId;

            if (txtMonto.Text != null)
            {
                double.TryParse(txtMonto.Text, out montoBono);
                b.Monto = montoBono;
            }

            if (monedaSeleccionada != null)
            {
                b.TasaId = monedaSeleccionada.Tasas.First().Id;
            }

            if (ValidarBono(b))
            {
                //var currency = monedaSeleccionada.simbolo.Equals("RD") ? "DOP" : monedaSeleccionada.simbolo.Equals("EU") ? "EUR" : monedaSeleccionada.simbolo;
                //var currency = monedaSeleccionada.Simbolo.Equals("EU") ? "EUR" : monedaSeleccionada.Simbolo;
                //var payment = (new PayPalItem("Bono Supermercado Rodríguez", (decimal)b.Monto, currency));


                //var result = await CrossPayPalManager.Current.Buy(payment, new Decimal(0));
                //if (result.Status == PayPalStatus.Cancelled)
                //{
                //    await IsRunning(false);
                //    await DisplayAlert("Cancelado", "Ha cancelado el proceso", "Ok");
                //}
                //else if (result.Status == PayPalStatus.Error)
                //{
                //    await IsRunning(false);
                //    await DisplayAlert("Error", "Ha ocurrido un error.  Intene de nuevo mas tarde", "Ok");
                //}
                //else if (result.Status == PayPalStatus.Successful)
                //{
                //    var paymentId = result.ServerResponse.Response.Id;
                //    Debug.WriteLine(result.ServerResponse.Response.Id);
                //    PayPalClient paypal = new PayPalClient();
                //    PayPalPayment paymentDetail = await paypal.getPayment(paymentId);

                //    b.PaypalId = paymentId;

                //    //TODO agregar demas propiedades del pago de paypal (estado y metodo)
                //    try
                //    {
                //        var bonoResult = bonosService.Buy(b);
                //        if (bonoResult != null)
                //        {

                //            await DisplayAlert("Exito", "Se ha comprado el bono de forma exitosa", "Ok");
                //            await Navigation.PopAsync();
                //        }
                //        else
                //        {
                //            await DisplayAlert("Error", "Ha ocurrido un error.  Intene de nuevo mas tarde", "Ok");
                //        }
                //        await IsRunning(false);
                //    }
                //    catch (Exception ex)
                //    {
                //        await IsRunning(false);
                //        Debug.WriteLine(ex.ToString());
                //        await DisplayAlert("Error", "Ha ocurrido un error.  Intene de nuevo mas tarde", "Ok");
                //    }
                //}

            }
            else
            {
                await IsRunning(false);
                await DisplayAlert("Faltan Datos", "Hay Errores en los datos introducidos", "Ok");
                Debug.WriteLine("Hay errores en los datos introducidos");
            }
        }

        private bool ValidarBono(BonoModel b)
        {
            return (b.NombreDestino != null && b.ApellidoDestino != null &&
                    Utils.IsValidCedula(b.CedulaDestino) &&
                    Utils.IsValidPhone(b.TelefonoDestino) &&
                        monedaSeleccionada != null);
        }

        private async Task GetMonedasAsync()
        {
            
            Task<IEnumerable<Moneda>> monedasTask = monedaService.GetAll();
            monedas = await monedasTask;
            IList monedasList = (IList)monedas;
            //cbMoneda.ItemsSource = monedasList;
            if (monedas != null && monedas.Count() > 0)
                monedaSeleccionada = monedas.FirstOrDefault(x => x.Simbolo.Equals("USD"));
            else
            {
                await DisplayAlert("", "No hay conexión para realizar las transacciones", "Ok");
                await Navigation.PopAsync();
            }
            //cbMoneda.SelectedItem = monedaSeleccionada;
        }

        void OnMontoChange(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            double valorNuevo = 0.00;
            double.TryParse(e.NewTextValue, out valorNuevo);
            montoRD = valorNuevo * tasaDia;
            lbMontoRD.Text = MontoRD;
        }

        async void OnMonedaChange(object sender, System.EventArgs e)
        {
            //if (cbMoneda.SelectedIndex != -1)
            //{
                //monedaSeleccionada = cbMoneda.SelectedItem as moneda;
                ////Task<tasa> tasaTask = tasaManager.GetBySimbolo(mon.simbolo);
                ////tasa = await tasaTask;
                //tasaDia = monedaSeleccionada.tasas.First().valor;
                //lbTasaDia.Text = TasaDia;
                //montoRD = double.Parse(txtMonto.Text != null ? txtMonto.Text : "0.00") * tasaDia;
                //lbMontoRD.Text = MontoRD;
            //}
        }

        public string TasaDia
        {
            get
            {
                return String.Format("Tasa del día: {0:0.00}", tasaDia);
            }
        }


        public string MontoRD
        {
            get
            {
                return String.Format("RD$ {0:#,##0.00}", montoRD);
            }
        }

        private Task IsRunning(bool value)
        {
            ActivityIndicator.IsVisible = value;
            ActivityIndicator.IsRunning = value;
            return Task.FromResult<object>(null);
        }

    }
}