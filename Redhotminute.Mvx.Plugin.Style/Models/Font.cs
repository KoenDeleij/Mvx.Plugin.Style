using System;
using MvvmCross.Platform.UI;

namespace Redhotminute.Mvx.Plugin.Style
{
	public class Font : BaseFont
	{
		public MvxColor SelectedColor {
			get;
			set;
		}

		public MvxColor DisabledColor {
			get;
			set;
		}

		public int? LineHeight {
			get;
			set;
		}

		public TextAlignment Alignment {
			get;
			set;
		}

        /// <summary>
        /// Creates a new font and tag for color modifications
        /// </summary>
        /// <returns>The font with modified color.</returns>
        /// <param name="fontWithoutColor">Font without color.</param>
        /// <param name="newId">New identifier.</param>
        /// <param name="newColor">New color.</param>
        public static Font NewFontWithModifiedColor(Font fontWithoutColor,string newId,MvxColor newColor){
            Font font = new Font();
            font.Name = newId;
            font.Color = newColor;
            font.Alignment = fontWithoutColor.Alignment;
            font.DisabledColor = fontWithoutColor.DisabledColor;
            font.FontFilename = fontWithoutColor.FontFilename;
            font.FontPlatformName = fontWithoutColor.FontPlatformName;
            font.FontPlatformSize = fontWithoutColor.FontPlatformSize;
            font.LineHeight = fontWithoutColor.LineHeight;
            font.SelectedColor = fontWithoutColor.SelectedColor;
            font.Size = fontWithoutColor.Size;
            return font;
        }
	}
}


