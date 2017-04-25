using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Foundation;
using MvvmCross.Plugins.Color.iOS;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch
{
	public class TouchAssetPlugin : AssetPlugin {
		#region implemented abstract members of AssetPlugin

		public override void ConvertFontFileNameForPlatform(ref IBaseFont font) {
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
			Font extendedFont = font as Font;

			if (extendedFont != null) {
				this.ConvertFontFileNameForPlatform(ref font);
				UIStringAttributes stringAttributes = CreateAttributesByFont(font);

				var assetPlugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();

				string cleanText = string.Empty;
				var indexPairs = AttributedFontHelper.GetFontTextBlocks(text, extendedFont.Name, assetPlugin, out cleanText);

				var attributedText = new NSMutableAttributedString(cleanText);
				attributedText.AddAttributes(stringAttributes, new NSRange(0, cleanText.Length));

				//TODO add caching for same fonttags for the attributes
				foreach (FontIndexPair block in indexPairs) {
					//get the font for each tag and decorate the text
					if (!string.IsNullOrEmpty(block.FontTag)) {
						var tagFont = assetPlugin.GetFontByTag(block.FontTag);
						UIStringAttributes attr = CreateAttributesByFont(tagFont);
						attributedText.SetAttributes(attr, new NSRange(block.StartIndex, block.EndIndex - block.StartIndex));
					}
				}

				return attributedText;
			}
			return null;
		}

		private UIStringAttributes CreateAttributesByFont(IBaseFont font) {
			UIStringAttributes stringAttributes = new UIStringAttributes { };

			//add the font
			stringAttributes.Font = TouchAssetPlugin.GetCachedFont(font);
	
			//add the color
			if (font.Color != null) {
				stringAttributes.ForegroundColor = font.Color.ToNativeColor();
			}

			if (font is Font) {
				var extendedFont = font as Font;
				if (extendedFont.Alignment != TextAlignment.None) {
					UITextAlignment alignment = extendedFont.ToNativeAlignment();
					if (stringAttributes.ParagraphStyle == null) {
						stringAttributes.ParagraphStyle = new NSMutableParagraphStyle();
					}
					stringAttributes.ParagraphStyle.Alignment = alignment;
				}

				//add the lineheight
				if (extendedFont.LineHeight != 0) {
					if (stringAttributes.ParagraphStyle == null) {
						stringAttributes.ParagraphStyle = new NSMutableParagraphStyle();
					}

					stringAttributes.ParagraphStyle.LineSpacing = GetPlatformLineHeight(font.Size, extendedFont.LineHeight) * 0.5f;
					stringAttributes.ParagraphStyle.LineBreakMode = UILineBreakMode.TailTruncation;
				}
			}

			return stringAttributes;
		}
	}
}

