using Newtonsoft.Json;
using Rodriguez.Mobile.Classes;
using System;

namespace Rodriguez.Mobile.Models
{
    public class Bono
    {
        public int Id { get; set; }
        public string Destinatario { get; set; }
        public string Remitente { get; set; }
        public string CedulaRemitente { get; set; }
        public string telefonoRemitente { get; set; }
        public string CedulaDestino { get; set; }
        public string TelefonoDestino { get; set; }
        public DateTime FechaCompra { get; set; }
        public double Monto { get; set; }
        public string SimboloMonedaOriginal { get; set; }
        public string EstadoBono { get; set; }
        public double Tasa { get; set; }




        #region custom

        public int TasaId { get; set; }
        public int ClienteId { get; set; }
        public string NombreDestino { get; set; }
        public string ApellidoDestino { get; set; }

        public string PaypalId { get; set; }

        [JsonIgnoreAttribute]
        public string MontoStr
        {
            get { return String.Format("{0}$ {1:##,##0.00}", SimboloMonedaOriginal, Monto); }
        }

        [JsonIgnoreAttribute]
        public string MontoRD
        {
            get { return String.Format("RD$ {0:##,##0.00}", Monto * Tasa); }
        }

        [JsonIgnoreAttribute]
        public string DestinoCompleto
        {
            get
            {
                return Destinatario + "\n" +
                    Utils.formatCedula(CedulaDestino) + "\n" +
                         Utils.formaTel(TelefonoDestino);

            }
            set
            {
                DestinoCompleto = value;
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

        #endregion
    }
}
