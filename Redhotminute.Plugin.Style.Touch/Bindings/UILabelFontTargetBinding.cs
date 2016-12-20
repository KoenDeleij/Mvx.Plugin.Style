﻿using System;
using UIKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Plugins.Color.iOS;
using MvvmCross.Platform;

namespace Redhotminute.Plugin.Style.Touch
{
	public class UILabelFontTargetBinding : BaseStyleBinding<UILabel>
	{
		public UILabelFontTargetBinding(UILabel target)
			: base(target)
		{
		}

		protected override void SetValueImpl(object target, object value)
		{
			var label = (UILabel)target;
			Font font = value as Font;
			if (font != null){
				try {
					label.Font = TouchAssetPlugin.GetCachedFont(font);
					if (font.Color != null) {
						label.TextColor = font.Color.ToNativeColor();
					}

					if (font.Alignment != TextAlignment.None) {
						label.TextAlignment = font.ToNativeAlignment();
					}
				}
				catch (Exception e) {
					Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font to UILabel. Check if font exists, has a size and filename, and is added to the plist");
				}
			}
		}
	}
}