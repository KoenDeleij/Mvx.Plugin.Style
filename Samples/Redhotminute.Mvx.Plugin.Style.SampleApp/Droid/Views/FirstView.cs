using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.Droid.Views
{
    [Activity]
    public class FirstView : MvxAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FirstView); 
        }
    }
}