using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Services;
using Xamarin.Forms;

namespace Rodriguez.Mobile.ViewModels
{
    public class AgregarProductoViewModel : BaseFodyObservable
    {
        public AgregarProductoViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Agregar = new Command(HandleAgregar);
            Cancelar = new Command(HandleCancelar);
        }

        private INavigation _navigation;
        private ListaCompraService manager = new ListaCompraService();
        public string Producto { get; set; }

        public Command Agregar { get; set; }
        public async void HandleAgregar()
        {
            if (!string.IsNullOrEmpty(Producto))
            {
                await manager.AgregarProducto(new ProductoCompra { Nombre = Producto });
                await _navigation.PopModalAsync();
            }
        }

        public Command Cancelar { get; set; }
        public async void HandleCancelar()
        {
            await _navigation.PopModalAsync();
        }

    }
}
