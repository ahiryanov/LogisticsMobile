using LogisticsMobile;
using Newtonsoft.Json;
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
        //const string Url = "https://logistics.ast-telecom.ru/api/Equipments";
        const string Url = "http://192.168.10.10:54298/api/Equipments";
        // настройка клиента
        private HttpClient GetClient()
        {
            var authData = string.Format("{0}:{1}", APIKeys.Username, APIKeys.Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<List<string>> GetPositions()
        {
            try
            {
                HttpClient client = GetClient();
                string result = await client.GetStringAsync(Url + "/getpositions");
                return JsonConvert.DeserializeObject<List<string>>(result);
            }
            catch(WebException ex)
            {
                return null;
            }
        }

        public async Task<List<string>> GetHealths()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/gethealths");
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        public async Task<List<string>> GetCategories()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url+"/getcategories");
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        public async Task<List<string>> GetTypes(string category)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + category);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        public async Task<List<ModelCount>> GetModels(string category, string type)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + category + "/" + type);
            return JsonConvert.DeserializeObject<List<ModelCount>>(result);
        }

        public async Task<List<Equipment>> GetEquipments(Model model)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + model.Category + "/" + model.EquipmentType + "/" +model.IDModel);
            return JsonConvert.DeserializeObject<List<Equipment>>(result);
        }

        public async Task<List<TransferEquipment>> GetHistory(Equipment equipment)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + equipment.IDEquipment + "/history");
            return JsonConvert.DeserializeObject<List<TransferEquipment>>(result);
        }

        // добавляем одного друга
        public async Task<Equipment> Add(Equipment equipment)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url+ "/" + equipment.IDEquipment,
                new StringContent(
                    JsonConvert.SerializeObject(equipment),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Equipment>(
                await response.Content.ReadAsStringAsync());
        }
        // обновляем друга
        public async Task<Equipment> UpdateEquipment(Equipment equipment)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url + "/" + equipment.IDEquipment, new StringContent(JsonConvert.SerializeObject(equipment), Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<Equipment>(await response.Content.ReadAsStringAsync());
        }
        // удаляем друга
        public async Task<Equipment> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Equipment>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
