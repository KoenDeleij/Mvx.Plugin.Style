using System;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.Platforms.Ios.Core;
using UIKit;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.ViewModels;

namespace Mvx.Plugin.Style.SampleApp.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        
		protected override IMvxPluginConfiguration GetPluginConfiguration(Type plugin) {
		    if (plugin == typeof(Mvx.Plugin.Style.iOS.Plugin.Plugin)) {
                return new Mvx.Plugin.Style.Plugin.RedhotminuteStyleConfiguration()
                {
                    FontSizeFactor = 1.0f,
                    LineHeightFactor = 1.0f
				};
			}
			return base.GetPluginConfiguration(plugin);
		}


        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions()
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }
    }
}
