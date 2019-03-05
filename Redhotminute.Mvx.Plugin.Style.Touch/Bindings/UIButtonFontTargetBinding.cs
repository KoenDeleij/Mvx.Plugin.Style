using System;
using UIKit;
using MvvmCross.Plugin.Color.Platforms.Ios;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Touch.Plugin;
using MvvmCross.Binding;
using MvvmCross.Logging;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Bindings
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
					MvxBindingLog.Instance.Error("Failed to set font to UIButton. Check if font exists, has a size and filename, and is added to the plist");
				}
			}
		}
	}
}