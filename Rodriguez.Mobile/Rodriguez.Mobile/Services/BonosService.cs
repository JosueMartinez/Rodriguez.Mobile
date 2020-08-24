using Newtonsoft.Json;
using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rodriguez.Mobile.Services
{
    public class BonosService : IBonosService<Bono>
    {
        HttpClient Client { get; set; }
        Cliente Cliente { get; set; }

        public BonosService()
        {
            Cliente = Application.Current.Properties.ContainsKey("cliente") ? (Rodriguez.Mobile.Models.Cliente)Application.Current.Properties["cliente"] : null;

            if (Client == null)
                Client = RequestClient.GetClient();
        }

        public async Task<Bono> Buy(Bono b)
        {
            try
            {
                var response = Client.PostAsync(AppSettingsManager.Settings["BaseUrl"] + "bonos",
                                new StringContent(
                                    JsonConvert.SerializeObject(b),
                                                    Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Bono>(
                        await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.ToString());
                return Task.FromResult<Bono>(null).Result;
            }

            return Task.FromResult<Bono>(null).Result;
        }

        public async Task<ObservableCollection<Bono>> GetAll()
        {
            try
            {
                if (Client != null && Cliente != null)
                {
                    var idCliente = Cliente.ClienteId;
                    var url = String.Format("cliente/{0}/bonos", idCliente);
                    var response = await Client.GetAsync(AppSettingsManager.Settings["BaseUrl"] + url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        ObservableCollection<Bono> bonos = JsonConvert.DeserializeObject<ObservableCollection<Bono>>(content);
                        return bonos;
                    }
                }
                else
                {
                    return null;
                }


            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }

            return new ObservableCollection<Bono>();
        }
    }
}
