using System;
using UIKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Plugins.Color.iOS;
using MvvmCross.Platform;
using Foundation;
using MvvmCross.Localization;

namespace Redhotminute.Plugin.Style.Touch
{
	public class UILabelAttributedFontTargetBinding : BaseStyleBinding<UILabel>
	{
		public UILabelAttributedFontTargetBinding(UILabel target)
			: base(target)
		{
		}

		protected override void SetValueImpl(object target, object value)
		{
			var label = (UILabel)target;
			if (value != null) {
				try {
					label.AttributedText = value as NSAttributedString;
				}
				catch (Exception e) {
					Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font+language to UILabel. Check if font exists, has a size and filename, and is added to the plist");
				}
			}
		}
	}
}