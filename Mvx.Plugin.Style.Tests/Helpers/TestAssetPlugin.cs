using System;
using Mvx.Plugin.Style.Models;
using Mvx.Plugin.Style.Plugin;

namespace Mvx.Plugin.Style.Tests.Helpers
{
    public class TestAssetPlugin : AssetPlugin
    {
        public override void ConvertFontFileNameForPlatform(ref IBaseFont font)
        {
            font.FontPlatformName = font.Name;
        }

		public override bool CanAddFont(IBaseFont font)
		{
            if(font is iOSFont || font is AndroidFont){
                return false;
            }
            return base.CanAddFont(font);
		}
	}
}
