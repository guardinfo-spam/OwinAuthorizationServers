using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace ApiUse
{
    class Program
    {
        static void Main(string[] args)
        {
            string accessToken;

            //first request a token from the Authorization Server
            using (var asClient = new WebClient())
            {
                var postValues = new NameValueCollection {{"grant_type", "password"}};

                asClient.Headers.Add("Authorization", "Basic YmM4NTJmZGIzMTFkNGFkYjlkMjJkZmE4MjI2YjM5MWE6TXpBMU1ESTRNVGhsWmpVeE5EVm1OVGd6TkRNMllXSTRNelE0TnpnM05qZz0=");
                asClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                
                byte[] result = asClient.UploadValues("http://authz.test.com/oauth2/token","POST", postValues);
                string receivedResponseData = Encoding.UTF8.GetString(result);
                dynamic parsedData = JsonConvert.DeserializeObject<dynamic>(receivedResponseData);

                accessToken = parsedData.access_token.ToString();
            }

            //now request a list of roles from the resource server
            using (var rsClient = new WebClient())
            {
                rsClient.Headers.Add("Authorization", string.Concat("Bearer", " ", accessToken));
                rsClient.Headers.Add("Content-Type", "application/json");
                byte[] result = rsClient.DownloadData("http://resz.test.com/resz/GetAllRoles");
                string receivedResponseData = Encoding.UTF8.GetString(result);
                string[] parsedData = JsonConvert.DeserializeObject<string[]>(receivedResponseData);
            }
        }
    }
}
