using System;
using Android.Views;
using Microsoft.Extensions.Logging;
using MvvmCross.Binding;
using MvvmCross.Logging;
using Mvx.Plugin.Style.Models;

namespace Mvx.Plugin.Style.Droid.Helpers {
	public static class FontAlignmentHelper {
		public static GravityFlags ToNativeAlignment(this Font font) {
			GravityFlags flags = GravityFlags.Left;

			switch (font.Alignment) {
				case TextAlignment.Left: 		flags= GravityFlags.Left;break;
				case TextAlignment.Center: 		flags= GravityFlags.CenterHorizontal; break;
				case TextAlignment.Right: 		flags= GravityFlags.Right; break;
				case TextAlignment.Justified: 	flags = GravityFlags.Left;
												MvxBindingLog.Instance.Log(LogLevel.Warning, "Justified binding is not supported on android");break;	
			}

			return flags;
		}
	}
}
