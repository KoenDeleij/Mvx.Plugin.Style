using MvvmCross.Platform.IoC;
using MvvmCross.Platform.UI;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.FirstViewModel>();
        }

		public override void LoadPlugins(MvvmCross.Platform.Plugins.IMvxPluginManager pluginManager) {
			base.LoadPlugins(pluginManager);
			var plugin = MvvmCross.Platform.Mvx.Resolve<IAssetPlugin>();

			//plugin.LoadJsonFontFile("textstyles.json");
		}
    }
}
