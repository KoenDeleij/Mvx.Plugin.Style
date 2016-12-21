using System;
using MvvmCross.Platform.Plugins;

namespace Redhotminute.Mvx.Plugin.Style.Droid
{
	public class Plugin : IMvxPlugin
	{
		private RedhotminuteStyleConfiguration configuration;
		public void Load()
		{
			var instance = new DroidAssetPlugin();
			MvvmCross.Platform.Mvx.RegisterSingleton<IAssetPlugin>(instance);
		}

		public void Configure(IMvxPluginConfiguration configuration) {
			if (configuration != null) {
				this.configuration = configuration as RedhotminuteStyleConfiguration;
			}
		}
	}
}

