using System;
using System.Reflection;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Plugin;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Touch.Bindings;
using Redhotminute.Mvx.Plugin.Style.Touch.Converters;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Plugin
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
	public class Plugin : IMvxConfigurablePlugin
	{
		private RedhotminuteStyleConfiguration configuration;
		public void Load() {
			var instance = new TouchAssetPlugin();
			instance.Setup(configuration);
			MvvmCross.Mvx.IoCProvider.RegisterSingleton<IAssetPlugin>(instance);

            MvvmCross.Mvx.IoCProvider.CallbackWhenRegistered<IMvxValueConverterRegistry>(RegisterValueConverters);
            MvvmCross.Mvx.IoCProvider.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterCustomBinding);
		}

		public void Configure(IMvxPluginConfiguration configuration) {
			if (configuration != null) {
				this.configuration = configuration as RedhotminuteStyleConfiguration;
			}
		}

        /// <summary>
        /// For converting an assetplugin + font name to an actual font
        /// </summary>
        private void RegisterValueConverters()
        {
            var registry = MvvmCross.Mvx.IoCProvider.Resolve<IMvxValueConverterRegistry>();
            registry.AddOrOverwriteFrom(typeof(FontResourceValueConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(AssetColorValueConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(FontLangValueConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(AttributedFontTextValueConverter).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Custom bind the Redhotminutefont to controls
        /// </summary>
        private void RegisterCustomBinding()
        {
            var registry = MvvmCross.Mvx.IoCProvider.Resolve<IMvxTargetBindingFactoryRegistry>();
            registry.RegisterCustomBindingFactory<UILabel>("Font", binary => new UILabelFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UILabel>("AttributedText", binary => new UILabelAttributedFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UITextView>("AttributedText", binary => new UITextViewAttributedFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UIButton>("Font", binary => new UIButtonFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UITextField>("Font", binary => new UITextFieldFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UITextView>("Font", binary => new UITextViewFontTargetBinding(binary));
        }
	}

}

