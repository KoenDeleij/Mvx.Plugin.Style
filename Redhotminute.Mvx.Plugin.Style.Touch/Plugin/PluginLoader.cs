using System;
using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platform.Converters;
using System.Reflection;
using UIKit;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Touch.Converters;
using Redhotminute.Mvx.Plugin.Style.Touch.Bindings;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Plugin
{
	public class PluginLoader :IMvxPluginLoader
	{
		public static readonly PluginLoader Instance = new PluginLoader();

		public void EnsureLoaded()
		{
			var manager = MvvmCross.Platform.Mvx.Resolve<IMvxPluginManager>();
			manager.TryEnsurePlatformAdaptionLoaded<PluginLoader>();

			MvvmCross.Platform.Mvx.CallbackWhenRegistered<IMvxValueConverterRegistry>(RegisterValueConverters);
			MvvmCross.Platform.Mvx.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry> (RegisterCustomBinding);
		}

		/// <summary>
		/// For converting a assetplugin + font name to an actual font
		/// </summary>
		private void RegisterValueConverters()
		{
			var registry = MvvmCross.Platform.Mvx.Resolve<IMvxValueConverterRegistry>();
			registry.AddOrOverwriteFrom(typeof(FontResourceValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AssetColorValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(FontLangValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AttributedFontTextValueConverter).GetTypeInfo().Assembly);
		}

		/// <summary>
		/// Custom bind the Redhotminutefont to controls
		/// </summary>
		private void RegisterCustomBinding(){
			var registry = MvvmCross.Platform.Mvx.Resolve<IMvxTargetBindingFactoryRegistry>();
			registry.RegisterCustomBindingFactory<UILabel>("Font",binary => new UILabelFontTargetBinding(binary) );
			registry.RegisterCustomBindingFactory<UILabel>("AttributedText", binary => new UILabelAttributedFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UITextView>("AttributedText", binary => new UITextViewAttributedFontTargetBinding(binary));
            registry.RegisterCustomBindingFactory<UIButton>("Font", binary => new UIButtonFontTargetBinding(binary));
			registry.RegisterCustomBindingFactory<UITextField>("Font", binary => new UITextFieldFontTargetBinding(binary));
			registry.RegisterCustomBindingFactory<UITextView>("Font", binary => new UITextViewFontTargetBinding(binary));
		}
	}
}






//