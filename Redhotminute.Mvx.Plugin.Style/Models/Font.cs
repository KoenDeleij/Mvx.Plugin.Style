using System;
using MvvmCross.Platform.UI;

namespace Redhotminute.Mvx.Plugin.Style.Models
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

        public float? LineHeightMultiplier
        {
            get;
            set;
        }

		public TextAlignment Alignment {
			get;
			set;
		}

        public static TFont CopyFont<TRefFont, TFont>(TRefFont font, string newId) where TRefFont : Font where TFont : Font, new()
        {
            TFont newFont = new TFont();
            newFont.Name = newId;
            newFont.FontFilename = font.FontFilename;
            newFont.FontPlatformName = font.FontPlatformName;
            newFont.FontPlatformSize = font.FontPlatformSize;
            newFont.Alignment = font.Alignment;
            newFont.Color = font.Color;
            newFont.DisabledColor = font.DisabledColor;
            newFont.LineHeight = font.LineHeight;
            newFont.LineHeightMultiplier = font.LineHeightMultiplier;
            newFont.SelectedColor = font.SelectedColor;
            newFont.Size = font.Size;

            return newFont;
        }
	}
}


