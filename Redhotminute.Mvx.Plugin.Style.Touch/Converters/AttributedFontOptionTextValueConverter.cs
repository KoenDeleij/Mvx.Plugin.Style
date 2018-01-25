using System;
using System.Globalization;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Touch.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Converters
{
    public class AttributedFontOptionTextValueConverter : MvxValueConverter
    {
        /// <summary>
        /// Convert the specified value, targetType, parameter and culture.
        /// </summary>
        /// <param name="value">AssetPlugin</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Font name</param>
        /// <param name="culture">Culture.</param>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertValue(value, parameter);
        }

        private object ConvertValue(object value, object parameter)
        {
            TouchAssetPlugin assetPlugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>() as TouchAssetPlugin;

            if (value != null && parameter != null)
            {
                try
                {
                    if (parameter is AttributedFontOption options)
                    {
                        return assetPlugin.ParseToAttributedText(value.ToString(), assetPlugin.GetFontByName(options.FontName.ToString()), options.Options);
                    }
                    else
                    {
                        throw new Exception("option is null");
                    }
                }
                catch (Exception e)
                {
                    MvxBindingTrace.Trace(MvxTraceLevel.Error, "Problem parsing binding {0}", e.ToLongString());
                }
            }

            return null;
        }
    }

    public class AttributedFontOption
    {
        public string FontName
        {
            get;
            set;
        }

        public NSAttributedStringDocumentAttributes Options
        {
            get;
            set;
        }
    }
}
