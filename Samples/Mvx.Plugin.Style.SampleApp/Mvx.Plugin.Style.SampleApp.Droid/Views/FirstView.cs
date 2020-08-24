using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using Mvx.Plugin.Style.SampleApp.ViewModels;

namespace Mvx.Plugin.Style.SampleApp.Droid.Views
{
    [Activity]
    public class FirstView : MvxActivity<FirstViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FirstView); 
        }
    }
}