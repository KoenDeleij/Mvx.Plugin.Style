using Android.Content;
using Mvx.Plugin.Style.Droid;
using MvvmCross.IoC;
using Mvx.Plugin.Style.Droid.BindingSetup;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.ViewModels;
using MvvmCross.Binding;
using MvvmCross.Logging;
using Mvx.Plugin.Style.Plugin;
using MvvmCross.Core;
using System.Reflection;
using System.Linq;

namespace Mvx.Plugin.Style.SampleApp.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        public override Assembly ExecutableAssembly => ViewAssemblies?.FirstOrDefault() ?? GetType().Assembly;
    

		protected override MvvmCross.Plugin.IMvxPluginConfiguration GetPluginConfiguration(System.Type plugin) {
			if (plugin == typeof(Mvx.Plugin.Style.Droid.Plugin.Plugin)) {
                return new RedhotminuteStyleConfiguration() {
					FontSizeFactor = 1.0f,
					LineHeightFactor = 1.0f
				};
			}

			return base.GetPluginConfiguration(plugin);
		}

        protected override MvxBindingBuilder CreateBindingBuilder()
        {
            var bindingBuilder = new MvxAndroidStyleBindingBuilder();
            return bindingBuilder;
        }

        protected override IMvxIocOptions CreateIocOptions() {
			return new MvxIocOptions() {
				PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
			};
		}

    }
}
