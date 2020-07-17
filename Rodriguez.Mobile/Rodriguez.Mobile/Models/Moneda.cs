using System.Collections.ObjectModel;

namespace Rodriguez.Mobile.Models
{
    public class Moneda
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Simbolo { get; set; }
        public Collection<Tasa> Tasas { get; set; }
    }
}