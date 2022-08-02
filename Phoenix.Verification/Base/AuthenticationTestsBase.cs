﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Phoenix.Verification.Base
{
    public class AuthenticationTestsBase : ConfigurationTestsBase
    {
        protected static readonly HttpClient _client = new();
        
        public AuthenticationTestsBase()
            : base()
        {
            string phonenum = _configuration["Auth:PhoneNum"];
            string password = _configuration["Auth:Password"];

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(new { phone = phonenum, password });
            var body = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = _client.PostAsync("https://auth.askphoenix.gr/basic", body).Result;

            var token = resp.Content.ReadAsStringAsync().Result;

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }

        protected static StringContent Encode<TContent>(TContent content)
        {
            return new(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        protected static async Task<TContent?> DecodeAsync<TContent>(HttpResponseMessage responseMsg)
        {
            return JsonConvert.DeserializeObject<TContent>(await responseMsg.Content.ReadAsStringAsync());
        }

        public async Task<TContent?> PostAsync<TContent>(string requestUri, TContent content)
        {
            var resp = await _client.PostAsync(requestUri, Encode(content));
            return await DecodeAsync<TContent>(resp);
        }

        public async Task<TContent?> GetAsync<TContent>(string requestUri)
        {
            var resp = await _client.GetAsync(requestUri);
            return await DecodeAsync<TContent>(resp);
        }

        public async Task<TContent?> PutAsync<TContent>(string requestUri, TContent content)
        {
            var resp = await _client.PutAsync(requestUri, Encode(content));
            return await DecodeAsync<TContent>(resp);
        }

        public async Task<TContent?> DeleteAsync<TContent>(string requestUri)
        {
            var resp = await _client.DeleteAsync(requestUri);
            return await DecodeAsync<TContent>(resp);
        }
    }
}
