using System;
namespace Redhotminute.Mvx.Plugin.Style.Models {
	public class FontTag {
        public FontTag(string originalFontName, string tag,FontTagAction action = FontTagAction.Text) {
			OriginalFontName = originalFontName;
			Tag = tag;
            FontAction = action;
		}
		public string OriginalFontName {
			get;
			set;
		}

		public string Tag {
			get;
			set;
		}

        public FontTagAction FontAction
        {
            get;
            set;
        }
	}
}
