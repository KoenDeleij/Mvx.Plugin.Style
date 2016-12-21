using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding;
using MvvmCross.Platform;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.SourceSteps;


namespace Redhotminute.Mvx.Plugin.Style.Touch {
	/// <summary>
	/// This extension is added because iOS cannot have certain properties be passed by the font only
	/// For example a lineheight has to be set from an attributed text, while for android, the font itself contains the definition of lineheight
	/// </summary>
	public static class MvxFontBindingContextOwnerExtensionsTouch {
		/// <summary>
		/// Binds the language font.
		/// </summary>
		/// <param name="owner">Owner.</param>
		/// <param name="target">Target.</param>
		/// <param name="languageId">Language identifier.</param>
		/// <param name="fontId">Font identifier.</param>
		public static void BindLanguageFont(this IMvxBindingContextOwner owner
										, object target
										, string languageId
										, string fontId) {
			var converter = MvvmCross.Platform.Core.MvxSingleton<IMvxBindingSingletonCache>.Instance.ValueConverterLookup.Find("FontLang");

			var bindingDescription = new MvxBindingDescription {
				TargetName = "AttributedText",
				Source = new MvxPathSourceStepDescription {
					SourcePropertyPath = "TextSource",
					Converter = converter,
					ConverterParameter = $"{languageId};{fontId}",
				},
				Mode = MvxBindingMode.OneTime
			};
			owner.AddBinding(target, bindingDescription);
		}
	}
}

