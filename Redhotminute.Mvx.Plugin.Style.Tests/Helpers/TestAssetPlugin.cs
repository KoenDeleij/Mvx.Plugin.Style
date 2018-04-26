using System;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Tests.Helpers
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
