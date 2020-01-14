using System;
using MvvmCross.Base;
using MvvmCross.Binding;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Binding.ResourceHelpers;

namespace Redhotminute.Mvx.Plugin.Style.Droid.BindingSetup {
	
    public class MvxAndroidStyleBindingResource : MvxAndroidBindingResource
    {
        public int BindingFontId => Resource.Styleable.MvxBinding_MvxFont;
    }
}
