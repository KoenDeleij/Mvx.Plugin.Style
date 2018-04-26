using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding;
using MvvmCross.Platform;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.SourceSteps;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Bindings {
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
										, string fontId
		                                ,string targetPropertyName = null) {
			var converter = MvvmCross.Platform.Core.MvxSingleton<IMvxBindingSingletonCache>.Instance.ValueConverterLookup.Find("FontLang");

			var bindingDescription = new MvxBindingDescription();
			if (!string.IsNullOrEmpty(targetPropertyName)) {
				bindingDescription.TargetName = targetPropertyName;
			}
			else {
				bindingDescription.TargetName = "AttributedText";
			}

			bindingDescription.Source = new MvxPathSourceStepDescription {
					SourcePropertyPath = "TextSource",
					Converter = converter,
					ConverterParameter = $"{languageId};{fontId}"
			};

			bindingDescription.Mode = MvxBindingMode.OneTime;
			owner.AddBinding(target, bindingDescription);
		}

		public static void BindLanguageFontColor(this IMvxBindingContextOwner owner
								, object target
								, string languageId
								, string fontId
                                , string color
								, string targetPropertyName = null)
		{
            BindLanguageFont(owner, target, languageId, $"{fontId}:{color}",targetPropertyName);
		}
	}
}

