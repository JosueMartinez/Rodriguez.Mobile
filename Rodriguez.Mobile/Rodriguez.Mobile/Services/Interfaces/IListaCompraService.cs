using Rodriguez.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rodriguez.Mobile.Services.Interfaces
{
    public interface IListaCompraService<T>
    {
        Task<List<T>> GetList();

        Task DeleteProducto(T item);

        Task ChangeProductoComprado(ProductoCompra item);

        Task AgregarProducto(ProductoCompra item);
    }
}
