using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using MvvmCross.Binding;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Plugins.Color.Droid;

namespace Redhotminute.Mvx.Plugin.Style.Droid {
	public class AttributedTextConverter : MvxValueConverter<string, SpannableString>, IMvxValueConverter {

		IAssetPlugin _assetPlugin;
		Font _extendedFont;
		Context _context;

		protected override SpannableString Convert (string value, Type targetType, object parameter, CultureInfo culture) {
			//TODO check if a tag on the first elemen works
			//TODO clean up some code, add unittests
			//TODO change the binding so the local:Font binding is not necessary
			try {
				string fontName = parameter.ToString();
				if (string.IsNullOrWhiteSpace(fontName)) {
					return new SpannableString(value);
				}

				if (_assetPlugin == null) {
					_assetPlugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();
					_extendedFont = _assetPlugin.GetFontByName(fontName) as Font;
				}
				if (_context == null) {
					_context = MvvmCross.Platform.Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.BaseContext;
				}

				string cleanText = string.Empty;
				List<FontIndexPair> blockIndexes = AttributedFontHelper.GetFontTextBlocks(value, fontName, _assetPlugin, out cleanText);

				SpannableString converted = new SpannableString(cleanText);

				foreach (FontIndexPair block in blockIndexes) {
					SetAttributed(converted, block);
				}

				return converted;
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
		private void SetAttributed(SpannableString converted,FontIndexPair pair) {
			//get the font by tags
			var taggedFont =_assetPlugin.GetFontByTag(pair.FontTag);

			if (taggedFont != null) {
				//set the text color
				if (taggedFont.Color != null) {
					converted.SetSpan(new ForegroundColorSpan(taggedFont.Color.ToAndroidColor()), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
				}
				//set allignmen 
				if (taggedFont is Font) {
					Font taggedExtendedFont = taggedFont as Font;

					if (taggedExtendedFont.Alignment != TextAlignment.None) {
						Layout.Alignment alignment = taggedExtendedFont.Alignment == TextAlignment.Center?Layout.Alignment.AlignCenter:Layout.Alignment.AlignNormal;
						converted.SetSpan(new AlignmentSpanStandard(alignment), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
					}
				}

				if (_extendedFont != null) {
					//calculate the relative size to the regular font
					converted.SetSpan(new RelativeSizeSpan((float)taggedFont.Size / (float)_extendedFont.Size), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
				}
				//set the custom typeface
				converted.SetSpan(new CustomTypefaceSpan("sans-serif", DroidAssetPlugin.GetCachedFont(taggedFont, _context)), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
			}
		}
	}

}
