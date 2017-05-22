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
			IAssetPlugin plugin = value as AssetPlugin;

			if (plugin == null) {
				//try to resolve it. Not ideal but sometimes necessary within simpel cells
				plugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();

				MvxBindingTrace.Trace("AssetProvider not available for Color conversion. Resolved it");
			}

			if (plugin!= null && parameter!= null){
				try{
					string colorName = parameter.ToString();
					var color = plugin.GetColor(colorName);
					if (color != null) {
						return NativeColor.ToNative(color);
					}
				}catch{
					MvxBindingTrace.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Warning, $"Failed to convert color");
				}
			}

			return null;
		}
	}

}

