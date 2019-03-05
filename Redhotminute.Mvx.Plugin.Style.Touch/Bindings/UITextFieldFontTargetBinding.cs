using System;
using UIKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Plugin.Color.Platforms.Ios;
using MvvmCross;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Touch.Plugin;
using Redhotminute.Mvx.Plugin.Style.Touch.Helpers;
using MvvmCross.Plugins.Color.iOS;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Bindings
{
    public class UITextFieldFontTargetBinding : BaseStyleBinding<UITextField>
	{
		public UITextFieldFontTargetBinding(UITextField target)
			: base(target)
		{
		}

		protected override void SetValueImpl(object target, object value)
		{
			var tf = (UITextField)target;
			Font font = value as Font;
			if (font != null){
				try {
					tf.Font = TouchAssetPlugin.GetCachedFont(font);
					if (font.Color != null) {
						tf.TextColor = font.Color.ToNativeColor();
					}

					if (font.Alignment != TextAlignment.None) {
						tf.TextAlignment = font.ToNativeAlignment();
					}
				}
				catch (Exception e) {
                    MvxBindingLog.Error("Failed to set font to UITextField. Check if font exists, has a size and filename, and is added to the plist");
				}
			}
		}
	}
}