using System;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Plugin
{
	public class Plugin : IMvxConfigurablePlugin
	{
		private RedhotminuteStyleConfiguration configuration;
		public void Load() {
			var instance = new TouchAssetPlugin();
			instance.Setup(configuration);
			MvvmCross.Platform.Mvx.RegisterSingleton<IAssetPlugin>(instance);
		}

		public void Configure(IMvxPluginConfiguration configuration) {
			if (configuration != null) {
				this.configuration = configuration as RedhotminuteStyleConfiguration;
			}
		}
	}
}

