using System;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Graphics;
using Android.Text;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Color.Droid;
using Redhotminute.Mvx.Plugin.Style.Droid.Plugin;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Bindings {
	public class ButtonFontTargetBinding
		: MvxConvertingTargetBinding {

		protected Button Btn => Target as Button;

		public ButtonFontTargetBinding(Button target)
			: base(target) {
			if (target == null) {
				MvxBindingTrace.Trace(MvxTraceLevel.Error, "Error - button is null in ButtonFontTargetBinding");
				return;
			}
		}

		protected override void SetValueImpl(object target, object toSet) {
			var button = (Button)target;
			Font font = toSet as Font;
			if (font != null) {
				try {
					Typeface droidFont = DroidAssetPlugin.GetCachedFont(font,button.Context);
					button.SetTypeface(droidFont, new TypefaceStyle());
					if (font.Size != default(int)) {
						button.SetTextSize(Android.Util.ComplexUnitType.Dip, AssetPlugin.GetPlatformFontSize(font.Size));
					}
					if (font.Color != null){
						List<int[]> states = new List<int[]>();
						List<int> colors = new List<int>();

						//if there's only a disabled color
						if (font.DisabledColor != null)
						{
							if (font.SelectedColor == null) {
								states.Add(new int[] { Android.Resource.Attribute.StateEnabled });
								states.Add(new int[] { -Android.Resource.Attribute.StateEnabled });
							}

							colors.Add(font.Color.ARGB);
							colors.Add(font.DisabledColor.ARGB);
						}

						if (font.SelectedColor != null){
							if (font.DisabledColor == null) {
								states.Add(new int[] { Android.Resource.Attribute.StateActivated });
								states.Add(new int[] { -Android.Resource.Attribute.StateActivated });
							}
							colors.Add(font.SelectedColor.ARGB);
							colors.Add(font.Color.ARGB);
						}

						//if both disabled color and activated color are available
						if (font.DisabledColor != null && font.SelectedColor != null) {
							states.Add(new int[] { Android.Resource.Attribute.StateEnabled,-Android.Resource.Attribute.StateActivated });
							states.Add(new int[] { -Android.Resource.Attribute.StateEnabled,-Android.Resource.Attribute.StateActivated });
							states.Add(new int[] { Android.Resource.Attribute.StateEnabled, Android.Resource.Attribute.StateActivated });
							states.Add(new int[] { -Android.Resource.Attribute.StateEnabled, Android.Resource.Attribute.StateActivated });
						}

						if (states.Count > 0) {
							button.SetTextColor(new ColorStateList(states.ToArray(), colors.ToArray()));
						}
						else {
							button.SetTextColor(font.Color.ToAndroidColor());
						}
					}
				}
				catch (Exception e) {
					MvvmCross.Platform.Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font to Button. Check if font exists, has a size and filename");
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

