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


		private static float FontSizeFactor {
			get;
			set;
		}

		private static float? LineHeightFactor {
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

		private Dictionary<string, IBaseFont> _fontsTagged;
		private Dictionary<string, IBaseFont> FontsTagged {
			get {
				if (_fonts == null) {
					_fonts = new Dictionary<string, IBaseFont>();
				}
				return _fonts;
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

		public IBaseFont GetFontByName(string id)
		{
			IBaseFont font;
			Fonts.TryGetValue (id, out font);
			return font;
		}

		public IBaseFont GetFontByTag(string tag) {
			IBaseFont font;
			FontsTagged.TryGetValue(tag, out font);
			return font;
		}

		public IAssetPlugin AddFont(IBaseFont font,string tag="") {
			//convert the filename so the platform would understand this
			ConvertFontFileNameForPlatform(ref font);
			Fonts.Add(font.Name, font);

			if (!string.IsNullOrWhiteSpace(tag)) {
				FontsTagged.Add(tag, font);
			}

			return this;
		}

		public IAssetPlugin ClearFonts() {
			_fonts = new Dictionary<string, IBaseFont>();
			_fontsTagged = new Dictionary<string, IBaseFont>();
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
		}

		#endregion
	}
}

