using System;
using MvvmCross.Platforms.Android.Binding.Binders;

namespace Mvx.Plugin.Style.Droid.BindingSetup {
	public class MvxAndroidStyleViewBinderFactory : IMvxAndroidViewBinderFactory {
		public IMvxAndroidViewBinder Create(object source) {
			return new MvxAndroidStyleViewBinder(source);
		}
	}
}
