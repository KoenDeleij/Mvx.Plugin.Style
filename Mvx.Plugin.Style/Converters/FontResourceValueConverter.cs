using System;
using System.Globalization;
using MvvmCross.Converters;
using MvvmCross.IoC;
using MvvmCross.Logging;
using Mvx.Plugin.Style.Plugin;

namespace Mvx.Plugin.Style.Converters
{
    public class FontResourceValueConverter : MvxValueConverter {
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

		protected object ConvertValue(object value,object parameter) {
			AssetPlugin plugin = value as AssetPlugin;

			if (plugin == null) {
				plugin = (AssetPlugin)MvxIoCProvider.Instance.Resolve<IAssetPlugin>();
			}

			if (parameter!= null){
				try{
					string fontName = parameter.ToString();

                    //if a string is bound, could be a color name, add it
                    if(value is string && !string.IsNullOrEmpty((string)value)){
                        fontName = $"{fontName}:{value}";
                    }

					var font = plugin.GetFontByName(fontName);
					return font;
				}catch(Exception e){
                    MvxPluginLog.Instance.Error(e, $"Failed to get font {e.Message}");
				}
			}

			return null;
		}
	}
}

