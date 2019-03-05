using System;
using UIKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Plugin.Color.Platforms.Ios;
using MvvmCross;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Bindings {
	public class BaseStyleBinding<T> : MvxConvertingTargetBinding {
		protected T TypedTarget {
			get { return (T)Target; }
		}

		public BaseStyleBinding(T target)
			: base(target) {
		}

		protected override void SetValueImpl(object target, object value) {
			
		}

		public override Type TargetType {
			get { return typeof(int); }
		}

		public override MvxBindingMode DefaultMode {
			get { return MvxBindingMode.OneWay; }
		}
	}
}