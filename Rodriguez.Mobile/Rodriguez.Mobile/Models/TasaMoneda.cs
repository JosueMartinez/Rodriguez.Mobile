using System;

namespace Rodriguez.Mobile.Models
{
    public class TasaMoneda
    {
            public int Id { get; set; }
            public double Valor { get; set; }
            public DateTime Fecha { get; set; }
            public string Simbolo { get; set; }
            public string Moneda { get; set; }
            public int MonedaId { get; set; }
    }
}