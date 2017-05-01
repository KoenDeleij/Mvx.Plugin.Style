using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS {
	public partial class StoryCell : MvxTableViewCell {
		public static readonly NSString Key = new NSString("StoryCell");
		public static readonly UINib Nib;

		static StoryCell() {
			Nib = UINib.FromName("StoryCell", NSBundle.MainBundle);
		}


		private void InitializeBindings() {
			this.DelayBind(() => {
				var bindingSet = this.CreateBindingSet<StoryCell, Story>();
				bindingSet.Bind(TitleLabel).For(v => v.AttributedText).To(vm => vm.Title).WithConversion("AttributedFontText", "Regular");
				bindingSet.Bind(SubtitleLabel).For(v => v.AttributedText).To(vm => vm.Subtitle).WithConversion("AttributedFontText", "Regular");
				bindingSet.Apply();
			});
		}

		protected StoryCell(IntPtr handle) : base(handle) {
            InitializeBindings();
			//UIView v = new UIView();
			//v.BackgroundColor = UIColor.Clear;
			//this.SelectedBackgroundView = v;
		}
	}
}
