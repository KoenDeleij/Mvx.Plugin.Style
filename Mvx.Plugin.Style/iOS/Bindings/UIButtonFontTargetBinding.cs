using System;
using UIKit;
using Mvx.Plugin.Style.Models;
using Mvx.Plugin.Style.iOS.Plugin;
using MvvmCross.Binding;
using MvvmCross.Logging;
using MvvmCross.Plugin.Color.Platforms.Ios;
using Microsoft.Extensions.Logging;

namespace Mvx.Plugin.Style.iOS.Bindings
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
					if (font.Color != System.Drawing.Color.Empty) {
						button.SetTitleColor(font.Color.ToNativeColor(),UIControlState.Normal);
					}

					if (font.SelectedColor != System.Drawing.Color.Empty) {
						button.SetTitleColor(font.SelectedColor.ToNativeColor(), UIControlState.Selected);
					}

					if (font.DisabledColor != System.Drawing.Color.Empty) {
						button.SetTitleColor(font.DisabledColor.ToNativeColor(), UIControlState.Disabled);
					}
				}
				catch (Exception e) {
					MvxBindingLog.Instance.Log(LogLevel.Error, "Failed to set font to UIButton. Check if font exists, has a size and filename, and is added to the plist");
				}
			}
		}
	}
}