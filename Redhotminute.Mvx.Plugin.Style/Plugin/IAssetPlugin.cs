using System;
using MvvmCross.Platform.UI;

namespace Redhotminute.Mvx.Plugin.Style
{
	public interface IAssetPlugin
	{
		/// <summary>
		/// Add a base font
		/// </summary>
		/// <returns>The font.</returns>
		/// <param name="fontId">Font identifier.</param>
		IBaseFont GetFontByName(string fontId);

		/// <summary>
		/// Get a font based on a specific tag. This can be used for external sources. for example 'h1' or 'b'
		/// </summary>
		/// <returns>The font by tag.</returns>
		/// <param name="tag">Tag.</param>
		IBaseFont GetFontByTag(string tag);

		/// <summary>
		/// Gets a color.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="colorId">Color identifier.</param>
		MvxColor GetColor(string colorId);

		/// <summary>
		/// Add a color
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="color">Color.</param>
		/// <param name="id">Identifier.</param>
		IAssetPlugin AddColor(MvxColor color, string id);

		/// <summary>
		/// Adds the font.
		/// </summary>
		/// <returns>The font.</returns>
		/// <param name="font">Font.</param>
		/// <param name="tag">Tag.</param>
		IAssetPlugin AddFont(IBaseFont font,string tag = "");

		IAssetPlugin ClearFonts();

		/// <summary>
		/// Loads the json font file exported from Sketch
		/// </summary>
		/// <param name="jsonFile">Json file.</param>
		void LoadJsonFontFile(string jsonFile,bool clearCurrentFonts = true);

		void ConvertFontFileNameForPlatform(ref IBaseFont font);
	}
}

