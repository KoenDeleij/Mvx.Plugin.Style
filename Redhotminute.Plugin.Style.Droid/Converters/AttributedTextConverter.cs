using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Plugins.Color.Droid;

namespace Redhotminute.Plugin.Style.Droid {
	public abstract class AttributedTextConverter : MvxValueConverter<string, SpannableString>, IMvxValueConverter {

		protected override SpannableString Convert(string value, Type targetType, object parameter, CultureInfo culture) {
			return new SpannableString(value);
		}

		private List<string> GetAttributedBlocks(string value, char characterToLookFor) {
			List<string> blocks = value.Split(characterToLookFor).ToList();
			//if the last item is ending with the character, remove that last empty one
			blocks.Remove(string.Empty);
			return blocks;
		}

		private string GetUnattributedValue(List<string> blocks) {
			//clean up the format
			return string.Concat(blocks);
		}

		protected SpannableString CreateAttributedText(string value, char characterToLookFor) {
			List<string> blocks = GetAttributedBlocks(value, characterToLookFor);
			string cleanValue = GetUnattributedValue(blocks);

			SpannableString converted = new SpannableString(cleanValue);

			//for each block, find the index, and set the span
			List<int> indexes = new List<int>();

			foreach (string block in blocks) {
				int index = cleanValue.IndexOf(block, StringComparison.Ordinal);
				indexes.Add(index);
			}
			indexes.Add(cleanValue.Length);

			bool attributeSwitch = value[0].Equals(characterToLookFor);
			for (int i = 0; i < indexes.Count - 1; i++) {
				if (attributeSwitch) {
					SetAttributed(converted, indexes[i], indexes[i + 1]);
				}
				attributeSwitch = !attributeSwitch;
			}
			return converted;
		}
		public abstract void SetAttributed(SpannableString converted, int startIndex, int endIndex);
	}

	public class AttributedBoldValueConverter : AttributedTextConverter {
		private IBaseFont _boldFont;
		IAssetPlugin _assetPlugin;
		Context _context;

		public override void SetAttributed(SpannableString converted, int startIndex, int endIndex) {
			converted.SetSpan(new ForegroundColorSpan(_boldFont.Color.ToAndroidColor()), startIndex, endIndex, SpanTypes.ExclusiveInclusive);
			converted.SetSpan(new CustomTypefaceSpan("sans-serif",DroidAssetPlugin.GetCachedFont(_boldFont,_context)), startIndex, endIndex, SpanTypes.ExclusiveInclusive);
		}

		protected override SpannableString Convert(string value, Type targetType, object parameter, CultureInfo culture) {

			//check if the parameters are filled
			string fontName = parameter.ToString();

			if (string.IsNullOrWhiteSpace(fontName)) {
				return new SpannableString(value);
			}

			if (_assetPlugin == null) {
				_assetPlugin = Mvx.Resolve<IAssetPlugin>();
			}
			if (_context == null) {
				_context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.BaseContext;
			}

			//get the font
			var font = _assetPlugin.GetFont(fontName);

			//get the bold font
			if (font != null) {
				//if there's a bold font defined, pick that one. if not, return to the given font
				var extendedFont = (font as Font);
				if (extendedFont != null) {
					if (extendedFont.BoldFont != null) {
						_boldFont = extendedFont.BoldFont;
					}
					else {
						_boldFont = extendedFont;
					}
					return CreateAttributedText(value, '*');
				}
			}

			return null;
		}
	}
}
