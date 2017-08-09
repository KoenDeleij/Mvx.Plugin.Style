using System;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using System.Collections.Generic;
using MvvmCross.Plugins.Color;
using MvvmCross.Platform.UI;
using MvvmCross.Binding;

namespace Redhotminute.Mvx.Plugin.Style
{
	public abstract class AssetPlugin : IAssetPlugin
	{
		RedhotminuteStyleConfiguration _configuration;

		public AssetPlugin() {
			_configuration = new RedhotminuteStyleConfiguration() { FontSizeFactor = 1.0f, LineHeightFactor = 1.0f};
			FontSizeFactor = _configuration.FontSizeFactor.Value;
			LineHeightFactor = _configuration.LineHeightFactor;
		}

		public void Setup(RedhotminuteStyleConfiguration configuration) {
			if (configuration != null) {
				_configuration = configuration;
			}
			if (_configuration.FontSizeFactor.HasValue) {
				FontSizeFactor = _configuration.FontSizeFactor.Value;
			}
			LineHeightFactor = _configuration.LineHeightFactor;
		}


		public static float FontSizeFactor {
			get;
			set;
		}

		public static float? LineHeightFactor {
			get;
			set;
		}

		public static float GetPlatformFontSize(float fontSize) {
			return fontSize * FontSizeFactor;
		}

		public static float GetPlatformLineHeight(float fontSize,float lineHeight) {
			float factor = LineHeightFactor.HasValue?LineHeightFactor.Value: FontSizeFactor;
			return (lineHeight-fontSize) * factor;
		}


		private Dictionary<string,IBaseFont> _fonts;
		private Dictionary<string,IBaseFont> Fonts {
			get{
				if (_fonts == null){
					_fonts = new Dictionary<string, IBaseFont>();
				}
				return _fonts;
			}
		}

		private Dictionary<string,Dictionary<string,string>> _fontsTagged;
		private Dictionary<string,Dictionary<string,string>> FontsTagged {
			get {
				if (_fontsTagged == null) {
					_fontsTagged = new Dictionary<string,Dictionary<string,string>>();
				}
				return _fontsTagged;
			}
		}

		private Dictionary<string, MvxColor> _colors;
		private Dictionary<string, MvxColor> Colors {
			get {
				if (_colors == null) {
					_colors = new Dictionary<string, MvxColor>();
				}
				return _colors;
			}
		}

		public abstract void ConvertFontFileNameForPlatform (ref IBaseFont font);

		#region IAssetPlugin implementation

		private bool GetColorFromFontName(ref string fontColor,ref string fontName,string fontAndColor)
		{
			if (fontAndColor.Contains(":"))
			{
				var elements = fontAndColor.Split(':');
				if (elements.Length > 1)
				{
					fontColor = elements[1];
                    fontName = elements[0];
                    return true;
				}
            }

            return false;
		}

		public IBaseFont GetFontByName(string id)
		{
            string fontColor = string.Empty;
            string fontName = string.Empty;
            bool foundColor = GetColorFromFontName(ref fontColor,ref fontName,id);

            //if a color is set, it's a unique font
			IBaseFont font;
			Fonts.TryGetValue (id, out font);

            //if the font is not found, but has a modified color, store it
            if (font == null && foundColor){
                IBaseFont fontWithoutColor;
                Fonts.TryGetValue(fontName, out fontWithoutColor);
                if (fontWithoutColor != null)
                {
                    font = Font.NewFontWithModifiedColor((Font)fontWithoutColor, id, GetColor(fontColor));
                    AddFont(font);
                }
            }

			return font;
		}

		public IBaseFont GetFontByTag(string originalFontName,string tag) {
			Dictionary<string, string> fontTag;
			string fontName = string.Empty;

			if (FontsTagged.TryGetValue(originalFontName, out fontTag)) {
				fontTag.TryGetValue(tag,out fontName);
			}

			if (string.IsNullOrEmpty(fontName)) {
				return null;
			}
			var font= GetFontByName(fontName);
			return font;
		}

		public IAssetPlugin AddFont(IBaseFont font,List<FontTag> fontTags) {
			//convert the filename so the platform would understand this
			ConvertFontFileNameForPlatform(ref font);
			Fonts.Add(font.Name, font);

			//for each tag, add a font
			if (fontTags != null && fontTags.Count > 0) {
				if (FontsTagged != null) {
					if (!FontsTagged.ContainsKey(font.Name)) {
						FontsTagged[font.Name] = new Dictionary<string, string>();
					}

					foreach (FontTag tag in fontTags) {
						FontsTagged[font.Name].Add(tag.Tag, tag.OriginalFontName);
					}
				}

			}
			return this;
		}

		public IAssetPlugin AddFont(IBaseFont font,FontTag fontTag) {
			List<FontTag> tags = new List<FontTag>();
			tags.Add(fontTag);
			return AddFont(font, tags);
		}

		public IAssetPlugin AddFont(IBaseFont font) {
			return AddFont(font, new List<FontTag>());
		}

		public virtual IAssetPlugin ClearFonts() {
			_fonts = null;
			_fontsTagged = null;
			return this;
		}

		public virtual IAssetPlugin ClearColors() {
			_colors = null;
			return this;
		}

		public IAssetPlugin AddColor(MvxColor color, string id) {
			Colors.Add(id, color);
			return this;
		}

		public MvxColor GetColor(string colorId) {
			MvxColor color;
			Colors.TryGetValue(colorId, out color);
			return color;
		}

        /*
		public void LoadJsonFontFile(string jsonFile,bool clearCurrentFonts = true) {
			if (clearCurrentFonts) {
				this.Fonts.Clear();
				this.FontsTagged.Clear();
			}

			var l = MvvmCross.Platform.Mvx.Resolve<IMvxResourceLoader>();
			var serializer = MvvmCross.Platform.Mvx.Resolve<IMvxTextSerializer>();

			//load the styles
			var styles=serializer.DeserializeObject<FontStyles>(l.GetTextResource(jsonFile));

			foreach (Style style in styles.styles) {
				Font font = new Font();

				//alignment
				switch (style.alignment) {//ignore alignment 0. default left or override 
					case 1: font.Alignment = TextAlignment.Right;break;
					case 2: font.Alignment = TextAlignment.Center;break;
				}

				//color
				font.Color = new MvxColor((int)(255*style.color.red),(int)(255*style.color.green),(int)(255*style.color.blue));

				//lineheight
				if (style.lineHeight != 0) {
					font.LineHeight = style.lineHeight - style.size;
				}
				font.Size = style.size;

				//split the name 
				string[] name = style.name.Split(',');
				font.Name = name[0];
				font.FontFilename = name[1];
				font.FontPlatformName = style.font;

				this.AddFont(font);

				//TODO how to add tag
			}
		}*/

		#endregion
	}
}

