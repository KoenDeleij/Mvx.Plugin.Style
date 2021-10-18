using System.Drawing;

namespace Mvx.Plugin.Style.Models
{
    public class Font : BaseFont
	{
		public Color SelectedColor {
			get;
			set;
		}

		public Color DisabledColor {
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

        public Font SetSelectedColor(Color color)
        {
            this.SelectedColor = color;
            return this;
        }

        public Font SetDisabledColor(Color color)
        {
            this.DisabledColor = color;
            return this;
        }

        public static new TFont CopyFont<TRefFont, TFont>(TRefFont font, string newId) where TRefFont : Font where TFont : Font, new()
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


