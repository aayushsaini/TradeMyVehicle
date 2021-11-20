using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TradeMyVehicle.Models;
using Xamarin.Essentials;

namespace TradeMyVehicle.Services
{
    public static class ApiService
    {
        //Service Layer for various APIs
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            //Using register model in order to work with the API
            var registerModel = new RegisterModel() 
            {
                Name = name,
                Email = email,
                Password = password
            };

            //set up http client
            var httpClient = new HttpClient();

            //Serializing the data
            var jsonData = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            //Check Post request response from server
            var response = await httpClient.PostAsync("https://trademycar.azurewebsites.net/api/accounts/register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var loginModel = new LoginModel()
            {
                Email = email,
                Password = password
            };

            //set up http client
            var httpClient = new HttpClient();

            //Serializing the data
            var jsonData = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Check Post request response from server
            var response = await httpClient.PostAsync("https://trademycar.azurewebsites.net/api/accounts/login", content);
            if (!response.IsSuccessStatusCode) return false;

            //Deserializing the response object into the Token Model form
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResponse);

            //Storing user login Access Token in App's isolated storage using preferences
            Preferences.Set("AccessToken", result.access_token);
            return true;
        }

        public static async Task<bool> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var changePasswordModel = new ChangePasswordModel()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            };

            //set up http client
            var httpClient = new HttpClient();

            //Serializing the data
            var jsonData = JsonConvert.SerializeObject(changePasswordModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));

            //Check Post request response from server
            var response = await httpClient.PostAsync("https://trademycar.azurewebsites.net/api/accounts/ChangePassword", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> EditPhoneNumber(string phoneNumber)
        {
            //set up http client
            var httpClient = new HttpClient();

            var content = new StringContent($"Number={phoneNumber}", Encoding.UTF8, "application/x-www-form-urlencoded");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));

            //Check Post request response from server
            var response = await httpClient.PostAsync("https://trademycar.azurewebsites.net/api/accounts/EditPhoneNumber", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> EditUserProfile(byte[] imageArray)
        {
            //set up http client
            var httpClient = new HttpClient();

            //Serializing the data
            var jsonData = JsonConvert.SerializeObject(imageArray);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));

            //Check Post request response from server
            var response = await httpClient.PostAsync("https://trademycar.azurewebsites.net/api/accounts/EditUserProfile ", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<UserImageModel> GetUserProfileImage()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));
            var response = await httpClient.GetStringAsync("https://trademycar.azurewebsites.net/api/accounts/UserProfileImage");
            return JsonConvert.DeserializeObject<UserImageModel>(response);
        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));
            var response = await httpClient.GetStringAsync("https://trademycar.azurewebsites.net/api/categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<bool> AddImage(int vehicleId,byte[] imageArray)
        {
            var vehicleImage = new VehicleImage()
            {
                VehicleId = vehicleId,
                ImageArray = imageArray
            };

            //set up http client
            var httpClient = new HttpClient();

            //Serializing the data
            var jsonData = JsonConvert.SerializeObject(vehicleImage);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));

            //Check Post request response from server
            var response = await httpClient.PostAsync("https://trademycar.azurewebsites.net/api/Images", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<List<Category>> GetVehicleDetail(int vehicleId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));
            var response = await httpClient.GetStringAsync($"https://trademycar.azurewebsites.net/api/Vehicles/VehicleDetails?id={vehicleId}");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<List<VehicleByCategory>> GetVehicleByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));
            var response = await httpClient.GetStringAsync($"https://trademycar.azurewebsites.net/api/Vehicles?categoryId={categoryId}");
            return JsonConvert.DeserializeObject<List<VehicleByCategory>>(response);
        }
        public static async Task<List<SearchVehicle>> SearchVehicle(string query)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));
            var response = await httpClient.GetStringAsync($"https://trademycar.azurewebsites.net/api/Vehicles/SearchVehicles?search={query}");
            return JsonConvert.DeserializeObject<List<SearchVehicle>>(response);
        }

        public static async Task<VehicleResponse> AddVehicle(Vehicle vehicle)
        {
            //set up http client
            var httpClient = new HttpClient();

            //Serializing the data
            var jsonData = JsonConvert.SerializeObject(vehicle);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));

            //Check Post request response from server
            var response = await httpClient.PostAsync("https://trademycar.azurewebsites.net/api/Vehicles", content);

            //Deseralize the JSON Response
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VehicleResponse>(jsonResult);
        }

        public static async Task<List<HotAndNewAd>> GetHotAndNewAds()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));
            var response = await httpClient.GetStringAsync($"https://trademycar.azurewebsites.net/api/Vehicles/HotAndNewAds");
            return JsonConvert.DeserializeObject<List<HotAndNewAd>>(response);
        }

        public static async Task<List<MyAd>> GetMyAds()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("AccessToken", ""));
            var response = await httpClient.GetStringAsync($"https://trademycar.azurewebsites.net/api/Vehicles/MyAds");
            return JsonConvert.DeserializeObject<List<MyAd>>(response);
        }
    }
}
