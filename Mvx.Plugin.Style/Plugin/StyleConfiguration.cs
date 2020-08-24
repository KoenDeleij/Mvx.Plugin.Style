using MvvmCross.Plugin;

namespace Mvx.Plugin.Style.Plugin
{
    public class StyleConfiguration : IMvxPluginConfiguration {
		public float? FontSizeFactor { get; set; }
		public float? LineHeightFactor { get; set; }
	}
}
