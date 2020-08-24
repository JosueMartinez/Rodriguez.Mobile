using System;

namespace Rodriguez.Mobile.Models
{
    public class HistorialBono
    {
		public int Id { get; set; }
		public int BonoId { get; set; }
		public int EstadoBonoId { get; set; }
		public DateTime FechaEntradaEstado { get; set; }
		public DateTime? FechaSalidaEstado { get; set; }
		public virtual Bono Bono { get; set; }
		public virtual EstadoBono EstadoBono { get; set; }
	}
}
