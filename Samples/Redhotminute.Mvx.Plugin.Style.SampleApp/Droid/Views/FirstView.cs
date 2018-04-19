using Android.App;
using Android.OS;
using Android.Text.Method;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using Redhotminute.Mvx.Plugin.Style.Droid.Helpers;
using Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.Droid.Views
{
    [Activity]
    public class FirstView : MvxAppCompatActivity
    {
        TextView _contentTextField;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FirstView); 
        }
    }
}