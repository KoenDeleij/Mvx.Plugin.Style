using System;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace Redhotminute.Plugin.Style.Touch
{
	public class Plugin : IMvxPlugin
	{
		private RedhotminuteStyleConfiguration configuration;
		public void Load() {
			var instance = new TouchAssetPlugin();
			Mvx.RegisterSingleton<IAssetPlugin>(instance);
		}

		public void Configure(IMvxPluginConfiguration configuration) {
			if (configuration != null) {
				this.configuration = configuration as RedhotminuteStyleConfiguration;
			}
		}
	}
}

