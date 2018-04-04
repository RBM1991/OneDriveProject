using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using OneDriveProject.Logic.Helpers;
using Newtonsoft.Json;
using OneDriveProject.Data.Models;
using OneDriveProject.Logic.Interfaces;

namespace OneDriveProject.Logic.Controllers
{
    public class GraphController : IGraphController
    {
        private static string ClientId = "fb48b449-d992-40e6-9d61-7a516c1e3b61";
        private PublicClientApplication _clientApp = new PublicClientApplication(ClientId, "https://login.microsoftonline.com/common", TokenCacheHelper.GetUserCache());
        static string graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";
        static string myFiles = "https://graph.microsoft.com/v1.0/me/drive/root/children";
        static string[] scopes = new string[] { "Files.ReadWrite.All" };


        public PublicClientApplication PublicClientApp
        {
            get
            {
                return _clientApp;
            }
        }

        //Acquires the user token
        public async void SignIn()
        {

            AuthenticationResult authResult = null;
            var app = PublicClientApp;
            
            try
            {
                //Tries to acquire the token without the user having to log in
                authResult = await app.AcquireTokenSilentAsync(scopes, app.Users.FirstOrDefault());
            }
            catch (MsalUiRequiredException ex)
            {
                //Indicates you need to call AcquireTokenAsync to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    authResult = await app.AcquireTokenAsync(scopes);
                }
                catch (MsalException msalex)
                {
                    Console.WriteLine($"Error Acquiring Token:{System.Environment.NewLine}{msalex}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}");
                return;
            }

            if (authResult != null)
            {

                //
                var model = await GetHttpContentWithToken(graphAPIEndpoint, authResult.AccessToken);

                if(model != null)
                {
                    Console.WriteLine($"\nAuthentication success. \n\nName: { model.DisplayName }\nUsername: { model.UserPrincipalName }");
                }
            }
        }

        //Removes the user 
        public void SignOut()
        {
            if (PublicClientApp.Users.Any())
            {
                try
                {
                    PublicClientApp.Remove(PublicClientApp.Users.FirstOrDefault());
                }
                catch (MsalException ex)
                {
                    Console.WriteLine($"Error signing-out user: {ex.Message}");
                }
            }
        }

        //Sends http get request and deserializes the response
        public async Task<OneDriveUser> GetHttpContentWithToken(string url, string token)
        {           
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                System.Net.Http.HttpResponseMessage response;
                try
                {
                    var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    response = await httpClient.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<OneDriveUser>(content);

                    return model;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("GetHttpContentWithToken failed with exception: " + ex.Message);
                    return null;
                }
            }           
        }
    }

}
