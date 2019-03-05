using System;
using System.Reflection;
using Android.Widget;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Plugin;
using Redhotminute.Mvx.Plugin.Style.Bindings;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Droid.Bindings;
using Redhotminute.Mvx.Plugin.Style.Droid.Converters;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Plugin
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
	public class Plugin : IMvxConfigurablePlugin
	{
        private RedhotminuteStyleConfiguration configuration;

        public void Load()
        {
            var instance = new DroidAssetPlugin();
            instance.Setup(configuration);
            MvvmCross.Mvx.IoCProvider.RegisterSingleton<IAssetPlugin>(instance);

            MvvmCross.Mvx.IoCProvider.CallbackWhenRegistered<IMvxValueConverterRegistry>(RegisterValueConverters);
            MvvmCross.Mvx.IoCProvider.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterCustomBinding);
            MvvmCross.Mvx.IoCProvider.RegisterSingleton<IMvxFontBindingParser>(new MvxFontBindingParser());
        }

        public void Configure(IMvxPluginConfiguration configuration)
        {
            if (configuration != null)
            {
                this.configuration = configuration as RedhotminuteStyleConfiguration;
            }
        }

        private void RegisterValueConverters()
        {
            var registry = MvvmCross.Mvx.IoCProvider.Resolve<IMvxValueConverterRegistry>();
            registry.AddOrOverwriteFrom(typeof(FontResourceValueConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(AttributedTextConverter).GetTypeInfo().Assembly);
            registry.AddOrOverwriteFrom(typeof(AssetColorValueConverter).GetTypeInfo().Assembly);
        }

        private void RegisterCustomBinding()
        {
            var registry = MvvmCross.Mvx.IoCProvider.Resolve<IMvxTargetBindingFactoryRegistry>();
            registry.RegisterCustomBindingFactory<TextView>("AttributedText", textView => new TextViewAttributedTargetBinding(textView));
            registry.RegisterCustomBindingFactory<TextView>("Font", textView => new TextViewFontTargetBinding(textView));
            registry.RegisterCustomBindingFactory<EditText>("Font", textView => new TextViewFontTargetBinding(textView));
            registry.RegisterCustomBindingFactory<Button>("Font", button => new ButtonFontTargetBinding(button));
        }

    }
}

