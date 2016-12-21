using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding;
using MvvmCross.Platform;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.SourceSteps;


namespace Redhotminute.Mvx.Plugin.Style
{
	public static class MvxFontBindingContextOwnerExtensions
	{
		public static void BindFont(this IMvxBindingContextOwner owner
			, object target
			, string sourceKey
			)
		{
			//lookup the converter
			var converter = MvxBindingSingletonCache.Instance.ValueConverterLookup.Find("FontResource");

			var bindingDescription = new MvxBindingDescription
			{
				TargetName = "Font",
				Source = new MvxPathSourceStepDescription
				{
					SourcePropertyPath = "AssetProvider",
					Converter = converter,
					ConverterParameter = sourceKey
				},
				Mode = MvxBindingMode.OneTime
			};
			owner.AddBinding(target, bindingDescription);
		}
	}
}

