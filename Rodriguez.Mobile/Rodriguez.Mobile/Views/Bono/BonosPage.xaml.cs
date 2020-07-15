using Rodriguez.Mobile.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rodriguez.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BonosPage : ContentPage
    {
        public BonosPage()
        {
            InitializeComponent();
            Message.Text = AppSettingsManager.Settings["BaseUrl"];
        }
    }
}