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

        public LineBreakMode LineBreakMode
        {
            get;
            set;
        } = LineBreakMode.None;

        public Font SetLineHeightMultiPlier(float multiplier){
            this.LineHeightMultiplier = multiplier;
            return this;
        }

        public Font SetAlignment(TextAlignment alignment)
        {
            this.Alignment = alignment;
            return this;
        }

        public Font SetLineHeight(int lineHeight)
        {
            this.LineHeight = lineHeight;
            return this;
        }

        public Font SetSelectedColor(MvxColor color)
        {
            this.SelectedColor = color;
            return this;
        }

        public Font SetDisabledColor(MvxColor color)
        {
            this.DisabledColor = color;
            return this;
        }

        public static TFont CopyFont<TRefFont, TFont>(TRefFont font, string newId) where TRefFont : Font where TFont : Font, new()
        {
            TFont newFont = new TFont
            {
                Name = newId,
                FontFilename = font.FontFilename,
                FontPlatformName = font.FontPlatformName,
                FontPlatformSize = font.FontPlatformSize,
                Alignment = font.Alignment,
                Color = font.Color,
                DisabledColor = font.DisabledColor,
                LineHeight = font.LineHeight,
                LineHeightMultiplier = font.LineHeightMultiplier,
                SelectedColor = font.SelectedColor,
                Size = font.Size
            };

            return newFont;
        }
	}
}


