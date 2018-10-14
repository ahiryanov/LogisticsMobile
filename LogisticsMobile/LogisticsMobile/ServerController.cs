using LogisticsMobile;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsMobile
{
    public class ServerController
    {
        const string Url = "https://logistics.ast-telecom.ru/api/Equipments";
        const string AuthUrl = "https://logistics.ast-telecom.ru/api/Auth";
        private string authString;
        public ServerController()
        {
            var Family = CrossSettings.Current.GetValueOrDefault("Family", null);
            var Name = CrossSettings.Current.GetValueOrDefault("Name", null);
            var Password = CrossSettings.Current.GetValueOrDefault("Password", null);

            authString = string.Format("{0}:{1}", Family + " " + Name, Password);
        }

        //http клиент для всех запросов, кроме authentification
        private HttpClient GetClientWithAuth()
        {
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        //http для authentification (без данных для аутентификации)
        private HttpClient GetClientWithoutAuth()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<List<string>> GetPositions()
        {
            try
            {
                HttpClient client = GetClientWithAuth();
                string result = await client.GetStringAsync(Url + "/getpositions");
                return JsonConvert.DeserializeObject<List<string>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<string>> GetHealths()
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/gethealths");
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        public async Task<List<string>> GetCategories()
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/getcategories");
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        public async Task<List<string>> GetTypes(string category)
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/" + category);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        public async Task<List<ModelCount>> GetModels(string category, string type)
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/" + category + "/" + type);
            return JsonConvert.DeserializeObject<List<ModelCount>>(result);
        }

        public async Task<Model> GetModel(int idModel)
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/Model/" + idModel);
            return JsonConvert.DeserializeObject<Model>(result);
        }

        public async Task<List<Equipment>> GetEquipments(Model model)
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/" + model.Category + "/" + model.EquipmentType + "/" + model.IDModel);
            return JsonConvert.DeserializeObject<List<Equipment>>(result);
        }

        public async Task<List<Equipment>> GetEquipment(string idOrSerial)
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/search/isnOrSerial/" + idOrSerial);
            return JsonConvert.DeserializeObject<List<Equipment>>(result);
        }

        public async Task<List<TransferEquipment>> GetHistory(Equipment equipment)
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/" + equipment.IDEquipment + "/history");
            return JsonConvert.DeserializeObject<List<TransferEquipment>>(result);
        }

        // 
        public async Task<Equipment> AddEquipment(Equipment equipment)
        {
            HttpClient client = GetClientWithAuth();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(equipment),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Equipment>(
                await response.Content.ReadAsStringAsync());
        }
        // 
        public async Task<Equipment> UpdateEquipment(Equipment equipment)
        {
            HttpClient client = GetClientWithAuth();
            var response = await client.PutAsync(Url + "/" + equipment.IDEquipment, new StringContent(JsonConvert.SerializeObject(equipment), Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<Equipment>(await response.Content.ReadAsStringAsync());
        }
        // 
        public async Task<Equipment> DeleteEquipment(int id)
        {
            HttpClient client = GetClientWithAuth();
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Equipment>(
               await response.Content.ReadAsStringAsync());
        }

        public async Task<Manager> AuthUser(Manager user)
        {
            HttpClient client = GetClientWithoutAuth();
            var response = await client.PostAsync(AuthUrl,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Manager>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<List<Manager>> GetAllUsers()
        {
            HttpClient client = GetClientWithAuth();
            string result = await client.GetStringAsync(Url + "/Users");
            return JsonConvert.DeserializeObject<List<Manager>>(result);
        }

    }
}
