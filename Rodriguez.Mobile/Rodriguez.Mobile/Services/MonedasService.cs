using Newtonsoft.Json;
using Rodriguez.Mobile.Classes;
using Rodriguez.Mobile.Models;
using Rodriguez.Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rodriguez.Mobile.Services
{
    public class MonedasService : IMonedasService<TasaMoneda>
    {
        HttpClient Client { get; set; }

        public MonedasService()
        {
            if (Client == null)
                Client = RequestClient.GetClient();
        }

        public async Task<TasaMoneda> Get(int id)
        {
            try
            {
                var response = await Client.GetAsync(AppSettingsManager.Settings["BaseUrl"] + String.Format("monedas/{0}", id));
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TasaMoneda>(content);
                }

                return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TasaMoneda>> GetAll()
        {
            try
            {
                var response = await Client.GetAsync(AppSettingsManager.Settings["BaseUrl"] + "tasas");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var monedas = JsonConvert.DeserializeObject<List<TasaMoneda>>(content);
                    return monedas;
                }

                return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
