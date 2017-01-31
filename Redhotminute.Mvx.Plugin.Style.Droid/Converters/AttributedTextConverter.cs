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

				//this is a sample <h1>text and</h1> it ends here <h2>more stuff</h2>
				//0				   1  2        3   4

				//find the first text
				int findIndex = 0;

				List<FontTextPair> fontTextBlocks = new List<FontTextPair>();

				int beginTagStartIndex = -1;
				int beginTagEndIndex = -1;

				int endTagStartIndex = -1;
				int endTagEndIndex = -1;

				//Start searching for tags
				bool foundTag = true;
				while (foundTag) {
					beginTagStartIndex = value.IndexOf('<', findIndex);
					string tag = string.Empty;
					string endTag = string.Empty;

					if (beginTagStartIndex != -1) {
						//find the end of the tag
						beginTagEndIndex = value.IndexOf('>', beginTagStartIndex);

						if (beginTagEndIndex != -1) {

							//there's a tag, get the description
							tag = value.Substring(beginTagStartIndex + 1, beginTagEndIndex - beginTagStartIndex - 1);

							//find the end Index
							endTag = $"</{tag}>";
							endTagStartIndex = value.IndexOf(endTag, beginTagEndIndex);

							endTagEndIndex = endTagStartIndex + endTag.Length;

							fontTextBlocks.Add(new FontTextPair() { Text = value.Substring(findIndex, beginTagStartIndex - findIndex), FontTag = string.Empty});

							//from 3 to 2
							fontTextBlocks.Add(new FontTextPair() { Text = value.Substring(beginTagEndIndex + 1, endTagStartIndex - beginTagEndIndex - 1), FontTag = tag });
							findIndex = endTagEndIndex;
						}
					}
					else {
						foundTag = false;
					}
				}
				//check if the end tag is the last character, if not add a final block till the end
				if (endTagEndIndex != value.Length) {
					fontTextBlocks.Add(new FontTextPair() { Text = value.Substring(endTagEndIndex + 1, value.Length - endTagEndIndex - 1), FontTag = string.Empty });
				}

				string cleanText = string.Empty;

				//create a clean text and convert the block fontTag pairs to indexTagPairs do that we know which text we need to decorate without tags present
				List<FontIndexPair> blockIndexes = new List<FontIndexPair>();

				int previousIndex = 0;
				foreach (FontTextPair block in fontTextBlocks) {
					cleanText = $"{cleanText}{block.Text}";
					blockIndexes.Add(new FontIndexPair() { FontTag = block.FontTag, StartIndex = previousIndex, EndIndex = cleanText.Length });
					previousIndex = cleanText.Length;
				}

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

	public class FontTextPair {
		public string FontTag {
			get;
			set;
		}

		public string Text {
			get;
			set;
		}
	}

	public class FontIndexPair {
		public string FontTag {
			get;
			set;
		}

		public int StartIndex {
			get;
			set;
		}

		public int EndIndex {
			get;
			set;
		}
	}
}
