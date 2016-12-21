using System;
using System.Collections.Generic;
using MvvmCross.Binding;
using MvvmCross.Binding.Binders;
using MvvmCross.Binding.Bindings;

namespace Redhotminute.Mvx.Plugin.Style {
	public class MvxFromTextExtendedBinder :MvxFromTextBinder {
		public IEnumerable<IMvxUpdateableBinding> FontBind(object source, object target, string bindingText) {
			
			var bindingDescriptions =
				(MvxBindingSingletonCache.Instance.BindingDescriptionParser as MvxBindingDescriptionExtendedParser).FontParse(bindingText);
			return this.Bind(source, target, bindingDescriptions);
		}

	}
}
