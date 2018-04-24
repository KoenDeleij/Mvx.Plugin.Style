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

        public NSAttributedString ParseToAttributedText(string text, IBaseFont font) {
			try
			{
                if (font != null)
                {
                    this.ConvertFontFileNameForPlatform(ref font);

                    var assetPlugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();

                    string cleanText = string.Empty;

                    var indexPairs = AttributedFontHelper.GetFontTextBlocks(text, font.Name, assetPlugin, out cleanText);

                    NSMutableAttributedString attributedText = new NSMutableAttributedString(cleanText);

                    UIStringAttributes stringAttributes = CreateAttributesByFont(ref attributedText, font);
                    attributedText.AddAttributes(stringAttributes, new NSRange(0, cleanText.Length));

					//TODO add caching for same fonttags for the attributes
					foreach (FontIndexPair block in indexPairs) {
						//get the font for each tag and decorate the text
                        if (block.FontTag != null && !string.IsNullOrEmpty(block.FontTag.OriginalFontName)) {
                            FontTag fontTag = null;
                            var tagFont = assetPlugin.GetFontByTagWithTag(font.Name,block.FontTag.Tag,out fontTag);

							tagFont = tagFont == null ? font : tagFont;
                            UIStringAttributes attr = CreateAttributesByFont(ref attributedText,tagFont,block,fontTag);
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

        private UIStringAttributes CreateAttributesByFont(ref NSMutableAttributedString text,IBaseFont font,FontIndexPair pair = null,FontTag tag = null) {
			UIStringAttributes stringAttributes = new UIStringAttributes { };

			//add the font
			stringAttributes.Font = TouchAssetPlugin.GetCachedFont(font);
	
			//add the color
			if (font.Color != null) {
				stringAttributes.ForegroundColor = font.Color.ToNativeColor();
			}

            if(pair != null && tag != null){
                if(tag.FontAction == FontTagAction.Link){
                    CreateLink(ref text,ref stringAttributes,font,pair);
                }
            }

			if (font is Font) {
				var extendedFont = font as Font;

                if (stringAttributes.ParagraphStyle == null)
                {
                    stringAttributes.ParagraphStyle = new NSMutableParagraphStyle();
                }

				if (extendedFont.Alignment != TextAlignment.None) {
					UITextAlignment alignment = extendedFont.ToNativeAlignment();
					stringAttributes.ParagraphStyle.Alignment = alignment;
				}

				//add the lineheight
                stringAttributes.ParagraphStyle.LineSpacing = extendedFont.LineHeight.HasValue?GetPlatformLineHeight(font.Size, extendedFont.LineHeight.Value):font.Size;
                stringAttributes.ParagraphStyle.LineBreakMode = UILineBreakMode.WordWrap;

                stringAttributes.ParagraphStyle.LineHeightMultiple = extendedFont.LineHeightMultiplier.HasValue?((float)extendedFont.LineHeightMultiplier.Value*1.37f):0f;
			}

			return stringAttributes;
		}

        private void CreateLink(ref NSMutableAttributedString text,ref UIStringAttributes attribute, IBaseFont font, FontIndexPair pair)
        {
            //if theres a link property, use that one, if not, use the text itself
            string link;
            if (pair.TagProperties != null && pair.TagProperties.ContainsKey("href"))
            {
                link = pair.TagProperties.GetValueOrDefault("href");
            }
            else
            {
                link = text.Value.Substring(pair.StartIndex, pair.EndIndex - pair.StartIndex).Trim();
            }
            attribute.Link = new NSUrl(link);
            attribute.UnderlineStyle = NSUnderlineStyle.Single;
            attribute.UnderlineColor = font.Color.ToNativeColor();
        }

		public override IAssetPlugin ClearFonts() {
			_fontsCache = new Dictionary<string, UIFont>();
			return base.ClearFonts();
		}

		public static float GetPlatformLineHeight(float fontSize, float lineHeight)
		{
			float factor = LineHeightFactor.HasValue ? LineHeightFactor.Value : FontSizeFactor;
            return (lineHeight*factor);//-fontSize
		}
	}
}

