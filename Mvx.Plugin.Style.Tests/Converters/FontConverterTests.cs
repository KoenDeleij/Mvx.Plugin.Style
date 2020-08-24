using FluentAssertions;
using MvvmCross.Tests;
using MvvmCross.UI;
using Mvx.Plugin.Style.Converters;
using Mvx.Plugin.Style.Models;
using Mvx.Plugin.Style.Plugin;
using Mvx.Plugin.Style.Tests.Helpers;
using Xunit;

namespace Mvx.Plugin.Style.Tests.Converters
{

    public class FontConverterTests : IClassFixture<CustomTestFixture>
    {
        private readonly CustomTestFixture _fixture;

        public FontConverterTests(CustomTestFixture fixture)
        {
            _fixture = fixture;
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

            var font = conv.Convert(_fixture.Ioc.Resolve<IAssetPlugin>(), typeof(Font), "Bold", null);

            font.Should().Be(_fixture.FontToAdd);
        }

        [Fact]
        public void IfNoFontNameIsGivenReturnNull()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(null, typeof(Font), "Bold", null);
            font.Should().Be(_fixture.FontToAdd);
        }

        [Fact]
        public void IfNoPluginIsPassedItsResolved()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_fixture.Ioc.Resolve<IAssetPlugin>(), typeof(Font), "", null);
            font.Should().BeNull();
        }

        [Fact]
        public void IfFontIsNotFoundReturnNull()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_fixture.Ioc.Resolve<IAssetPlugin>(), typeof(Font), "11", null);
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
