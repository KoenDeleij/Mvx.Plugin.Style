using System;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.Platforms.Ios.Core;
using UIKit;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.ViewModels;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Serilog;

namespace Mvx.Plugin.Style.SampleApp.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        
		protected override IMvxPluginConfiguration GetPluginConfiguration(Type plugin) {
		    if (plugin == typeof(Mvx.Plugin.Style.iOS.Plugin.Plugin)) {
                return new Mvx.Plugin.Style.Plugin.StyleConfiguration()
                {
                    FontSizeFactor = 1.0f,
                    LineHeightFactor = 1.0f
				};
			}
			return base.GetPluginConfiguration(plugin);
		}

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions()
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                // add more sinks here
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

    }
}
