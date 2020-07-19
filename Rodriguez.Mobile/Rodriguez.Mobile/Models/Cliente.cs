using System;
using System.Collections.Generic;
using System.Text;

namespace Rodriguez.Mobile.Models
{
    public class Cliente
    {
        public string Id { get; set; }
        public int ClienteId { get; set; }
        public string NombreCompleto { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Cedula { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }


        public string FormattedCelular
        {
            get => String.Format("{0:###-###-####}", Convert.ToInt64(Celular));
            set { }
        }
    }
}
