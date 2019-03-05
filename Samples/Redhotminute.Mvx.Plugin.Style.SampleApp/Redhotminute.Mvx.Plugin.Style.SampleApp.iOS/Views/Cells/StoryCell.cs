using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS {
	public partial class StoryCell : MvxTableViewCell ,IDynamicCell{
		public static readonly NSString Key = new NSString("StoryCell");
		public static readonly UINib Nib;

		static StoryCell() {
			Nib = UINib.FromName("StoryCell", NSBundle.MainBundle);
		}

		public void ResetBindings() {
			this.BindingContext.ClearAllBindings();
			this.InitializeBindings();
		}

		private void InitializeBindings() {
			this.DelayBind(() => {
				var bindingSet = this.CreateBindingSet<StoryCell, Story>();
				bindingSet.Bind(this).For(v => v.SelectedBackgroundColor).To(vm => vm.Empty).WithConversion("AssetColor", "Primary");
				bindingSet.Bind(TitleLabel).For(v => v.AttributedText).To(vm => vm.Title).WithConversion("AttributedFontText", "Bold");
				bindingSet.Bind(SubtitleLabel).For(v => v.AttributedText).To(vm => vm.Subtitle).WithConversion("AttributedFontText", "Italic");
				bindingSet.Apply();
			});
		}

		public UIColor SelectedBackgroundColor {
			get {
				return null;
			}set {
				UIView v = new UIView();
				v.BackgroundColor = value;
				this.SelectedBackgroundView = v;
			}
		}

		protected StoryCell(IntPtr handle) : base(handle) {
            this.BackgroundColor = UIColor.Clear;
		}
	}
}
