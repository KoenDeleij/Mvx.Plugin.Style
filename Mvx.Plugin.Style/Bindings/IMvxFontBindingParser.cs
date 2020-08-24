using System;
using MvvmCross.Binding.Parse.Binding;

namespace Mvx.Plugin.Style.Bindings {

	public interface IMvxFontBindingParser
		: IMvxBindingParser {
		string DefaultConverterName { get; set; }
		string DefaultAssetPluginName { get; set; }
	}
}
