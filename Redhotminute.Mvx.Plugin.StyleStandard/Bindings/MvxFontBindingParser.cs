namespace Redhotminute.Mvx.Plugin.Style.Binding
{
    using MvvmCross.Binding;
    using MvvmCross.Binding.Parse.Binding;
    using MvvmCross.Exceptions;

    public class MvxFontBindingParser
		: MvxBindingParser
		  , IMvxFontBindingParser {
		
		public MvxBindingMode DefaultBindingMode { get; set; }
		public string DefaultConverterName { get; set; }
		public string DefaultAssetPluginName { get; set; }

		public MvxFontBindingParser() {
			this.DefaultConverterName = "FontResource";
			this.DefaultAssetPluginName = "AssetProvider";
			this.DefaultBindingMode = MvxBindingMode.OneTime;
		}

		protected void ParseNextBindingDescriptionOptionInto(MvxSerializableBindingDescription description) {
			if (this.IsComplete)
				return;

			var block = this.ReadTextUntilNonQuotedOccurrenceOfAnyOf('=', ',', ';');
			block = block.Trim();
			if (string.IsNullOrEmpty(block)) {
				return;
			}

			switch (block) {
				case "Source":
					this.ParseEquals(block);
					var sourceName = this.ReadTextUntilNonQuotedOccurrenceOfAnyOf(',', ';');
					description.Path = sourceName;
					break;

				case "Converter":
					this.ParseEquals(block);
					description.Converter = this.ReadValidCSharpName();
					break;

				case "Key":
					this.ParseEquals(block);
					description.ConverterParameter = this.ReadValue();
					break;

				case "FallbackValue":
					this.ParseEquals(block);
					description.FallbackValue = this.ReadValue();
					break;

				default:
					if (description.ConverterParameter != null) {
						throw new MvxException(
							"Problem parsing Language Binding near '{0}', Key set to '{1}', position {2} in {3}",
							block, description.ConverterParameter, this.CurrentIndex, this.FullText);
					}

					block = UnquoteBlockIfNecessary(block);

					description.ConverterParameter = block;
					break;
			}
		}

		private static string UnquoteBlockIfNecessary(string block) {
			if (string.IsNullOrEmpty(block))
				return block;

			if (block.Length < 2)
				return block;

			if ((block.StartsWith("\'") && block.EndsWith("\'"))
				|| (block.StartsWith("\"") && block.EndsWith("\"")))
				return block.Substring(1, block.Length - 2);

			return block;
		}

		protected override MvxSerializableBindingDescription ParseBindingDescription() {
			var description = new MvxSerializableBindingDescription {
				Converter = this.DefaultConverterName,
				Path = this.DefaultAssetPluginName,
				Mode = this.DefaultBindingMode
			};

			this.SkipWhitespace();

			while (true) {
				this.ParseNextBindingDescriptionOptionInto(description);

				this.SkipWhitespace();
				if (this.IsComplete)
					return description;

				switch (this.CurrentChar) {
					case ',':
						this.MoveNext();
						break;

					case ';':
						return description;

					default:
						throw new MvxException(
							"Unexpected character {0} at position {1} in {2} - expected string-end, ',' or ';'",
							this.CurrentChar,
							this.CurrentIndex,
							this.FullText);
				}
			}
		}
	}
}
