using MvvmCross.Binding.Binders;
using MvvmCross.IoC;
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

        protected override void RegisterCore(IMvxIoCProvider provider) {
			base.RegisterCore(provider);
			//override the IMvxBinder by a custom one providing a fontbinding
			MvxIoCProvider.Instance.RegisterSingleton<IMvxBinder>(new MvxFromTextExtendedBinder());
		}

		protected override void RegisterBindingDescriptionParser(IMvxIoCProvider provider) {
			MvxIoCProvider.Instance.RegisterSingleton<IMvxBindingDescriptionParser>(new MvxBindingDescriptionExtendedParser());
		}
	}
}
