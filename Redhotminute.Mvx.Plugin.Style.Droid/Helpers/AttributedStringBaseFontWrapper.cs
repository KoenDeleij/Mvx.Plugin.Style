using System;
using Android.Text;
using Redhotminute.Mvx.Plugin.Style.Models;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Helpers {
	public class AttributedStringBaseFontWrapper {
		public SpannableString SpannableString {
			get;
			set;
		}

		public Font Font {
			get;
			set;
		}

        public bool ContainsClickable
        {
            get;
            set;
        } = false;

        public IBaseFont ClickableFont
        {
            get;
            set;
        }
	}
}
