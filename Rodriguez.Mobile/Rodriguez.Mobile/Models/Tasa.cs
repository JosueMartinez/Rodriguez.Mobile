using System;
using System.Collections.Generic;
using System.Text;

namespace Rodriguez.Mobile.Models
{
    public class Tasa
    {
        public int Id { get; set; }
        public int MonedaId { get; set; }
        public double Valor { get; set; }
        public DateTime Fecha { get; set; }
        public Moneda Moneda { get; set; }
    }
}
