using Rodriguez.Mobile.Classes;
using SQLite;

namespace Rodriguez.Mobile.Models
{
    public class ProductoCompra : BaseFodyObservable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Comprado { get; set; }
    }
}
