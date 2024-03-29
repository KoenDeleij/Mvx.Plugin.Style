using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using MvvmCross.Converters;
using MvvmCross.IoC;
using MvvmCross.Logging;
using MvvmCross.UI;
using Mvx.Plugin.Style.Plugin;

namespace Mvx.Plugin.Style.Converters
{
    public class AssetColorValueConverter : MvxValueConverter {

		private IMvxNativeColor _nativeColor;
        private IMvxNativeColor NativeColor => _nativeColor ?? (_nativeColor = MvxIoCProvider.Instance.Resolve<IMvxNativeColor>());

        private IAssetPlugin _plugin;
		public override object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertValue(value, parameter,culture);
		}

		private object ConvertValue(object value,object parameter,CultureInfo culture) 
        {
            if (value is IAssetPlugin)
            {
                _plugin = value as AssetPlugin;
            }
            else if(_plugin == null)
            {
				//try to resolve it. Not ideal but sometimes necessary within simpel cells
                _plugin = MvxIoCProvider.Instance.Resolve<IAssetPlugin>();

                MvxPluginLog.Instance.Log(LogLevel.Trace, "AssetProvider not available for Color conversion. Resolved it");
			}

			if (_plugin != null )
            {
				try
                {
                    if (parameter != null)
                    {
                        return GetColorByName(parameter.ToString());
                    }

                    if (value != null && value is string)
                    {
						return GetColorByName(value.ToString());
                    }
				}
                catch
                {
                    MvxPluginLog.Instance.Log(LogLevel.Warning, $"Failed to convert '{(parameter?.ToString() ?? value?.ToString())}' into a color");
				}
			}

			return null;
		}

        private object GetColorByName(string colorName)
        {
			var color = _plugin.GetColor(colorName);
			if (color != System.Drawing.Color.Empty)
			{
				return NativeColor.ToNative(color);
			}
            return null;
        } 
	}

}

