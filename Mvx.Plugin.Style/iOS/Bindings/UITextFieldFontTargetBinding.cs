using System;
using UIKit;
using MvvmCross.Binding;
using Mvx.Plugin.Style.Models;
using Mvx.Plugin.Style.iOS.Plugin;
using Mvx.Plugin.Style.iOS.Helpers;
using MvvmCross.Plugin.Color.Platforms.Ios;

namespace Mvx.Plugin.Style.iOS.Bindings
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
					if (font.Color != System.Drawing.Color.Empty) {
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