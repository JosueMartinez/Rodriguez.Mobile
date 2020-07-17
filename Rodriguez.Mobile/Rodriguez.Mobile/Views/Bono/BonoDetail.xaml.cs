using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonoModel = Rodriguez.Mobile.Models.Bono;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rodriguez.Mobile.Views.Bono
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BonoDetail : ContentPage
    {
		public BonoDetail() { }   //Just for Previewing

		public BonoDetail(BonoModel bono)
		{
			InitializeComponent();
			inicializarComponentes(bono);
			BindingContext = bono;
			//Setting toolbar
			//ToolbarItems.Add(new ToolbarItem("Go Back", null, () =>
			//{
			//	Debug.WriteLine("Go Back");
			//}));

		}

        private void inicializarComponentes(BonoModel bono)
        {
			//emitidoADetail.Text = b.destinoCompleto;
			metodoPagoDetail.Text = "**** - 1756"; //TODO: obtener pago
												   //montoRdDetail.Text = 25.50.ToString();
		}
	}
}