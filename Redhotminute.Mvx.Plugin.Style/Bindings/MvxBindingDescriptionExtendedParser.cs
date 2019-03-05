using System.Collections.Generic;
using MvvmCross.Binding.Binders;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Parse.Binding;

namespace Redhotminute.Mvx.Plugin.Style.Bindings {
	public class MvxBindingDescriptionExtendedParser :MvxBindingDescriptionParser, IMvxBindingDescriptionParser{

		private IMvxFontBindingParser _fontBindingParser;

		protected IMvxFontBindingParser FontBindingParser {
			get {
				this._fontBindingParser = this._fontBindingParser ?? MvvmCross.Mvx.IoCProvider.Resolve<IMvxFontBindingParser>();
				return this._fontBindingParser;
			}
		}

		public IEnumerable<MvxBindingDescription> FontParse(string text) {
			var parser = this.FontBindingParser;
			return this.Parse(text, parser);
		}
	}
}
