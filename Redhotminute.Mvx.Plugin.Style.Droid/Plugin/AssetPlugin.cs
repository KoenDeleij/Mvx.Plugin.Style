using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;

namespace Redhotminute.Mvx.Plugin.Style.Droid {
	public class DroidAssetPlugin : AssetPlugin {
		#region implemented abstract members of AssetPlugin

		public override void ConvertFontFileNameForPlatform(ref IBaseFont font) {
			//replace stripes, android doesn't like it
			font.FontPlatformName = font.FontFilename;
		}

		#endregion

		private static Dictionary<string, Typeface> _fontsCache = new Dictionary<string, Typeface>();

		public static Typeface GetCachedFont(IBaseFont font,Context context) {
			if (!_fontsCache.ContainsKey(font.Name)) {
				//TODO make the 'Fonts' folder customizable for android
				_fontsCache[font.Name] = Typeface.CreateFromAsset(context.Assets, $"Fonts/{font.FontPlatformName}");
			}

			return _fontsCache[font.Name];
		}

		public override IAssetPlugin ClearFonts() {
			_fontsCache = new Dictionary<string, Typeface>();
			return base.ClearFonts();
		}

		public static float GetPlatformLineHeight(float fontSize, float lineHeight)
		{
            //0.65 is some factor necessary to make the lineheight match iOS
            float factor = LineHeightFactor.HasValue ? (LineHeightFactor.Value * 0.6f) : FontSizeFactor;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, (lineHeight-(fontSize)) * factor, Resources.System.DisplayMetrics);
		}

	}
}
