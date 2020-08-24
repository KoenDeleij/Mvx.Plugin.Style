using System.Drawing;

namespace Mvx.Plugin.Style.Models
{
    public interface IBaseFont {
		string FontFilename {
			get;
			set;
		}

		string Name {
			get;
			set;
		}

		Color Color {
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
