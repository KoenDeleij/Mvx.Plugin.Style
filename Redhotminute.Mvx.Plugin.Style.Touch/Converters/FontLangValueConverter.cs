using System;
using MvvmCross.Platform.Converters;
using System.Globalization;
using MvvmCross.Localization;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Exceptions;
using UIKit;
using MvvmCross.Plugins.Color.iOS;

namespace Redhotminute.Mvx.Plugin.Style.Touch
{
	public class FontLangValueConverter : MvxValueConverter {
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
			IMvxLanguageBinder textProvider = value as IMvxLanguageBinder;
			TouchAssetPlugin assetPlugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>() as TouchAssetPlugin;

			if (value!= null && parameter!= null){
				try{
					//split the text and font definition
					var values = parameter.ToString().Split(';');

					return assetPlugin.ParseToAttributedText(textProvider.GetText(values[0]), assetPlugin.GetFontByName(values[1]));
				}catch(Exception e){
					MvxBindingTrace.Trace(MvxTraceLevel.Error,
									  "Problem parsing binding {0}", e.ToLongString());
				}
			}

			return null;
		}


	}
}

