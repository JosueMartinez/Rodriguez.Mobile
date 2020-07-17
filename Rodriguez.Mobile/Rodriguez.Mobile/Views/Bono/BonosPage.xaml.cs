using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Services;
using Rodriguez.Mobile.Views.Bono;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BonoModel = Rodriguez.Mobile.Models.Bono;

namespace Rodriguez.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BonosPage : ContentPage
    {
        BonosService service { get; set; }
        ObservableCollection<BonoModel> BonosLista { get; set; }

        public BonosPage()
        {
            service = new BonosService();
            this.Appearing += async (object sender, EventArgs e) =>
            {
                await RefreshData();
            };

            InitializeComponent();
        }

        private async Task RefreshData()
        {
            this.IsBusy = true;
            BonosLista = await service.GetAll();  //obtaining bonos from Server

            if (BonosLista != null)
            {
                if (BonosLista.Any())
                {
                    BonosList.ItemsSource = BonosLista;
                }
                else
                {
                    BonosList.IsVisible = false;
                    BonosListMessage.IsVisible = true;
                }
            }
            else
            {
                await DisplayAlert("Error!", "Se ha producido un error en la conexión", "OK");
            }

            this.IsBusy = false;
        }

        async void AddBono(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBonoPage());
        }

        async void ViewDetails(object sender, ItemTappedEventArgs e)
        {
            BonoModel b = (BonoModel)e.Item;
            await Navigation.PushAsync(new BonoDetail(b));
        }
    }
}