using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Services;
using Rodriguez.Mobile.Views.Compras;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rodriguez.Mobile.ViewModels
{
    public class ComprasViewModel : BaseFodyObservable
    {
        public static ListaCompraService service = new ListaCompraService();
        private INavigation _navigation;

        public ComprasViewModel(INavigation navigation)
        {
            _navigation = navigation;
            GetListaCompraGrupos().ContinueWith(t =>
            {
                ListaCompraGrupos = t.Result;
            });
            Delete = new Command<ProductoCompra>(HandleDelete);
            CambioComprado = new Command<ProductoCompra>(HandleCambioComprado);
            AgregarProducto = new Command(HandleAgregarProducto);
        }

        public ILookup<string, ProductoCompra> ListaCompraGrupos { get; set; }

        public async Task RefrescarLista()
        {
            ListaCompraGrupos = await GetListaCompraGrupos();
        }

        public string Title => "Lista de Compras";

        private async Task<ILookup<string, ProductoCompra>> GetListaCompraGrupos()
        {
            return (await service.GetList()).OrderBy(t => t.Comprado).ToLookup(t => t.Comprado ? "Comprado" : "Pendiente");
        }

        public Command<ProductoCompra> Delete { get; set; }
        public async void HandleDelete(ProductoCompra item)
        {
            await service.DeleteProducto(item);
            //update displayed list
            ListaCompraGrupos = await GetListaCompraGrupos();
        }

        public Command<ProductoCompra> CambioComprado { get; set; }
        public async void HandleCambioComprado(ProductoCompra item)
        {
            await service.ChangeProductoComprado(item);
            //update displayed list
            ListaCompraGrupos = await GetListaCompraGrupos();
        }

        public Command AgregarProducto { get; set; }
        public async void HandleAgregarProducto()
        {
            await _navigation.PushModalAsync(new AgregarProducto());
        }
    }
}
