using System;
using MvvmCross.Platform.UI;

namespace Redhotminute.Plugin.Style
{
	public interface IAssetPlugin
	{
		IBaseFont GetFont(string fontId);
		MvxColor GetColor(string colorId);
		IAssetPlugin AddFont(IBaseFont font);
		IAssetPlugin AddColor(MvxColor color,string id);
		void ConvertFontFileNameForPlatform(ref IBaseFont font);
	}
}

