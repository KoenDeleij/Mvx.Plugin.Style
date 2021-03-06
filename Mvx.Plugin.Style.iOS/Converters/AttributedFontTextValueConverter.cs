using System;
using MvvmCross.Converters;
using System.Globalization;
using MvvmCross.Binding;
using MvvmCross;
using MvvmCross.Exceptions;
using Mvx.Plugin.Style.Plugin;
using Mvx.Plugin.Style.iOS.Plugin;
using Foundation;
using UIKit;
using MvvmCross.IoC;

namespace Mvx.Plugin.Style.iOS.Converters
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

		private object ConvertValue(object value,object parameter) 
        {
			TouchAssetPlugin assetPlugin = MvxIoCProvider.Instance.Resolve<IAssetPlugin>() as TouchAssetPlugin;

			if (value!= null && parameter!= null)
            {
				try
                {
                    var stringValue = value.ToString();
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        return string.Empty;
                    }
					var text = assetPlugin.ParseToAttributedText(value.ToString(), assetPlugin.GetFontByName(parameter.ToString()));
                    return text;
				}
                catch(Exception e)
                {
                    MvxBindingLog.Error("Problem parsing binding {0}", e.ToLongString());
				}
			}

			return null;
		}
	}
}

