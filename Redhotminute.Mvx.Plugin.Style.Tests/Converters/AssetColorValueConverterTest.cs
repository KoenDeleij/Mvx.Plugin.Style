using System.Drawing;
using FluentAssertions;
using MvvmCross.Tests;
using MvvmCross.UI;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;
using Xunit;

namespace Redhotminute.Mvx.Plugin.Style.Tests.Converters
{
    public class AssetColorValueConverterTest : IClassFixture<CustomTestFixture>
    {
        private readonly CustomTestFixture _fixture;

        public AssetColorValueConverterTest(CustomTestFixture fixture)
        {
            _fixture = fixture;
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
            var font = conv.Convert(_fixture.Ioc.Resolve<IAssetPlugin>(), typeof(Color), "Red", null);
            font.Should().BeNull();
        }

        [Fact]
        public void IfColorIsNotFoundReturnNull()
        {
            AssetColorValueConverter conv = new AssetColorValueConverter();
            var font = conv.Convert(_fixture.Ioc.Resolve<IAssetPlugin>(), typeof(Color), "11", null);
            font.Should().BeNull();
        }
    }
}
