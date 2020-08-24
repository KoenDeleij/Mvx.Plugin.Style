using MvvmCross.Binding.Binders;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.Binders;
using MvvmCross.Platforms.Android.Binding.ResourceHelpers;
using Mvx.Plugin.Style.Bindings;

namespace Mvx.Plugin.Style.Droid.BindingSetup
{
    public class MvxAndroidStyleBindingBuilder : MvxAndroidBindingBuilder {

		protected override IMvxAndroidViewBinderFactory CreateAndroidViewBinderFactory() {
			return new MvxAndroidStyleViewBinderFactory();
		}

        protected override IMvxAndroidBindingResource CreateAndroidBindingResource()
        {
			return new MvxAndroidStyleBindingResource();
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
