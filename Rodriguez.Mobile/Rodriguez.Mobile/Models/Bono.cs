using Newtonsoft.Json;
using Rodriguez.Mobile.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rodriguez.Mobile.Models
{
    public class Bono
    {
        public int Id { get; set; }
        public double Monto { get; set; }
        public int TasaId { get; set; }
        public int ClienteId { get; set; }
        public string NombreDestino { get; set; }
        public string ApellidoDestino { get; set; }
        public string CedulaDestino { get; set; }
        public string TelefonoDestino { get; set; }
        public DateTime FechaCompra { get; set; }
        public Cliente Cliente { get; set; }
        public Tasa Tasa { get; set; }
        public EstadoBono Estadobono { get; set; }

        public string PaypalId { get; set; }

        public ICollection<HistorialBono> HistorialBonos { get; set; }

        #region custom
        public string nombreCompleto
        {
            get { return NombreDestino + " " + ApellidoDestino; }
        }

        [JsonIgnoreAttribute]
        public string MontoStr
        {
            get { return String.Format("{0}$ {1:##,##0.00}", Tasa.Moneda.Simbolo, Monto); }
        }

        [JsonIgnoreAttribute]
        public string MontoRD
        {
            get { return String.Format("RD$ {0:##,##0.00}", Monto * Tasa.Valor); }
        }

        [JsonIgnoreAttribute]
        public string destinoCompleto
        {
            get
            {
                return nombreCompleto + "\n" +
                    Utils.formatCedula(CedulaDestino) + "\n" +
                         Utils.formaTel(TelefonoDestino);

            }
            set
            {
                destinoCompleto = value;
            }
        }

        [JsonIgnoreAttribute]
        public string MetodoPago
        {
            get
            {
                return "**** - 1756"; //TODO: obtener pago
            }
        }

        [JsonIgnoreAttribute]
        public string Estado
        {
            get { return Estadobono.Descripcion; }
        }

        #endregion
    }
}
