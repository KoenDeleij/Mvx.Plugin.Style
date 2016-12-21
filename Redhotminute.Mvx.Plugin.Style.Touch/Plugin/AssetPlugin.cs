using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Foundation;
using MvvmCross.Plugins.Color.iOS;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch
{
	public class TouchAssetPlugin : AssetPlugin
	{
		#region implemented abstract members of AssetPlugin

		public override void ConvertFontFileNameForPlatform (ref IBaseFont font)
		{
			//if the fontplatformname is already set, just use that one
			if (string.IsNullOrWhiteSpace(font.FontPlatformName)) {
				font.FontPlatformName = RemoveFontExtension(font.FontFilename);
			}
		}

		private string RemoveFontExtension(string fontFileName) {
			if (string.IsNullOrWhiteSpace(fontFileName)) {
				return string.Empty;
			}
			return fontFileName.Substring(0, fontFileName.IndexOf('.')).Replace("-", " ");
		}

		#endregion

		private static Dictionary<string, UIFont> _fontsCache = new Dictionary<string, UIFont>();
		//TODO generic possible?
		public static UIFont GetCachedFont(IBaseFont font) {
			if (!_fontsCache.ContainsKey(font.Name)) {
				if (font.Size == default(int)) {
					font.Size = 16;
				}
				_fontsCache[font.Name] = UIFont.FromName(font.FontPlatformName, AssetPlugin.GetPlatformFontSize(font.Size));

			}

			return _fontsCache[font.Name];
		}

		public NSAttributedString ParseToAttributedText(string text, IBaseFont font) {
			var AttributedText = new NSMutableAttributedString(text);

			UIStringAttributes stringAttributes;
			stringAttributes = new UIStringAttributes { };

			Font extendedFont = font as Font;

			if (extendedFont != null) {
				this.ConvertFontFileNameForPlatform(ref font);
				//add the font
				stringAttributes.Font = TouchAssetPlugin.GetCachedFont(font);

				//add the lineheight
				if (extendedFont.LineHeight != 0) {
					stringAttributes.ParagraphStyle = new NSMutableParagraphStyle() { LineSpacing = GetPlatformLineHeight(font.Size,extendedFont.LineHeight)* 0.5f,LineBreakMode=UILineBreakMode.TailTruncation };
				}

				//add the color
				if (font.Color != null) {
					stringAttributes.ForegroundColor = font.Color.ToNativeColor();
				}

				if (extendedFont.Alignment != TextAlignment.None) {
					UITextAlignment alignment = extendedFont.ToNativeAlignment();
					if (stringAttributes.ParagraphStyle == null) {
						stringAttributes.ParagraphStyle = new NSMutableParagraphStyle();
					}
					stringAttributes.ParagraphStyle.Alignment = alignment;
				}
			}

			AttributedText.AddAttributes(stringAttributes, new NSRange(0, text.Length));
			AttributedText = ParseBoldAttributedText(text, extendedFont, AttributedText);

			return AttributedText;
		}

		public NSMutableAttributedString ParseBoldAttributedText(string originalText,Font font,NSMutableAttributedString mutableText = null) {
			
			if (font.BoldFont != null) {
				try {
					if (mutableText == null) {
						mutableText = new NSMutableAttributedString(originalText);
					}
					const string RegexPattern = @"\*([^\*]+)\*";
					Regex r = new Regex(RegexPattern, RegexOptions.IgnoreCase);

					List<int> replacing = new List<int>();

					if (r.IsMatch(originalText)) {
						var match = r.Matches(originalText);
						foreach (Group gr in match) {
							UIStringAttributes attr = new UIStringAttributes { ForegroundColor = font.BoldFont.Color.ToNativeColor(),Font=TouchAssetPlugin.GetCachedFont(font.BoldFont),ParagraphStyle=new NSMutableParagraphStyle() { LineSpacing = GetPlatformLineHeight(font.Size, font.LineHeight) * 0.5f }};

							mutableText.SetAttributes(attr, new NSRange(gr.Index, gr.Length));
							replacing.Add(gr.Index);
							replacing.Add(gr.Index + gr.Length - 1);
						}
					}
					//remove all * occurrences
					replacing.Reverse();
					foreach (int boldTag in replacing) {
						mutableText.Replace(new NSRange(boldTag, 1), string.Empty);
					}
				}
				catch (Exception e) {
					// do nothing
				}
			}
			return mutableText;
		}

	}
}

