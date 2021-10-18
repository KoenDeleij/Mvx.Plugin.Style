using System;
using Android.Text;
using Mvx.Plugin.Style.Models;

namespace Mvx.Plugin.Style.Droid.Helpers {
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
