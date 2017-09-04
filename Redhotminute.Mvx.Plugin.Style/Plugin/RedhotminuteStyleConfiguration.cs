namespace Redhotminute.Mvx.Plugin.Style.Plugin {
	using System;
	using MvvmCross.Platform.Plugins;

	public class RedhotminuteStyleConfiguration : IMvxPluginConfiguration {
		public float? FontSizeFactor { get; set; }
		public float? LineHeightFactor { get; set; }
	}
}
