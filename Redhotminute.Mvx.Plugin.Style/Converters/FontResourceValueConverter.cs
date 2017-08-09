using System;
using MvvmCross.Platform.Converters;
using System.Globalization;


namespace Redhotminute.Mvx.Plugin.Style
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

		private object ConvertValue(object value,object parameter) {
			AssetPlugin plugin = value as AssetPlugin;

			if (plugin == null) {
				plugin = (Redhotminute.Mvx.Plugin.Style.AssetPlugin)MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();
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
					MvvmCross.Platform.Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, $"Failed to get font {e.Message}");
				}
			}

			return null;
		}
	}
}

