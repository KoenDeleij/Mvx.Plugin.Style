using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Binding;
using MvvmCross.Binding.Binders;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Parse.Binding;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace Redhotminute.Plugin.Style {
	public class MvxBindingDescriptionExtendedParser :MvxBindingDescriptionParser, IMvxBindingDescriptionParser{
		public MvxBindingDescriptionExtendedParser() {
		}

		private IMvxFontBindingParser _fontBindingParser;

		protected IMvxFontBindingParser FontBindingParser {
			get {
				this._fontBindingParser = this._fontBindingParser ?? Mvx.Resolve<IMvxFontBindingParser>();
				return this._fontBindingParser;
			}
		}

		public IEnumerable<MvxBindingDescription> FontParse(string text) {
			var parser = this.FontBindingParser;
			return this.Parse(text, parser);
		}
	}
}
