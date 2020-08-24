using Android.Graphics;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Logging;
using Redhotminute.Mvx.Plugin.Style.Droid.Helpers;
using Redhotminute.Mvx.Plugin.Style.Droid.Plugin;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using System;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Bindings
{
    public class TextViewFontTargetBinding
		: MvxConvertingTargetBinding {

		protected TextView tv => Target as TextView;

		public TextViewFontTargetBinding(TextView target): base(target) 
        {
			if (target == null) 
            {
                MvxBindingLog.Instance.Error("Error - textview is null in TextViewFontTargetBinding");
				return;
			}
		}

		protected override void SetValueImpl(object target, object toSet) {
			var label = (TextView)target;
			Font font = toSet as Font;
			if (font != null)
            {
				try 
                {
					Typeface droidFont = DroidAssetPlugin.GetCachedFont(font,label.Context);
					label.SetTypeface(droidFont, new TypefaceStyle());

                    float fontSize;
					if (font.Size != default(int)) {
                        fontSize = AssetPlugin.GetPlatformFontSize(font.Size);
                        label.SetTextSize(Android.Util.ComplexUnitType.Dip,fontSize);
                    }else{
                        fontSize = label.TextSize;
                    }

					if (font.Color != System.Drawing.Color.Empty)
                    {
						label.SetTextColor(new Color(font.Color.R,font.Color.G,font.Color.B,font.Color.A));
					}

					var multiplierFactor = AssetPlugin.LineHeightFactor ?? 1;

					var lineHeight = DroidAssetPlugin.GetPlatformLineHeight(font.Size, font.LineHeight* multiplierFactor);
               
					var lineSpacingMultiplier = font.LineHeightMultiplier.HasValue ? font.LineHeightMultiplier.Value : 1;

					label.SetLineSpacing(lineHeight, lineSpacingMultiplier);
  				
					if (font.Alignment != TextAlignment.None) {
						label.Gravity = font.ToNativeAlignment();
					}
				}
				catch
                {
                    MvxBindingLog.Instance.Error("Failed to set font to Textview. Check if font exists, has a size and filename");
				}
			}
		}

		public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

		public override Type TargetType => typeof(Font);
	}
}

