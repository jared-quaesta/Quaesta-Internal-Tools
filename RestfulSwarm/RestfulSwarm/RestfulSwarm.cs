///////////////////////
/// Jared Malooly
/// Quaesta Instruments
/// jared.malooly@gmail.com
/// 
/// Module meant to communicate with https://bumblebee.hive.swarm.space/
/// 's API. Returns JSON strings with each call.
///////////////////////


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace QIRestfulSwarm
{
    public class RestfulSwarm
    {
        public bool signedIn = false;

        private static readonly HttpClient client = new HttpClient();
        private const string URL = "https://bumblebee.hive.swarm.space/hive";
        public RestfulSwarm()
        {

        }


        /// <summary>
        /// Asyncronous login to the swarm serve given a string username and password.
        /// </summary>
        /// <returns>True if successful login, false otherwise</returns>
        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var values = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(URL + "/login", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString.Length != 0)
            {
                dynamic res = JsonConvert.DeserializeObject(responseString);
                string token = res.Token;
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(token);
                signedIn = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Logout and invalidate auth token.
        /// </summary>
        public async void LogoutAsync()
        {
            await client.GetAsync(URL + "/logout");
            signedIn = false;
        }

        /// <summary>
        /// Get devices and data attached to the Swarm account currently logged in.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDevices()
        {
            if (!signedIn)
            {
                throw new UnauthorizedAccessException("You aren't signed in! Login before requesting this info.");
            }
            var response = await client.GetAsync(URL + "/api/v1/devices");
            var val = await response.Content.ReadAsStringAsync();
           
            return val;
        }


        public async Task<int> GetDevicesCount()
        {
            if (!signedIn)
            {
                throw new UnauthorizedAccessException("You aren't signed in! Login before requesting this info.");
            }

            var response = await client.GetAsync(URL + "/api/v1/devices/count");
            string val = await response.Content.ReadAsStringAsync();
           

            return Int32.Parse(val);
        }
        
        public async Task<string> GetMsgs()
        {
            if (!signedIn)
            {
                throw new UnauthorizedAccessException("You aren't signed in! Login before requesting this info.");
            }

            var response = await client.GetAsync(URL + "/api/v1/messages");
            string val = await response.Content.ReadAsStringAsync();
           

            return val;
        }


        public async Task<int> GetMsgCount()
        {
            if (!signedIn)
            {
                throw new UnauthorizedAccessException("You aren't signed in! Login before requesting this info.");
            }
            var response = await client.GetAsync(URL + "/api/v1/messages");
            var val = await response.Content.ReadAsStringAsync();

            dynamic jObj = JsonConvert.DeserializeObject(val);
            int count = jObj.Count;


            return count;
        }

    }
}
