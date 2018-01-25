using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using MvvmCross.Binding;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Plugins.Color.Droid;
using Redhotminute.Mvx.Plugin.Style.Droid.Helpers;
using Redhotminute.Mvx.Plugin.Style.Droid.Plugin;
using Redhotminute.Mvx.Plugin.Style.Helpers;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Converters {
	public class AttributedTextConverter : MvxValueConverter<string, AttributedStringBaseFontWrapper>, IMvxValueConverter {

		IAssetPlugin _assetPlugin;
		Font _extendedFont;
		Context _context;

		protected override AttributedStringBaseFontWrapper Convert (string value, Type targetType, object parameter, CultureInfo culture) {
			//TODO check if a tag on the first elemen works
			//TODO clean up some code, add unittests
			try {
				string fontName = parameter.ToString();
				if (string.IsNullOrWhiteSpace(fontName)) {
					return new AttributedStringBaseFontWrapper() { SpannableString = new SpannableString(value)};
				}

				if (_assetPlugin == null) {
					_assetPlugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();
				}
				if (_context == null) {
					_context = MvvmCross.Platform.Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.BaseContext;
				}

				_extendedFont = _assetPlugin.GetFontByName(fontName) as Font;

				string cleanText = string.Empty;
				List<FontIndexPair> blockIndexes = AttributedFontHelper.GetFontTextBlocks(value, fontName, _assetPlugin, out cleanText);

				SpannableString converted = new SpannableString(cleanText);

				foreach (FontIndexPair block in blockIndexes) {
					SetAttributed(converted, block,_extendedFont);
				}

				return new AttributedStringBaseFontWrapper() { SpannableString = converted , Font = _extendedFont};
			}
			catch (Exception e){
				MvxBindingTrace.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, e.Message);
			}
			return null;
		}
		/// <summary>
		/// Set the attributes of a part of the text
		/// </summary>
		/// <param name="converted">Converted.</param>
		/// <param name="pair">Pair.</param>
		private void SetAttributed(SpannableString converted, FontIndexPair pair, Font fallbackFont) {
            //get the font by tags

            FontTag fontTag = null;
            var taggedFont = _assetPlugin.GetFontByTag(fallbackFont.Name, pair.FontTag.OriginalFontName,out fontTag);

			if (taggedFont != null) {
				SetFont(converted, taggedFont, pair.StartIndex, pair.EndIndex);
			}
			else if(fallbackFont!= null) {
				SetFont(converted, fallbackFont, pair.StartIndex, pair.EndIndex);
			}
		}

		private void SetFont(SpannableString converted, IBaseFont font,int startIndex,int endIndex) {
					//set the text color
			if (font.Color != null) {
				converted.SetSpan(new ForegroundColorSpan(font.Color.ToAndroidColor()), startIndex, endIndex, SpanTypes.ExclusiveInclusive);
			}
			//set allignment
			if (font is Font) {
				Font taggedExtendedFont = font as Font;

				if (taggedExtendedFont.Alignment != TextAlignment.None) {
					Layout.Alignment alignment = taggedExtendedFont.Alignment == TextAlignment.Center ? Layout.Alignment.AlignCenter : Layout.Alignment.AlignNormal;
					converted.SetSpan(new AlignmentSpanStandard(alignment), startIndex, endIndex, SpanTypes.ExclusiveInclusive);
				}
			}

			if (_extendedFont != null) {
				//calculate the relative size to the regular font
				converted.SetSpan(new RelativeSizeSpan((float)font.Size / (float)_extendedFont.Size), startIndex, endIndex, SpanTypes.ExclusiveInclusive);
			}
			//set the custom typeface
			converted.SetSpan(new CustomTypefaceSpan("sans-serif", DroidAssetPlugin.GetCachedFont(font, _context)), startIndex, endIndex, SpanTypes.ExclusiveInclusive);

		}
	}

}
