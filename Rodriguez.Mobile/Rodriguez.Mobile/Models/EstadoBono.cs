using System.Collections.Generic;

namespace Rodriguez.Mobile.Models
{
    public class EstadoBono
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Bono> Bonos { get; set; }
        public virtual ICollection<HistorialBono> Historial { get; set; }
    }
}
