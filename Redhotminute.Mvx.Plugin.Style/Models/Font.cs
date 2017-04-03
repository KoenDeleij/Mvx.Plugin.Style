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

		public int LineHeight {
			get;
			set;
		}

		public TextAlignment Alignment {
			get;
			set;
		}
	}
}


