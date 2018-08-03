using System;
using MvvmCross.Converters;
using System.Globalization;
using MvvmCross.Binding;
using MvvmCross;
using MvvmCross.Exceptions;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Touch.Plugin;
using Foundation;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Converters
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
			TouchAssetPlugin assetPlugin = MvvmCross.Mvx.Resolve<IAssetPlugin>() as TouchAssetPlugin;

			if (value!= null && parameter!= null){
				try{
                    var stringValue = value.ToString();
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        return string.Empty;
                    }
					var text = assetPlugin.ParseToAttributedText(value.ToString(), assetPlugin.GetFontByName(parameter.ToString()));
                    return text;
				}catch(Exception e){
                    MvxBindingLog.Error("Problem parsing binding {0}", e.ToLongString());
				}
			}

			return null;
		}
	}
}

