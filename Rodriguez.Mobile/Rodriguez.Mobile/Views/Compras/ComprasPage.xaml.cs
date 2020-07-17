using Rodriguez.Mobile.ViewModels;
using Rodriguez.Mobile.Views.Compras;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rodriguez.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComprasPage : ContentPage
    {
        public ComprasPage()
        {
            InitializeComponent();
            BindingContext = new ComprasViewModel(Navigation);
        }

        private void AgregarProducto(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AgregarProducto());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as ComprasViewModel).RefrescarLista();
        }
    }
}