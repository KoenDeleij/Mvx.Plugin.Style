using System;
using MvvmCross.Binding.Droid.Binders;

namespace Redhotminute.Mvx.Plugin.Style.Droid {
	public class MvxAndroidStyleViewBinderFactory : IMvxAndroidViewBinderFactory {
		public IMvxAndroidViewBinder Create(object source) {
			return new MvxAndroidStyleViewBinder(source);
		}
	}
}
