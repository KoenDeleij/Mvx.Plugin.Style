using FluentAssertions;
using MvvmCross.Tests;
using MvvmCross.UI;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;
using Xunit;

namespace Redhotminute.Mvx.Plugin.Style.Tests.Converters
{

    public class FontConverterTests : MvxIoCSupportingTest
    {
        private AssetPlugin _plugin;
        private Font _fontToAdd;

        public FontConverterTests(){
            base.Setup();

            _plugin = new TestAssetPlugin();
            _plugin.AddColor(new MvxColor(255, 0, 0), "Red");
            _plugin.AddColor(new MvxColor(0, 0, 255), "Blue");

            _fontToAdd = new Font() { Name = "Bold", FontFilename = "Bold.otf", Color = _plugin.GetColor("Blue") };
            _plugin.AddFont(_fontToAdd);

            Ioc.RegisterSingleton<IAssetPlugin>(_plugin);
        }

        [Fact]
        public void ShouldConstruct()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            conv.Should().NotBeNull();
        }

        [Fact]
        public void ConverterConvertsFontNameToFontObject()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_plugin, typeof(Font), "Bold", null);

            font.Should().Be(_fontToAdd);
        }

        [Fact]
        public void IfNoFontNameIsGivenReturnNull()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(null, typeof(Font), "Bold", null);
            font.Should().Be(_fontToAdd);
        }

        [Fact]
        public void IfNoPluginIsPassedItsResolved()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_plugin, typeof(Font), "", null);
            font.Should().BeNull();
        }

        [Fact]
        public void IfFontIsNotFoundReturnNull()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_plugin, typeof(Font), "11", null);
            font.Should().BeNull();
        }

        [Fact]
        public void IfParameterIsStringCombineItToCreateFontColorCombo()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert("Blue", typeof(Font), "Bold", null) as Font;

            font.Color.B.Should().Be(255);
        }
    }
}
