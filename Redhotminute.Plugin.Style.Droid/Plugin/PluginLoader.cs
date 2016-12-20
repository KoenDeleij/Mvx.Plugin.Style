using System;
using System.Reflection;
using Android.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Plugins;
using Redhotminute.Plugin.Style.Droid;

namespace Redhotminute.Plugin.Style {
	public class PluginLoader : IMvxPluginLoader {
		public static readonly PluginLoader Instance = new PluginLoader();

		public void EnsureLoaded() {
			var manager = Mvx.Resolve<IMvxPluginManager>();
			manager.TryEnsurePlatformAdaptionLoaded<PluginLoader>();

			Mvx.CallbackWhenRegistered<IMvxValueConverterRegistry>(RegisterValueConverters);
			Mvx.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterCustomBinding);

			Mvx.RegisterSingleton<IMvxFontBindingParser>(new MvxFontBindingParser());
		}

		private void RegisterValueConverters() {
			var registry = Mvx.Resolve<IMvxValueConverterRegistry>();
			registry.AddOrOverwriteFrom(typeof(FontResourceValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AttributedBoldValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AssetColorValueConverter).GetTypeInfo().Assembly);
		}

		private void RegisterCustomBinding() {
			var registry = Mvx.Resolve<IMvxTargetBindingFactoryRegistry>();
			registry.RegisterCustomBindingFactory<TextView>("AttributedText", textView => new TextViewAttributedTargetBinding(textView));
			registry.RegisterCustomBindingFactory<TextView>("Font", textView => new TextViewFontTargetBinding(textView));
			registry.RegisterCustomBindingFactory<EditText>("Font", textView => new TextViewFontTargetBinding(textView));
			registry.RegisterCustomBindingFactory<Button>("Font", button => new ButtonFontTargetBinding(button));
		}
	}
}
