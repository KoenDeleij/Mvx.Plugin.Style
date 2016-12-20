using System;
using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platform.Converters;
using System.Reflection;
using UIKit;
using MvvmCross.Binding.Bindings.Source.Construction;
using MvvmCross.Binding.Binders;
using Redhotminute.Plugin.Style.Touch;

namespace Redhotminute.Plugin.Style.Touch
{
	public class PluginLoader :IMvxPluginLoader
	{
		public static readonly PluginLoader Instance = new PluginLoader();

		public void EnsureLoaded()
		{
			var manager = Mvx.Resolve<IMvxPluginManager>();
			manager.TryEnsurePlatformAdaptionLoaded<PluginLoader>();

			Mvx.CallbackWhenRegistered<IMvxValueConverterRegistry>(RegisterValueConverters);
			Mvx.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry> (RegisterCustomBinding);
			//Mvx.CallbackWhenRegistered<IMvxSourceBindingFactoryExtensionHost>(RegisterBindingExtensions);
		}

		/// <summary>
		/// For converting a assetplugin + font name to an actual font
		/// </summary>
		private void RegisterValueConverters()
		{
			var registry = Mvx.Resolve<IMvxValueConverterRegistry>();
			registry.AddOrOverwriteFrom(typeof(FontResourceValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AssetColorValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(FontLangValueConverter).GetTypeInfo().Assembly);
			registry.AddOrOverwriteFrom(typeof(AttributedFontTextValueConverter).GetTypeInfo().Assembly);
		}

		/// <summary>
		/// Custom bind the Redhotminutefont to controls
		/// </summary>
		private void RegisterCustomBinding(){
			var registry = Mvx.Resolve<IMvxTargetBindingFactoryRegistry>();
			registry.RegisterCustomBindingFactory<UILabel>("Font",binary => new UILabelFontTargetBinding(binary) );
			registry.RegisterCustomBindingFactory<UILabel>("AttributedText", binary => new UILabelAttributedFontTargetBinding(binary));
			registry.RegisterCustomBindingFactory<UIButton>("Font", binary => new UIButtonFontTargetBinding(binary));
			registry.RegisterCustomBindingFactory<UITextField>("Font", binary => new UITextFieldFontTargetBinding(binary));
		}
		/*
		private void RegisterBindingExtensions(IMvxSourceBindingFactoryExtensionHost host)
		{
			host.Extensions.Add(new MvxFieldSourceBindingFactoryExtension());
		}*/
	}
}






//