using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Services.Interfaces;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rodriguez.Mobile.Services
{
    public class ListaCompraService : IListaCompraService<ProductoCompra>
    {
        private readonly SQLiteAsyncConnection _db;

        public ListaCompraService()
        {
            _db = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("RodriguezSQLite.db3"));
            _db.CreateTableAsync<ProductoCompra>().Wait();
        }

        private List<ProductoCompra> _listaCompra { get; set; } = new List<ProductoCompra>
        {
        };

        public Task AgregarProducto(ProductoCompra item)
        {
            return _db.InsertAsync(item);
        }

        public Task ChangeProductoComprado(ProductoCompra item)
        {
            item.Comprado = !item.Comprado;
            return _db.UpdateAsync(item);
        }

        public Task DeleteProducto(ProductoCompra item)
        {
            return _db.DeleteAsync(item);
        }

        public async Task<List<ProductoCompra>> GetList()
        {
            if (await _db.Table<ProductoCompra>().CountAsync() == 0)
            {
                await _db.InsertAllAsync(_listaCompra);
            }

            return await _db.Table<ProductoCompra>().ToListAsync();
        }
    }
}
