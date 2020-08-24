
using Rodriguez.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rodriguez.Mobile.Views.Compras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarProducto : ContentPage
    {
        public AgregarProducto()
        {
            InitializeComponent();
            BindingContext = new AgregarProductoViewModel(Navigation);
        }
    }
}