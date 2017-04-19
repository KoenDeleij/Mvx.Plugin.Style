using System;
using Android.Views;
using MvvmCross.Binding;

namespace Redhotminute.Mvx.Plugin.Style.Droid {
	public static class FontAlignmentHelper {
		public static GravityFlags ToNativeAlignment(this Font font) {
			GravityFlags flags = GravityFlags.Left;

			switch (font.Alignment) {
				case TextAlignment.Left: 		flags= GravityFlags.Left;break;
				case TextAlignment.Center: 		flags= GravityFlags.CenterHorizontal; break;
				case TextAlignment.Right: 		flags= GravityFlags.Right; break;
				case TextAlignment.Justified: 	flags = GravityFlags.Left;
												MvxBindingTrace.Warning("Justified binding is not supported on android");break;	
			}

			return flags;
		}
	}
}
