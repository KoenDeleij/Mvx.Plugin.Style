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
	}
}

