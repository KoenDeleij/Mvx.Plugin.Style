using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS.Views
{
    public partial class FirstView : MvxViewController
    {
        public FirstView() : base("FirstView", null)
        {
        }

		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);

			if (NavigationController != null) {
				this.NavigationController.NavigationBarHidden = true;
			}
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
			set.Bind(HeaderLabel).For(v => v.AttributedText).To(vm => vm.Header).WithConversion("AttributedFontText", "H1");
			set.Bind(ContentLabel).For(v => v.AttributedText).To(vm => vm.Paragraph1).WithConversion("AttributedFontText", "RegularFont");

			set.Bind(ScrollView).For(v => v.BackgroundColor).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Secondairy");
            set.Apply();
        }
    }
}
