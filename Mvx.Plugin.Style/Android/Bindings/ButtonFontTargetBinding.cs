using System;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Graphics;
using Android.Text;
using Android.Widget;
using Microsoft.Extensions.Logging;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Logging;
using MvvmCross.Plugin.Color.Platforms.Android;
using Mvx.Plugin.Style.Droid.Plugin;
using Mvx.Plugin.Style.Models;
using Mvx.Plugin.Style.Plugin;


namespace Mvx.Plugin.Style.Droid.Bindings 
{
	public class ButtonFontTargetBinding : MvxConvertingTargetBinding {

		protected Button Btn => Target as Button;

		public ButtonFontTargetBinding(Button target)
			: base(target) 
        {
			if (target == null) {
                MvxBindingLog.Instance.Log(LogLevel.Error, "Error - button is null in ButtonFontTargetBinding");
				return;
			}
		}

		protected override void SetValueImpl(object target, object toSet) 
        {
			var button = (Button)target;
			Font font = toSet as Font;
			if (font != null) {
				try {
					Typeface droidFont = DroidAssetPlugin.GetCachedFont(font,button.Context);
					button.SetTypeface(droidFont, new TypefaceStyle());
					if (font.Size != default(int)) {
						button.SetTextSize(Android.Util.ComplexUnitType.Dip, AssetPlugin.GetPlatformFontSize(font.Size));
					}
					if (font.Color != System.Drawing.Color.Empty)
					{
						List<int[]> states = new List<int[]>();
						List<int> colors = new List<int>();

						//if there's only a disabled color
						if (font.DisabledColor != System.Drawing.Color.Empty)
						{
							if (font.SelectedColor == null) {
								states.Add(new int[] { Android.Resource.Attribute.StateEnabled });
								states.Add(new int[] { -Android.Resource.Attribute.StateEnabled });
							}

							colors.Add(font.Color.ToArgb());
							colors.Add(font.DisabledColor.ToArgb());
						}

						if (font.SelectedColor != System.Drawing.Color.Empty)
						{
							if (font.DisabledColor == null) {
								states.Add(new int[] { Android.Resource.Attribute.StateActivated });
								states.Add(new int[] { -Android.Resource.Attribute.StateActivated });
							}
							colors.Add(font.SelectedColor.ToArgb());
							colors.Add(font.Color.ToArgb());
						}

						//if both disabled color and activated color are available
						if (font.DisabledColor != System.Drawing.Color.Empty && font.SelectedColor != System.Drawing.Color.Empty) {
							states.Add(new int[] { Android.Resource.Attribute.StateEnabled,-Android.Resource.Attribute.StateActivated });
							states.Add(new int[] { -Android.Resource.Attribute.StateEnabled,-Android.Resource.Attribute.StateActivated });
							states.Add(new int[] { Android.Resource.Attribute.StateEnabled, Android.Resource.Attribute.StateActivated });
							states.Add(new int[] { -Android.Resource.Attribute.StateEnabled, Android.Resource.Attribute.StateActivated });
						}

						if (states.Count > 0) {
							button.SetTextColor(new ColorStateList(states.ToArray(), colors.ToArray()));
						}
						else {
							button.SetTextColor(font.Color.ToNativeColor());
						}
					}
				}
				catch
                { 
                    MvxBindingLog.Instance.Log(LogLevel.Error, "Failed to set font to Button. Check if font exists, has a size and filename");
				}
			}
		}

		public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
		public override Type TargetType => typeof(Font);
	}
}

