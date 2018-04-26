using System;
using System.Collections.Generic;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Helpers {
	public class AttributedFontHelper {
		public static List<FontIndexPair> GetFontTextBlocks(string text,string fontName,IAssetPlugin assetPlugin,out string cleanText) {
			cleanText = string.Empty;

			if (string.IsNullOrWhiteSpace(fontName)) {
				return null;
			}

            //this is a sample <h1>text and</h1> it ends here <h2>more stuff</h2>
            //0				   1  2        3   4

            var fontTextBlocks = IterateThroughTags(assetPlugin, text, fontName);

            //create a clean text and convert the block fontTag pairs to indexTagPairs do that we know which text we need to decorate without tags present
            List<FontIndexPair> blockIndexes = new List<FontIndexPair>();

            int previousIndex = 0;
            foreach (FontTextPair block in fontTextBlocks)
            {
                cleanText = $"{cleanText}{block.Text}";
                blockIndexes.Add(new FontIndexPair() { FontTag = block.FontTag, StartIndex = previousIndex, EndIndex = cleanText.Length, TagProperties = block.TagProperties });
                previousIndex = cleanText.Length;
            }

            return blockIndexes;
		}

        private static List<FontTextPair> IterateThroughTags(IAssetPlugin assetPlugin,string text,string fontName){
            //find the first text
            int findIndex = 0;

            List<FontTextPair> fontTextBlocks = new List<FontTextPair>();

            int beginTagStartIndex = -1;
            int beginTagEndIndex = -1;

            int endTagStartIndex = -1;
            int endTagEndIndex = -1;

            //Start searching for tags
            bool foundTag = true;
            int previousBeginTag = -1;
            Dictionary<string, string> tagProperties;
            bool skippedUnknownTag = false;
            while (foundTag)
            {

                FontTag fontTag = null;
                tagProperties = new Dictionary<string, string>();

                //find the end of the tag
                beginTagStartIndex = text.IndexOf('<', findIndex);

                string tag = string.Empty;
                string endTag = string.Empty;

                if (beginTagStartIndex != -1)
                {
                    beginTagEndIndex = text.IndexOf('>', beginTagStartIndex);

                    if (beginTagEndIndex != -1)
                    {
                        //there's a tag, get the description
                        tag = text.Substring(beginTagStartIndex + 1, beginTagEndIndex - beginTagStartIndex - 1);

                        tagProperties = GetTagProperties(ref tag);

                        var tagFont = assetPlugin.GetFontByTagWithTag(fontName, tag, out fontTag);

                        if (tagFont == null)
                        {
                            //if the font is not found, let it remain in the text and embed it in another block
                            //beginTagStartIndex = previousBeginTag;
                            if(findIndex == 0){
                                previousBeginTag = 0;
                            }
                            findIndex = beginTagEndIndex + 1;
                            skippedUnknownTag = true;
                            continue;
                        }

                        endTag = $"</{tag}>";
                        endTagStartIndex = text.IndexOf(endTag, beginTagEndIndex);

                        //end tag not found
                        if (endTagStartIndex == -1)
                        {
                            throw new Exception($"No matching end tag found in {text}");
                        }

                        endTagEndIndex = endTagStartIndex + (endTag.Length - 1);

                        if (beginTagStartIndex != 0)
                        {
                            var startIndex = findIndex;
                            if (skippedUnknownTag)
                            {
                                startIndex = previousBeginTag;
                            }
                            //in case the first block has no tag, add an empty block
                            fontTextBlocks.Add(new FontTextPair() { Text = text.Substring(startIndex, beginTagStartIndex - startIndex), FontTag = null });
                        }

                        fontTextBlocks.Add(new FontTextPair() { Text = text.Substring(beginTagEndIndex + 1, endTagStartIndex - beginTagEndIndex - 1), FontTag = fontTag, TagProperties = tagProperties });
                        findIndex = endTagEndIndex + 1;
                    }
                }
                else
                {
                    foundTag = false;
                }

                skippedUnknownTag = false;
                previousBeginTag = beginTagStartIndex;
            }

            //check if the end tag is the last character, if not add a final block till the end
            if (endTagEndIndex < text.Length - 1)
            {
                fontTextBlocks.Add(new FontTextPair() { Text = text.Substring(endTagEndIndex + 1, text.Length - endTagEndIndex - 1), FontTag = null });
            }

            return fontTextBlocks;
        }

        private static Dictionary<string,string> GetTagProperties(ref string tag){
            Dictionary<string, string> tagProperties = null;
            //in case the tag contains an = , for example a href=, split the tag and the attribute
            if (tag.Contains("=") && tag.Contains(" "))
            {
                var attrs = tag.Split(' ');
                if (attrs.Length > 1)
                {
                    //get the tag
                    tag = attrs[0];

                    //for each attribute
                    for (int i = 1; i < attrs.Length; i++)
                    {
                        if (tagProperties == null)
                        {
                            tagProperties = new Dictionary<string, string>();
                        }
                        var attributePair = GetKeyValueFromPropertyTag(attrs[i]);
                        tagProperties.Add(attributePair.Key,attributePair.Value);
                    }
                }
            }
            return tagProperties;
        }

        private static KeyValuePair<string,string> GetKeyValueFromPropertyTag(string keyValueProperty){
            var indexOfFirstEqual = keyValueProperty.IndexOf('=');
            //Strip the first and last part
            var attrKey = keyValueProperty.Substring(0, indexOfFirstEqual);
            var attrValue = keyValueProperty.Substring(indexOfFirstEqual + 1, keyValueProperty.Length - (indexOfFirstEqual + 1));

            //Strip the quotes for the actual value
            attrValue = attrValue.Replace("\"", "");
            attrValue = attrValue.Replace("'", "");

           return new KeyValuePair<string, string>(attrKey, attrValue);
        }
	}


    public class FontTextPair
    {
        public FontTag FontTag
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public Dictionary<string, string> TagProperties
        {
            get;
            set;
        }
    }

	public class FontIndexPair {
        public FontTag FontTag {
			get;
			set;
		}

        public Dictionary<string, string> TagProperties
        {
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
