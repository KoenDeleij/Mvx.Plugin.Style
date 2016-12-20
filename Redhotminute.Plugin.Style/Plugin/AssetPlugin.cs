using System;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using System.Collections.Generic;
using MvvmCross.Plugins.Color;
using MvvmCross.Platform.UI;
using MvvmCross.Binding;

namespace Redhotminute.Plugin.Style
{
	public abstract class AssetPlugin : IAssetPlugin
	{
		RedhotminuteStyleConfiguration _configuration;

		public AssetPlugin() {
			_configuration = new RedhotminuteStyleConfiguration() { FontSizeFactor = 1.0f, LineHeightFactor = 1.0f};
			FontSizeFactor = _configuration.FontSizeFactor;
			LineHeightFactor = _configuration.LineHeightFactor;
		}

		public void Setup(RedhotminuteStyleConfiguration configuration) {
			if (configuration != null) {
				_configuration = configuration;
			}

			FontSizeFactor = _configuration.FontSizeFactor;
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

		public IBaseFont GetFont (string fontId)
		{
			IBaseFont font;
			Fonts.TryGetValue (fontId,out font);
			return font;
		}

		public IAssetPlugin AddFont(IBaseFont font) {
			//convert the filename so the platform would understand this
			ConvertFontFileNameForPlatform(ref font);
			Fonts.Add(font.Name, font);

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

		#endregion
	}
}

