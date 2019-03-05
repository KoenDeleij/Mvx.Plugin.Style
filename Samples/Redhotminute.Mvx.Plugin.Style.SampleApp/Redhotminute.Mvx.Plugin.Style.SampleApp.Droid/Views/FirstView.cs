using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.Droid.Views
{
    [Activity]
    public class FirstView : MvxAppCompatActivity<FirstViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FirstView); 
        }
    }
}