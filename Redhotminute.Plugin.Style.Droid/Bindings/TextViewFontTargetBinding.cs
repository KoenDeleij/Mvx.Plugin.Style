using System;
using Android.Graphics;
using Android.Text;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Color.Droid;

namespace Redhotminute.Plugin.Style.Droid {
	public class TextViewFontTargetBinding
		: MvxConvertingTargetBinding {

		protected TextView tv => Target as TextView;

		public TextViewFontTargetBinding(TextView target)
			: base(target) {
			if (target == null) {
				MvxBindingTrace.Trace(MvxTraceLevel.Error, "Error - textview is null in TextViewFontTargetBinding");
				return;
			}
		}

		protected override void SetValueImpl(object target, object toSet) {
			var label = (TextView)target;
			Font font = toSet as Font;
			if (font != null) {
				try {
					Typeface droidFont = DroidAssetPlugin.GetCachedFont(font,label.Context);
					label.SetTypeface(droidFont, new TypefaceStyle());

					if (font.Size != default(int)) {
						label.SetTextSize(Android.Util.ComplexUnitType.Dip, AssetPlugin.GetPlatformFontSize(font.Size));
					}

					if (font.Color != null) {
						label.SetTextColor(font.Color.ToAndroidColor());
					}

					if (font.LineHeight != 0) {
						var newLineHeight = DroidAssetPlugin.GetPlatformLineHeight(font.Size, font.LineHeight);
						label.SetLineSpacing(newLineHeight,1.15f);
					}

					if (font.Alignment != TextAlignment.None) {
						label.Gravity = font.ToNativeAlignment();
					}
				}
				catch (Exception e) {
					Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font to Textview. Check if font exists, has a size and filename");
				}
			}
		}

		public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

		public override Type TargetType {
			get {
				return typeof(Font);
			}
		}
	}
}

