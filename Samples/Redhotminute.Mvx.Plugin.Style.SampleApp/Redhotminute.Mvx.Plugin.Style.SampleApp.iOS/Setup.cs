using System;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.Platforms.Ios.Core;
using UIKit;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.ViewModels;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        
		protected override IMvxPluginConfiguration GetPluginConfiguration(Type plugin) {
		    if (plugin == typeof(Redhotminute.Mvx.Plugin.Style.Touch.Plugin.Plugin)) {
                return new Redhotminute.Mvx.Plugin.Style.Plugin.RedhotminuteStyleConfiguration()
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
