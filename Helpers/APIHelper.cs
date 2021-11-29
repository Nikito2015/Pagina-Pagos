using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class APIHelpers
    {
        public static async Task<HttpResponseMessage> PostAsync(String URL, String JsonParams, int timeOut = 60, IDictionary<string, string> lstHeaders = null)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.NotFound;


            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (lstHeaders != null) { lstHeaders.ToList().ForEach(z => client.DefaultRequestHeaders.Add(z.Key, z.Value)); }
                client.Timeout = TimeSpan.FromSeconds(timeOut);

                HttpContent content = new StringContent(JsonParams, Encoding.UTF8, "application/json");


                response = await client.PostAsync(new Uri(URL), content);
            }


            return response;
        }
      
    }

}
