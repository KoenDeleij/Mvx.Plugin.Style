using System;
using UIKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Plugins.Color.iOS;
using MvvmCross.Platform;

namespace Redhotminute.Mvx.Plugin.Style.Touch {
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