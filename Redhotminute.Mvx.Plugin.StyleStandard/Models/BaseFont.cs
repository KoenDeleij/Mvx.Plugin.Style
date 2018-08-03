using MvvmCross.UI;

namespace Redhotminute.Mvx.Plugin.Style.Models
{
    public class BaseFont:IBaseFont
	{
		public string FontFilename {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public MvxColor Color {
			get;
			set;
		}

		public string FontPlatformName {
			get;
			set;
		}

		public float FontPlatformSize {
			get;
			set;
		}

		public int Size {
			get;
			set;
		}

        public BaseFont SetColor(MvxColor color)
        {
            this.Color = color;
            return this;
        }

        public BaseFont SetName(string name)
        {
            this.Name = name;
            return this;
        }

        public BaseFont SetSize(int size)
        {
            this.Size = size;
            return this;
        }

        public static TFont CopyFont<TRefFont, TFont>(TRefFont font, string newId) where TRefFont : BaseFont where TFont : BaseFont, new()
        {
            TFont newFont = new TFont();
            newFont.Name = newId;
            newFont.FontFilename = font.FontFilename;
            newFont.FontPlatformName = font.FontPlatformName;
            newFont.FontPlatformSize = font.FontPlatformSize;
            newFont.Color = font.Color;
            newFont.Size = font.Size;

            return newFont;
        }
	}
}

