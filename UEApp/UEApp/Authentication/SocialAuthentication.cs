#define AZURE

#if AZURE

using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

[assembly: Dependency(typeof(UEApp.SocialAuthentication))]
namespace UEApp
{
    public class SocialAuthentication : IAuthentication
    {
#if __ANDROID__

        public Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                return client.LoginAsync(Forms.Context, provider, parameters);
            }
            catch { }

            return null;
        }

        public void ClearCookies()
        {
            try
            {
                if ((int)global::Android.OS.Build.VERSION.SdkInt >= 21)
                    global::Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
            }
            catch (Exception ex)
            {
            }
        }

#else
        public Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                return client.LoginAsync(provider, null);
            }
            catch { }

            return null;
        }

        public void ClearCookies()
        {
           
        }
#endif

        public virtual async Task<bool> RefreshUser(IMobileServiceClient client)
        {
            try
            {
                var user = await client.RefreshUserAsync();

                if (user != null)
                {
                    client.CurrentUser = user;
                    Settings.AuthToken = user.MobileServiceAuthenticationToken;
                    Settings.UserId = user.UserId;
                    return true;
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine("Unable to refresh user: " + e);
            }

            return false;
        }
    }
}
#endif