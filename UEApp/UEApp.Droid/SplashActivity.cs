using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;

namespace UEApp.Droid
{
    [Activity(Label = "campusLoop", MainLauncher = true, NoHistory = true, Theme = "@style/MyTheme.Splash",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}