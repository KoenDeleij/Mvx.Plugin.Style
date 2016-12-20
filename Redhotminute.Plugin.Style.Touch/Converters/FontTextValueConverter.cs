﻿using System;
using MvvmCross.Platform.Converters;
using System.Globalization;
using MvvmCross.Platform;
using MvvmCross.Localization;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Exceptions;
using UIKit;
using MvvmCross.Plugins.Color.iOS;

namespace Redhotminute.Plugin.Style.Touch
{
	public class AttributedFontTextValueConverter : MvxValueConverter {
		/// <summary>
		/// Convert the specified value, targetType, parameter and culture.
		/// </summary>
		/// <param name="value">AssetPlugin</param>
		/// <param name="targetType">Target type.</param>
		/// <param name="parameter">Font name</param>
		/// <param name="culture">Culture.</param>
		public override object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertValue(value, parameter);
		}

		private object ConvertValue(object value,object parameter) {
			TouchAssetPlugin assetPlugin = Mvx.Resolve<IAssetPlugin>() as TouchAssetPlugin;

			if (value!= null && parameter!= null){
				try{
					return assetPlugin.ParseToAttributedText(value.ToString(), assetPlugin.GetFont(parameter.ToString()));
				}catch(Exception e){
					MvxBindingTrace.Trace(MvxTraceLevel.Error,
									  "Problem parsing binding {0}", e.ToLongString());
				}
			}

			return null;
		}


	}
}

