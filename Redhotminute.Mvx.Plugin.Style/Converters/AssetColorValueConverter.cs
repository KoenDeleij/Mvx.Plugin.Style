using System;
using MvvmCross.Platform.Converters;
using System.Globalization;
using MvvmCross.Platform.UI;
using MvvmCross.Binding;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Converters
{
	public class AssetColorValueConverter : MvxValueConverter {

		private IMvxNativeColor _nativeColor;
		private IMvxNativeColor NativeColor => _nativeColor ?? (_nativeColor = MvvmCross.Platform.Mvx.Resolve<IMvxNativeColor>());

        private IAssetPlugin _plugin;
		public override object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertValue(value, parameter,culture);
		}

		private object ConvertValue(object value,object parameter,CultureInfo culture) {
            if (value is IAssetPlugin){
                _plugin = value as AssetPlugin;
            }else if(_plugin == null){
				//try to resolve it. Not ideal but sometimes necessary within simpel cells
				_plugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();

				MvxBindingTrace.Trace("AssetProvider not available for Color conversion. Resolved it");
			}

			if (_plugin != null ){
				try{
                    if (parameter != null)
                    {
                        return GetColorByName(parameter.ToString());
                    }else if (value != null && value is string){
						return GetColorByName(value.ToString());
                    }
				}catch{
					MvxBindingTrace.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Warning, $"Failed to convert color");
				}
			}

			return null;
		}

        private object GetColorByName(string colorName){
			var color = _plugin.GetColor(colorName);
			if (color != null)
			{
				return NativeColor.ToNative(color);
			}
            return null;
        } 
	}

}

