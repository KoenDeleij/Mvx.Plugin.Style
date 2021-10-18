using System;
using Mvx.Plugin.Style.Models;
using UIKit;

namespace Mvx.Plugin.Style.iOS.Helpers {
	public static class FontHelpers {
		public static UITextAlignment ToNativeAlignment(this Font font) {

			switch (font.Alignment) {
				case TextAlignment.Left: return UITextAlignment.Left;
				case TextAlignment.Center: return UITextAlignment.Center; 
				case TextAlignment.Right: return UITextAlignment.Right;
				case TextAlignment.Justified: return UITextAlignment.Justified; 
			}
			return UITextAlignment.Left;
		}

        public static UILineBreakMode ToNativeLineBreakMode(this Font font)
        {
            switch (font.LineBreakMode)
            {
                case LineBreakMode.None: return UILineBreakMode.WordWrap;
                case LineBreakMode.TruncateTail: return UILineBreakMode.TailTruncation;
                case LineBreakMode.WordWrap: return UILineBreakMode.WordWrap;
            }
            return UILineBreakMode.WordWrap;
        }
	}
}
