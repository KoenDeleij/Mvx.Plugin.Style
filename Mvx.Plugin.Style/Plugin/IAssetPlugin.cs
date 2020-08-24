using System.Collections.Generic;
using System.Drawing;
using Mvx.Plugin.Style.Models;

namespace Mvx.Plugin.Style.Plugin
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
		/// <param name="originalFontName">The font the tag is linked to</param>
		/// <param name="tag">Tag.</param>
		IBaseFont GetFontByTag(string originalFontName,string tag);
        IBaseFont GetFontByTagWithTag(string originalFontName, string tag, out FontTag originalTag);

		/// <summary>
		/// Gets a color.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="colorId">Color identifier.</param>
		Color GetColor(string colorId);

		/// <summary>
		/// Add a color
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="color">Color.</param>
		/// <param name="id">Identifier.</param>
		IAssetPlugin AddColor(Color color, string id);

		/// <summary>
		/// Adds the font.
		/// </summary>
		/// <returns>The font.</returns>
		/// <param name="font">Font.</param>
		/// <param name="fontTags">Font tags.</param>
		IAssetPlugin AddFont(IBaseFont font,List<FontTag> fontTags);

		IAssetPlugin AddFont(IBaseFont font,FontTag fontTag);

		IAssetPlugin AddFont(IBaseFont font);

		IAssetPlugin ClearFonts();

		IAssetPlugin ClearColors();

		/// <summary>
		/// Loads the json font file exported from Sketch
		/// </summary>
		/// <param name="jsonFile">Json file.</param>
		//void LoadJsonFontFile(string jsonFile,bool clearCurrentFonts = true);

		void ConvertFontFileNameForPlatform(ref IBaseFont font);
	}
}

