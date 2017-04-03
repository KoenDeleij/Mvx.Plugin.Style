using System;
using System.Reflection;
using Android.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Plugins;

namespace Redhotminute.Mvx.Plugin.Style.Droid {
	public class PluginLoader : IMvxPluginLoader {
		public static readonly PluginLoader Instance = new PluginLoader();

		public void EnsureLoaded() {
			var manager = MvvmCross.Platform.Mvx.Resolve<IMvxPluginManager>();
			manager.TryEnsurePlatformAdaptionLoaded<PluginLoader>();

			MvvmCross.Platform.Mvx.CallbackWhenRegistered<IMvxValueConverterRegistry>(RegisterValueConverters);
			MvvmCross.Platform.Mvx.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterCustomBinding);

			MvvmCross.Platform.Mvx.RegisterSingleton<IMvxFontBindingParser>(new MvxFontBindingParser());
		}

		private void RegisterValueConverters() {
			var registry = MvvmCross.Platform.Mvx.Resolve<IMvxValueConverterRegistry>();
			registry.AddOrOverwriteFrom(typeof(FontResourceValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AttributedTextConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AssetColorValueConverter).GetTypeInfo().Assembly);
		}

		private void RegisterCustomBinding() {
			var registry = MvvmCross.Platform.Mvx.Resolve<IMvxTargetBindingFactoryRegistry>();
			registry.RegisterCustomBindingFactory<TextView>("AttributedText", textView => new TextViewAttributedTargetBinding(textView));
			registry.RegisterCustomBindingFactory<TextView>("Font", textView => new TextViewFontTargetBinding(textView));
			registry.RegisterCustomBindingFactory<EditText>("Font", textView => new TextViewFontTargetBinding(textView));
			registry.RegisterCustomBindingFactory<Button>("Font", button => new ButtonFontTargetBinding(button));
		}
	}
}
