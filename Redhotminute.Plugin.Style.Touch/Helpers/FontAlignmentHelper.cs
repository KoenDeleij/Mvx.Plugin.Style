using System;
using UIKit;

namespace Redhotminute.Plugin.Style.Touch {
	public static class FontAlignmentHelper {
		public static UITextAlignment ToNativeAlignment(this Font font) {

			switch (font.Alignment) {
				case TextAlignment.Left: return UITextAlignment.Left;break;
				case TextAlignment.Center: return UITextAlignment.Center; break;
				case TextAlignment.Right: return UITextAlignment.Right; break;
			}
			return UITextAlignment.Left;
		}
	}
}
