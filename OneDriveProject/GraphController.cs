using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace OneDriveProject
{
    class GraphController
    {
        private static string ClientId = "fb48b449-d992-40e6-9d61-7a516c1e3b61";
        private static PublicClientApplication _clientApp = new PublicClientApplication(ClientId, "https://login.microsoftonline.com/common", TokenCacheHelper.GetUserCache());
        static string graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";
        static string myFiles = "https://graph.microsoft.com/v1.0/me/drive/root/children";
        static string[] scopes = new string[] { "Files.ReadWrite.All" };
        //static AuthenticationResult authResult = null;

        public static PublicClientApplication PublicClientApp
        {
            get
            {
                return _clientApp;
            }
        }

        public static async void SignIn()
        {

            AuthenticationResult authResult = null;
            var app = PublicClientApp;
            
            try
            {
                authResult = await app.AcquireTokenSilentAsync(scopes, app.Users.FirstOrDefault());
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilentAsync. 
                // This indicates you need to call AcquireTokenAsync to acquire a token
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
                Console.WriteLine(await GetHttpContentWithToken(graphAPIEndpoint, authResult.AccessToken));
                //Console.WriteLine(await GetHttpContentWithToken());
                //DisplayBasicTokenInfo(authResult);
                //this.SignOutButton.Visibility = Visibility.Visible;
                //DisplayBasicTokenInfo(authResult);
            }
        }

        public static void SignOut()
        {
            if (PublicClientApp.Users.Any())
            {
                try
                {
                    PublicClientApp.Remove(PublicClientApp.Users.FirstOrDefault());
                    //this.ResultText.Text = "User has signed-out";
                    //this.CallGraphButton.Visibility = Visibility.Visible;
                    //this.SignOutButton.Visibility = Visibility.Collapsed;
                    //Console.WriteLine("Signed out");
                }
                catch (MsalException ex)
                {
                    Console.WriteLine($"Error signing-out user: {ex.Message}");
                }
            }
        }

        public static async Task<string> GetHttpContentWithToken(string url, string token)
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                return JsonHelper.FormatJson(content);
                //return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static async Task getMyFilesAsync()
        {


            //Console.WriteLine(await GetHttpContentWithToken(myFiles, authResult.AccessToken));

        }

        public static void DisplayBasicTokenInfo(AuthenticationResult authResult)
        {
            //TokenInfoText.Text = "";
            if (authResult != null)
            {
                Console.WriteLine($"Name: {authResult.User.Name}");
                Console.WriteLine($"Username: {authResult.User.DisplayableId}");
            }
        }



        /*public static class TokenCacheHelper
        {

            /// <summary>
            /// Get the user token cache
            /// </summary>
            /// <returns></returns>
            public static TokenCache GetUserCache()
            {
                if (usertokenCache == null)
                {
                    usertokenCache = new TokenCache();
                    usertokenCache.SetBeforeAccess(BeforeAccessNotification);
                    usertokenCache.SetAfterAccess(AfterAccessNotification);
                }
                return usertokenCache;
            }

            static TokenCache usertokenCache;

            /// <summary>
            /// Path to the token cache
            /// </summary>
            public static string CacheFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + "msalcache.txt";

            private static readonly object FileLock = new object();

            public static void BeforeAccessNotification(TokenCacheNotificationArgs args)
            {
                lock (FileLock)
                {
                    args.TokenCache.Deserialize(File.Exists(CacheFilePath)
                        ? File.ReadAllBytes(CacheFilePath)
                        : null);
                }
            }

            public static void AfterAccessNotification(TokenCacheNotificationArgs args)
            {
                // if the access operation resulted in a cache update
                if (args.TokenCache.HasStateChanged)
                {
                    lock (FileLock)
                    {
                        // reflect changesgs in the persistent store
                        File.WriteAllBytes(CacheFilePath, args.TokenCache.Serialize());
                        // once the write operationtakes place restore the HasStateChanged bit to filse
                        args.TokenCache.HasStateChanged = false;
                    }
                }
            }
        }*/
    }

}
