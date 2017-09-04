using System;
using MvvmCross.Platform.UI;

namespace Redhotminute.Mvx.Plugin.Style.Models {
	public interface IBaseFont {
		string FontFilename {
			get;
			set;
		}

		string Name {
			get;
			set;
		}

		MvxColor Color {
			get;
			set;
		}

		string FontPlatformName {
			get;
			set;
		}

		float FontPlatformSize {
			get;
			set;
		}

		int Size {
			get;
			set;
		}
	}
}
