using System;
using MvvmCross.Platform.UI;

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

		public static BaseFont NewFontWithModifiedColor(BaseFont fontWithoutColor, string newId, MvxColor newColor)
		{
			BaseFont font = new BaseFont();
			Font fontwOutColor = fontWithoutColor as Font;
			font.Name = newId;
			font.Color = newColor;
			font.FontFilename = fontWithoutColor.FontFilename;
			font.FontPlatformName = fontWithoutColor.FontPlatformName;
			font.FontPlatformSize = fontWithoutColor.FontPlatformSize;
			font.Size = fontWithoutColor.Size;
			return font;
		}
	}
}

