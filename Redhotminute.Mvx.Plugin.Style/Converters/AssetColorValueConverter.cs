using System;
using MvvmCross.Platform.Converters;
using System.Globalization;
using MvvmCross.Platform.UI;
using MvvmCross.Platform;
using MvvmCross.Binding;

namespace Redhotminute.Mvx.Plugin.Style
{
	public class AssetColorValueConverter : MvxValueConverter {

		private IMvxNativeColor _nativeColor;
		private IMvxNativeColor NativeColor => _nativeColor ?? (_nativeColor = MvvmCross.Platform.Mvx.Resolve<IMvxNativeColor>());

		public override object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertValue(value, parameter,culture);
		}

		private object ConvertValue(object value,object parameter,CultureInfo culture) {
			AssetPlugin plugin = value as AssetPlugin;

			if (value != null && parameter!= null){
				try{
					string colorName = parameter.ToString();
					var color = plugin.GetColor(colorName);
					if (color != null) {
						return NativeColor.ToNative(color);
					}
				}catch{
					MvxBindingTrace.Trace("Failed to find and convert color");
				}
			}

			return null;
		}
	}
}

