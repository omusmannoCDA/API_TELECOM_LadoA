using API_TELECOM_LadoA.Constants;
using API_TELECOM_LadoA.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_TELECOM_LadoA.Services
{
    public class LadoAService
    {
        private static string _Url = Constantes.Url;
        public static async Task<Clima> GetClimaAsync()
        {
            using (HttpClient _Client = new HttpClient())
            {
                var response = await _Client.GetAsync(_Url + "london");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Clima>(json);
            }

        }


    }
}
