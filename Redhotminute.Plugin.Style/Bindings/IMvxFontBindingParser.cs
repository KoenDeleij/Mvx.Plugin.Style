using System;
using MvvmCross.Binding.Parse.Binding;

namespace Redhotminute.Plugin.Style {

	public interface IMvxFontBindingParser
		: IMvxBindingParser {
		string DefaultConverterName { get; set; }
		string DefaultAssetPluginName { get; set; }
	}
}
