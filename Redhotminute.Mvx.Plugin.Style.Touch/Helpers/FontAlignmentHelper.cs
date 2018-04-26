using System;
using Redhotminute.Mvx.Plugin.Style.Models;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Helpers {
	public static class FontAlignmentHelper {
		public static UITextAlignment ToNativeAlignment(this Font font) {

			switch (font.Alignment) {
				case TextAlignment.Left: return UITextAlignment.Left;
				case TextAlignment.Center: return UITextAlignment.Center; 
				case TextAlignment.Right: return UITextAlignment.Right;
				case TextAlignment.Justified: return UITextAlignment.Justified; 
			}
			return UITextAlignment.Left;
		}
	}
}
