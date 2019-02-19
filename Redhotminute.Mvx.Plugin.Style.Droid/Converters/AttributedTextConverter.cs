using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using MvvmCross.Binding;
using MvvmCross.Converters;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android;
using MvvmCross.Plugin.Color.Platforms.Android;
using Redhotminute.Mvx.Plugin.Style.Droid.Helpers;
using Redhotminute.Mvx.Plugin.Style.Droid.Helpers.Spans;
using Redhotminute.Mvx.Plugin.Style.Droid.Plugin;
using Redhotminute.Mvx.Plugin.Style.Helpers;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Converters {
	public class AttributedTextConverter : MvxValueConverter<string, AttributedStringBaseFontWrapper>, IMvxValueConverter {

		IAssetPlugin _assetPlugin;
		Font _extendedFont;
		Context _context;
        bool _containsLink = false;
        IBaseFont _clickableFont;

		protected override AttributedStringBaseFontWrapper Convert (string value, Type targetType, object parameter, CultureInfo culture) {
            if(parameter == null){
                return DefaultWrapper(value);
            }
			try {
				string fontName = parameter.ToString();
				if (string.IsNullOrWhiteSpace(fontName)) {
                    return DefaultWrapper(value);
				}

				if (_assetPlugin == null) {
					_assetPlugin = MvvmCross.Mvx.IoCProvider.Resolve<IAssetPlugin>();
				}
				if (_context == null) {
					_context = MvvmCross.Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity.BaseContext;
				}

				_extendedFont = _assetPlugin.GetFontByName(fontName) as Font;

				string cleanText = string.Empty;
				List<FontIndexPair> blockIndexes = AttributedFontHelper.GetFontTextBlocks(value, fontName, _assetPlugin, out cleanText);

				SpannableString converted = new SpannableString(cleanText);

				foreach (FontIndexPair block in blockIndexes) {
					SetAttributed(converted, block,_extendedFont);
				}

				return new AttributedStringBaseFontWrapper() { SpannableString = converted , Font = _extendedFont,ContainsClickable = _containsLink,ClickableFont = _clickableFont};
			}
			catch (Exception e){
                MvxBindingLog.Instance.Error(e.Message);
			}

            return DefaultWrapper(value);
        }

        private AttributedStringBaseFontWrapper DefaultWrapper(string value) =>
            new AttributedStringBaseFontWrapper() { SpannableString = new SpannableString(value) };

		/// <summary>
		/// Set the attributes of a part of the text
		/// </summary>
		/// <param name="converted">Converted.</param>
		/// <param name="pair">Pair.</param>
		private void SetAttributed(SpannableString converted, FontIndexPair pair, Font fallbackFont) {
            //get the font by tags

            FontTag fontTag = null;
            if (pair.FontTag != null)
            {
                var taggedFont = _assetPlugin.GetFontByTagWithTag(fallbackFont.Name, pair.FontTag.Tag, out fontTag);

                if (taggedFont != null)
                {
                    SetFont(ref converted, taggedFont, pair,fontTag);
                    return;
                }
            }

			if(fallbackFont!= null) {
                SetFont(ref converted, fallbackFont, pair, fontTag);
			}
		}

        private void SetFont(ref SpannableString converted, IBaseFont font,FontIndexPair pair,FontTag fontTag) {
			//set the text color
			if (font.Color != null) {
                converted.SetSpan(new ForegroundColorSpan(font.Color.ToNativeColor()), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
			}

            if (fontTag != null && fontTag.FontAction == FontTagAction.Link)
            {
                CreateLink(ref converted,font,pair);
            }

			//set allignment
			if (font is Font) {
				Font taggedExtendedFont = font as Font;

				if (taggedExtendedFont.Alignment != TextAlignment.None) {
					Layout.Alignment alignment = taggedExtendedFont.Alignment == TextAlignment.Center ? Layout.Alignment.AlignCenter : Layout.Alignment.AlignNormal;
                    converted.SetSpan(new AlignmentSpanStandard(alignment), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
				}
			}

			if (_extendedFont != null) {
				//calculate the relative size to the regular font
                converted.SetSpan(new RelativeSizeSpan((float)font.Size / (float)_extendedFont.Size), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
			}
			//set the custom typeface
            converted.SetSpan(new CustomTypefaceSpan("sans-serif", DroidAssetPlugin.GetCachedFont(font, _context)), pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);

		}

        private void CreateLink(ref SpannableString converted,IBaseFont font, FontIndexPair pair){
            //if theres a link property, use that one, if not, use the text itself
            string link;
            if (pair.TagProperties != null && pair.TagProperties.ContainsKey("href"))
            {
                link = pair.TagProperties.GetValueOrDefault("href");
            }
            else
            {
                link = converted.ToString().Substring(pair.StartIndex, pair.EndIndex - pair.StartIndex).Trim();
            }
            converted.SetSpan(new ClickableLinkSpan() { Link = link }, pair.StartIndex, pair.EndIndex, SpanTypes.ExclusiveInclusive);
            _clickableFont = font;
            _containsLink = true;
        }
	}

}
