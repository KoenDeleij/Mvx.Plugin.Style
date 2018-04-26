using System;
using Android.Views;
using MvvmCross.Binding;
using Redhotminute.Mvx.Plugin.Style.Models;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Helpers {
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
