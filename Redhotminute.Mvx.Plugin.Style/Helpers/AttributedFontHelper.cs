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
			while (foundTag) {

                FontTag fontTag = null;
                tagProperties = new Dictionary<string, string>();
                beginTagStartIndex = text.IndexOf('<', findIndex);

				string tag = string.Empty;
				string endTag = string.Empty;

				if (beginTagStartIndex != -1) {
					//find the end of the tag
					beginTagEndIndex = text.IndexOf('>', beginTagStartIndex);

					if (beginTagEndIndex != -1) {

						//there's a tag, get the description
						tag = text.Substring(beginTagStartIndex + 1, beginTagEndIndex - beginTagStartIndex - 1);

                        //in case the tag contains an = , for example a href=, split the tag and the attribute
                        if(tag.Contains("=") && tag.Contains(" ")){
                            var attrs = tag.Split(' ');
                            if(attrs.Length >1){
                                //get the tag
                                tag = attrs[0];

                                //for each attribute
                                for (int i = 1; i < attrs.Length;i++){
                                    var splitAttribute = attrs[i].Split('=');
                                    if(splitAttribute.Length==2)
                                    {
                                        tagProperties.Add(splitAttribute[0], splitAttribute[1]);
                                    }                                    
                                }
                            }
                        }

                        var tagFont = assetPlugin.GetFontByTagWithTag(fontName, tag,out fontTag);

                        if (tagFont == null)
                        {
                            //if the font is not found, let it remain in the text and embed it in another block
                            int resetIndex = beginTagStartIndex;
                            if(beginTagStartIndex != 0){
                                resetIndex = 0;
                            }
                            //find the next tag
                            findIndex = beginTagEndIndex+1;
                            previousBeginTag = resetIndex;
                            continue;
                        }

                        endTag = $"</{tag}>";
                        endTagStartIndex = text.IndexOf(endTag, beginTagEndIndex);

                        //end tag not found
                        if (endTagStartIndex ==-1){
                            throw new Exception($"No matching end tag found in {text}");
                        }

                        endTagEndIndex = endTagStartIndex + (endTag.Length-1);

                        if (beginTagStartIndex != 0)
                        {
                            var startIndex = findIndex;
                            if(previousBeginTag != -1){
                                startIndex = previousBeginTag;
                            }
                            //in case the first block has no tag, add an empty block
                            fontTextBlocks.Add(new FontTextPair() { Text = text.Substring(startIndex, beginTagStartIndex - startIndex), FontTag = null });
                        }
						
                        fontTextBlocks.Add(new FontTextPair() { Text = text.Substring(beginTagEndIndex + 1, endTagStartIndex - beginTagEndIndex - 1), FontTag = fontTag,TagProperties=tagProperties });
						findIndex = endTagEndIndex+1;
					}
				}
				else {
					foundTag = false;
				}
			}
			//check if the end tag is the last character, if not add a final block till the end
			if (endTagEndIndex != text.Length) {
                fontTextBlocks.Add(new FontTextPair() { Text = text.Substring(endTagEndIndex + 1, text.Length - endTagEndIndex - 1), FontTag = null });
			}

			//create a clean text and convert the block fontTag pairs to indexTagPairs do that we know which text we need to decorate without tags present
			List<FontIndexPair> blockIndexes = new List<FontIndexPair>();

			int previousIndex = 0;
			foreach (FontTextPair block in fontTextBlocks) {
				cleanText = $"{cleanText}{block.Text}";
                blockIndexes.Add(new FontIndexPair() { FontTag = block.FontTag, StartIndex = previousIndex, EndIndex = cleanText.Length,TagProperties=block.TagProperties });
				previousIndex = cleanText.Length;
			}

			return blockIndexes;
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
