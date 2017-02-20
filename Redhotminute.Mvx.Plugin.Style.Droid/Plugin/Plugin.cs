using System;
using MvvmCross.Platform.Plugins;

namespace Redhotminute.Mvx.Plugin.Style.Droid
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

