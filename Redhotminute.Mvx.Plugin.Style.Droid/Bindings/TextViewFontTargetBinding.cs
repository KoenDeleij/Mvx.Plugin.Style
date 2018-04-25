using System;
using Android.Graphics;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Color.Droid;
using Redhotminute.Mvx.Plugin.Style.Droid.Helpers;
using Redhotminute.Mvx.Plugin.Style.Droid.Plugin;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Bindings {
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
                    label.SetIncludeFontPadding(false);
					label.SetTypeface(droidFont, new TypefaceStyle());

					if (font.Size != default(int)) {
                        float fontSize = AssetPlugin.GetPlatformFontSize(font.Size);
                        label.SetTextSize(Android.Util.ComplexUnitType.Dip,fontSize);
					}

					if (font.Color != null) {
						label.SetTextColor(font.Color.ToAndroidColor());
					}

                    var lineHeight = font.LineHeight.HasValue?DroidAssetPlugin.GetPlatformLineHeight(font.Size, font.LineHeight.Value):(float)(label.TextSize*1.5f);
                    var lineSpacingMultiplier = font.LineHeightMultiplier.HasValue ? (font.LineHeightMultiplier.Value-1)*1.37f : 0;

                    label.SetLineSpacing(lineHeight, lineSpacingMultiplier);
  				
					if (font.Alignment != TextAlignment.None) {
						label.Gravity = font.ToNativeAlignment();
					}
				}
				catch (Exception e) {
					MvvmCross.Platform.Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font to Textview. Check if font exists, has a size and filename");
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

