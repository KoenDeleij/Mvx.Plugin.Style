using System;
using UIKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Plugins.Color.iOS;
using MvvmCross.Platform;

namespace Redhotminute.Mvx.Plugin.Style.Touch
{
	public class UIButtonFontTargetBinding : BaseStyleBinding<UIButton>
	{
		public UIButtonFontTargetBinding(UIButton target)
			: base(target)
		{
		}

		protected override void SetValueImpl(object target, object value)
		{
			var button = (UIButton)target;
			Font font = value as Font;
			if (font != null){
				try {
					button.Font = TouchAssetPlugin.GetCachedFont(font);
					if (font.Color != null) {
						button.SetTitleColor(font.Color.ToNativeColor(),UIControlState.Normal);
					}

					if (font.SelectedColor != null) {
						button.SetTitleColor(font.SelectedColor.ToNativeColor(), UIControlState.Selected);
					}

					if (font.DisabledColor != null) {
						button.SetTitleColor(font.DisabledColor.ToNativeColor(), UIControlState.Disabled);
					}
				}
				catch (Exception e) {
					MvvmCross.Platform.Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font to UIButton. Check if font exists, has a size and filename, and is added to the plist");
				}
			}
		}
	}
}