using System;
using MvvmCross.Platform.Plugins;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Plugin
{
	public class Plugin : IMvxConfigurablePlugin
	{
		private RedhotminuteStyleConfiguration configuration;
		public void Load()
		{
			var instance = new DroidAssetPlugin();
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

