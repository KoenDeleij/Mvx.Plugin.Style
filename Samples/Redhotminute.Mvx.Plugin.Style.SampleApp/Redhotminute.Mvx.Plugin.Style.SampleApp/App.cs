using MvvmCross.Platform.IoC;
using MvvmCross.Platform.UI;

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

			plugin.AddFont(new BaseFont() { Name = "BoldFont", FontFilename = "OpenSans-Semibold.ttf", FontPlatformName = "OpenSans-Semibold", Size = 20, Color = new MvxColor(255, 0, 0) });
			plugin.AddFont(new Font() { Name = "RegularFont", FontFilename = "OpenSans-Regular.ttf", FontPlatformName = "OpenSans", Size = 16, LineHeight = 28, Color = new MvxColor(255, 255, 255), BoldFont = plugin.GetFont("BoldFont") });
			//add your fonts here. of course wouldn't actually add them here, but after the plugins are loaded you could.
		}
    }
}
