using FluentAssertions;
using MvvmCross.Tests;
using MvvmCross.UI;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;
using Xunit;

namespace Redhotminute.Mvx.Plugin.Style.Tests.Converters
{
    public class AssetColorValueConverterTest : MvxIoCSupportingTest
    {
        private AssetPlugin _plugin;
        //private Font _fontToAdd;


        public AssetColorValueConverterTest(){
            base.Setup();

            _plugin = new TestAssetPlugin();
            _plugin.AddColor(new MvxColor(255, 0, 0), "Red");
            _plugin.AddColor(new MvxColor(0, 0, 255), "Blue");

            Ioc.RegisterSingleton<IAssetPlugin>(_plugin);
        }

        [Fact]
        public void ShouldConstruct()
        {
            AssetColorValueConverter conv = new AssetColorValueConverter();
            conv.Should().NotBeNull();
        }

        [Fact]
        public void IfNoPluginIsPassedItsResolved()
        {
            AssetColorValueConverter conv = new AssetColorValueConverter();
            var font = conv.Convert(_plugin, typeof(MvxColor), "Red", null);
            font.Should().BeNull();
        }

        [Fact]
        public void IfColorIsNotFoundReturnNull()
        {
            AssetColorValueConverter conv = new AssetColorValueConverter();
            var font = conv.Convert(_plugin, typeof(MvxColor), "11", null);
            font.Should().BeNull();
        }
    }
}
