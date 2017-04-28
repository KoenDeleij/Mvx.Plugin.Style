using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.UI;
using Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels;
using UIKit;

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
			set.Bind(ContentLabel).For(v => v.AttributedText).To(vm => vm.Paragraph1).WithConversion("AttributedFontText", "Regular");
			set.Bind(View).For(v => v.BackgroundColor).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Background");
			set.Apply();

			ParseZeplin();
        }

		public void ParseZeplin() {

		}
		/*
		public void ParseZeplin() {
			var l=MvvmCross.Platform.Mvx.Resolve<IMvxResourceLoader>();
			var res = l.GetTextResource("UIColor.swift");

			int start = res.IndexOf("extension UIColor {");
			string colors = res.Substring(res.IndexOf("extension UIColor {"), res.Length - start - 2);

			//strip whatever
			colors = colors.Replace("extension UIColor {","");
			colors = colors.Replace("class var ","");
			colors = colors.Replace(": UIColor {", "");
			colors = colors.Replace("return UIColor(", "*");
			colors = colors.Replace("(", "");
			colors = colors.Replace(")", "");
			colors = colors.Replace("}\n\n", "|");
			colors = colors.Replace("\n  ", ""); 
			colors = colors.Replace("}", "");

			var colorsBlocks = colors.Split('|');
			foreach (string colorBlock in colorsBlocks) {
				MvxColor color = new MvxColor(0, 0, 0, 1);

				var colorTitleValue = colorBlock.Split('*');

				string title = colorTitleValue[0].Trim();

				string[] colorValues = colorTitleValue[1].Split(',');

				foreach (string colorValue in colorValues) {
					//split color from value
					string[] rgbValues = colorValue.Split(':');

					string colorTitle = rgbValues[0];
					string colorRGBValue = rgbValues[1];

					float trueValue = 0;
					//could contain calculations
					if (colorRGBValue.Contains("/")) {
						string[] valls = colorRGBValue.Split('/');

						float startValue = 0;
						float finalValue = 0;
						if (float.TryParse(valls[0], out startValue)) {
							if (float.TryParse(valls[1], out finalValue)) {
								trueValue = startValue / finalValue;
							}
						}
					}
					else {
						float.TryParse(colorRGBValue, out trueValue);
					}

					switch (colorTitle) {
						case "red":color.R = (int)(255.0f / trueValue);break;
						case "green":color.G = (int)(255.0f / trueValue);break;
						case "blue":color.B = (int)(255.0f / trueValue);break;
						case "alpha":color.B = (int)(255.0f / trueValue);break;
					}
				}
			}
		}*/
    }
}
