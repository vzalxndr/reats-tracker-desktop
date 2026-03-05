using ReatsTracker.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace ReatsTracker.Desktop.Services
{
    class ApiService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://reatstracker-api-dhcecehuesbdf9b0.switzerlandnorth-01.azurewebsites.net/api";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{BaseUrl}/companies");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                return JsonSerializer.Deserialize<List<Company>>(response, options) ?? new List<Company>();
            }
            catch
            {
                return new List<Company>();
            }
        }

        public async Task<Company> AddCompanyAsync(Company company)
        {
            if (!company.Website.StartsWith("http"))
            {
                company.Website = "https://" + company.Website;
            }

            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/companies", new
            {
                name = company.Name,
                website = company.Website
            });

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"ERROR ({response.StatusCode}): {errorContent}");
            }

            var createdCompany = await response.Content.ReadFromJsonAsync<Company>();
            return createdCompany;
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/companies/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"ERROR ({response.StatusCode}): {errorContent}");
            }

            return true;
        }
    }
}
