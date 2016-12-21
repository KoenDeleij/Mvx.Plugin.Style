using System;
using Android.Graphics;
using Android.Text;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Color.Droid;

namespace Redhotminute.Mvx.Plugin.Style.Droid {
	public class TextViewAttributedTargetBinding
		: MvxConvertingTargetBinding {

		protected TextView tv => Target as TextView;

		public TextViewAttributedTargetBinding(TextView target)
			: base(target) {
			if (target == null) {
				MvxBindingTrace.Trace(MvxTraceLevel.Error, "Error - textview is null in TextViewAttributedTargetBinding");
				return;
			}
		}

		protected override void SetValueImpl(object target, object toSet) {
			var label = (TextView)target;
			var spannable = toSet as SpannableString;
			if (spannable != null) {
				label.TextFormatted = toSet as SpannableString;
			}
			else {
				label.Text = toSet.ToString();
			}
		}

		public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

		public override Type TargetType {
			get {
				return typeof(SpannableString);
			}
		}
	}
}

