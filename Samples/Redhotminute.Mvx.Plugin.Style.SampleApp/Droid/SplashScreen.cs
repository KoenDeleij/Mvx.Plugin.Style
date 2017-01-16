using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.Droid
{
    [Activity(
        Label = "Redhotminute.Mvx.Plugin.Style.SampleApp.Droid"
        , MainLauncher = true
        , Icon = "@mipmap/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
