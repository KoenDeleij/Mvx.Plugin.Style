using System;
using UIKit;
using Redhotminute.Mvx.Plugin.Style.Touch.Plugin;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Touch.Helpers;
using MvvmCross.Plugins.Color.iOS;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Bindings
{
    public class UITextViewFontTargetBinding : BaseStyleBinding<UITextView>
	{
		public UITextViewFontTargetBinding(UITextView target)
			: base(target)
		{
		}

		protected override void SetValueImpl(object target, object value)
		{
			var tf = (UITextView)target;
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
					MvvmCross.Platform.Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font to UITextView. Check if font exists, has a size and filename, and is added to the plist");
				}
			}
		}
	}
}