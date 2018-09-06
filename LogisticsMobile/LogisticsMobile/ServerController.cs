﻿using LogisticsMobile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsMobile
{
    public class ServerController
    {
        const string Url = "http://192.168.10.10:54297/api/Equipments";
        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
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

        public async Task<List<Model>> GetModels(string category, string type)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "/" + category + "/" + type);
            return JsonConvert.DeserializeObject<List<Model>>(result);
        }

        // получаем всех друзей
        public async Task<IEnumerable<Equipment>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Equipment>>(result);
        }

        public async Task<List<string>> GetEquipment(int id)
        {
            HttpClient client = new HttpClient();
            
            string json = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }

               // добавляем одного друга
        public async Task<Equipment> Add(Equipment equipment)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(equipment),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Equipment>(
                await response.Content.ReadAsStringAsync());
        }
        // обновляем друга
        public async Task<Equipment> Update(Equipment equipment)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url + "/" + equipment.IDEquipment,
                new StringContent(
                    JsonConvert.SerializeObject(equipment),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Equipment>(
                await response.Content.ReadAsStringAsync());
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
