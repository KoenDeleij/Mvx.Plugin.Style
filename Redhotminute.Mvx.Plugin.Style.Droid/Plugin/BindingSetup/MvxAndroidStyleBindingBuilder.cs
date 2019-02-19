using MvvmCross.Binding.Binders;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.Binders;
using Redhotminute.Mvx.Plugin.Style.Bindings;

namespace Redhotminute.Mvx.Plugin.Style.Droid.BindingSetup
{
    public class MvxAndroidStyleBindingBuilder : MvxAndroidBindingBuilder {

		//NOTE registering the custom bindings can be moved from plugin to this setup

		protected override IMvxAndroidViewBinderFactory CreateAndroidViewBinderFactory() {
			return new MvxAndroidStyleViewBinderFactory();
		}

		protected override void InitializeBindingResources() {
			MvxAndroidStyleBindingResource.Initialize();
		}

		protected override void RegisterCore() {
			base.RegisterCore();
			//override the IMvxBinder by a custom one providing a fontbinding
			MvvmCross.Mvx.IoCProvider.RegisterSingleton<IMvxBinder>(new MvxFromTextExtendedBinder());
		}

		protected override void RegisterBindingDescriptionParser() {
			MvvmCross.Mvx.IoCProvider.RegisterSingleton<IMvxBindingDescriptionParser>(new MvxBindingDescriptionExtendedParser());
		}
	}
}
