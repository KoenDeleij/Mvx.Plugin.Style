using System;
using System.Reflection;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Plugin;
using Mvx.Plugin.Style.Converters;
using Mvx.Plugin.Style.Plugin;
using Mvx.Plugin.Style.iOS.Bindings;
using Mvx.Plugin.Style.iOS.Converters;
using UIKit;
using MvvmCross.IoC;

namespace Mvx.Plugin.Style.iOS.Plugin
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
	public class Plugin : IMvxConfigurablePlugin
	{
		private StyleConfiguration configuration;
		public void Load() {
			var instance = new TouchAssetPlugin();
			instance.Setup(configuration);
			MvxIoCProvider.Instance.RegisterSingleton<IAssetPlugin>(instance);

            MvxIoCProvider.Instance.CallbackWhenRegistered<IMvxValueConverterRegistry>(RegisterValueConverters);
            MvxIoCProvider.Instance.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterCustomBinding);
		}

		public void Configure(IMvxPluginConfiguration configuration) {
			if (configuration != null) {
				this.configuration = configuration as StyleConfiguration;
			}
		}

        /// <summary>
        /// For converting an assetplugin + font name to an actual font
        /// </summary>
        private void RegisterValueConverters()
        {
            var registry = MvxIoCProvider.Instance.Resolve<IMvxValueConverterRegistry>();
            registry.AddOrOverwriteFrom(typeof(FontResourceValueConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(AssetColorValueConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(FontLangValueConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(AttributedFontTextValueConverter).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Custom bind the controls
        /// </summary>
        private void RegisterCustomBinding()
        {
            var registry = MvxIoCProvider.Instance.Resolve<IMvxTargetBindingFactoryRegistry>();
            registry.RegisterCustomBindingFactory<UILabel>("Font", binary => new UILabelFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UILabel>("AttributedText", binary => new UILabelAttributedFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UITextView>("AttributedText", binary => new UITextViewAttributedFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UIButton>("Font", binary => new UIButtonFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UITextField>("Font", binary => new UITextFieldFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UITextView>("Font", binary => new UITextViewFontTargetBinding(binary));
        }
	}

}

