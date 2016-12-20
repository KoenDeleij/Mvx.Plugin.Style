using System;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace Redhotminute.Plugin.Style.Droid
{
	public class Plugin : IMvxPlugin
	{
		private RedhotminuteStyleConfiguration configuration;
		public void Load()
		{
			var instance = new DroidAssetPlugin();
			Mvx.RegisterSingleton<IAssetPlugin>(instance);
		}

		public void Configure(IMvxPluginConfiguration configuration) {
			if (configuration != null) {
				this.configuration = configuration as RedhotminuteStyleConfiguration;
			}
		}
	}
}

