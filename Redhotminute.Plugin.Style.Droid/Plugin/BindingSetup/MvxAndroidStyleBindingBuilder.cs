using System;
using MvvmCross.Binding.Binders;
using MvvmCross.Binding.Droid;
using MvvmCross.Binding.Parse.Binding;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;

namespace Redhotminute.Plugin.Style.Droid {
	public class MvxAndroidStyleBindingBuilder : MvxAndroidBindingBuilder {

		//NOTE registering the custom bindings can be moved from plugin to this setup

		protected override MvvmCross.Binding.Droid.Binders.IMvxAndroidViewBinderFactory CreateAndroidViewBinderFactory() {
			return new MvxAndroidStyleViewBinderFactory();
		}

		protected override void InitializeBindingResources() {
			MvxAndroidStyleBindingResource.Initialize();
		}

		protected override void RegisterCore() {
			base.RegisterCore();
			//override the IMvxBinder by a custom one providing a fontbinding
			Mvx.RegisterSingleton<IMvxBinder>(new MvxFromTextExtendedBinder());
		}

		protected override void RegisterBindingDescriptionParser() {
			Mvx.RegisterSingleton<IMvxBindingDescriptionParser>(new MvxBindingDescriptionExtendedParser());
		}
	}
}
