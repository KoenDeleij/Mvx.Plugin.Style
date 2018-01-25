using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Plugins.Color.iOS;
using Redhotminute.Mvx.Plugin.Style.Helpers;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Touch.Helpers;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Plugin
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

        public NSAttributedString ParseToAttributedText(string text, IBaseFont font, NSAttributedStringDocumentAttributes docAttributes = null) {
			try
			{
                if (font != null)
                {
                    this.ConvertFontFileNameForPlatform(ref font);
                    UIStringAttributes stringAttributes = CreateAttributesByFont(font);

                    var assetPlugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();

                    string cleanText = string.Empty;

                    var indexPairs = AttributedFontHelper.GetFontTextBlocks(text, font.Name, assetPlugin, out cleanText);


                    NSMutableAttributedString attributedText;

                    if (docAttributes != null)
                    {
                        NSError er = null;
                        var newText = new NSAttributedString(cleanText, docAttributes,ref er);
                        attributedText = newText.MutableCopy() as NSMutableAttributedString;
                    }
                    else{
                        attributedText = new NSMutableAttributedString(cleanText);
                    }

					attributedText.AddAttributes(stringAttributes, new NSRange(0, cleanText.Length));

					//TODO add caching for same fonttags for the attributes
					foreach (FontIndexPair block in indexPairs) {
						//get the font for each tag and decorate the text
						if (!string.IsNullOrEmpty(block.FontTag)) {
							var tagFont = assetPlugin.GetFontByTag(font.Name,block.FontTag);
							tagFont = tagFont == null ? font : tagFont;
							UIStringAttributes attr = CreateAttributesByFont(tagFont);
							attributedText.SetAttributes(attr, new NSRange(block.StartIndex, block.EndIndex - block.StartIndex));
						}
					}
	                

					return attributedText;
				}
			}
			catch (Exception e)
			{
                //just return the text as passed if something fails
                return new NSMutableAttributedString(text);
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
                if (extendedFont.LineHeight.HasValue) {
					if (stringAttributes.ParagraphStyle == null) {
						stringAttributes.ParagraphStyle = new NSMutableParagraphStyle();
					}

                    stringAttributes.ParagraphStyle.LineSpacing = GetPlatformLineHeight(font.Size, extendedFont.LineHeight.Value) * 0.5f;
                    //TODO make the linebreakmode configurable
                    stringAttributes.ParagraphStyle.LineBreakMode = UILineBreakMode.WordWrap;
				}
			}

			return stringAttributes;
		}

		public override IAssetPlugin ClearFonts() {
			_fontsCache = new Dictionary<string, UIFont>();
			return base.ClearFonts();
		}

		public static float GetPlatformLineHeight(float fontSize, float lineHeight)
		{
			float factor = LineHeightFactor.HasValue ? LineHeightFactor.Value : FontSizeFactor;
			return (lineHeight - fontSize) * factor;
		}
	}
}

