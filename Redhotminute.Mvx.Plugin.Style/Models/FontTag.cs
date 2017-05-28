using System;
namespace Redhotminute.Mvx.Plugin.Style {
	public class FontTag {
		public FontTag(string originalFontName, string tag) {
			OriginalFontName = originalFontName;
			Tag = tag;
		}
		public string OriginalFontName {
			get;
			set;
		}

		public string Tag {
			get;
			set;
		}
	}
}
