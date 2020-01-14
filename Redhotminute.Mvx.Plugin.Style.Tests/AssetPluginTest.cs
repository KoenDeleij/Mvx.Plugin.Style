using System;
using System.Drawing;
using FluentAssertions;
using MvvmCross.Tests;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;
using Xunit;

namespace Redhotminute.Mvx.Plugin.Style.Tests
{
    public class AssetPluginTest : MvxIoCSupportingTest
    {
        [Fact]
        public void ShouldConstruct()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.Should().NotBeNull();
        }

        [Fact]
        public void PlatformSizeIsModifiedByFontSizeFactor()
		{
            AssetPlugin plugin = new TestAssetPlugin();

            AssetPlugin.GetPlatformFontSize(10.0f).Should().Be(10.0f);

            AssetPlugin.FontSizeFactor = 2.0f;
            AssetPlugin.GetPlatformFontSize(10.0f).Should().Be(20.0f);
		}

        [Fact]
		public void AddingAFontWithoutNameShouldThrow()
		{
            AssetPlugin plugin = new TestAssetPlugin();
            Assert.Throws<Exception>(()=>plugin.AddFont(new BaseFont()));
		}

        [Fact]
		public void AddingAFontWithoutFileNameShouldThrow()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			Assert.Throws<Exception>(() => plugin.AddFont(new BaseFont() { Name = "H1" }));
		}

        [Fact]
        public void AfterAddingAFontWithNameAndFileNameShouldBeStored()
		{
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "H1",FontFilename="H1.otf" });

            plugin.GetFontByName("H1").Should().NotBeNull();
		}

        [Fact]
		public void GettingANonExistingFontReturnsNull()
		{
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.GetFontByName("H1").Should().BeNull();
		}

        [Fact]
		public void GettingANonExistingColorReturnsNull()
		{
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.GetColor("H1").Should().Be(Color.Empty);
		}

        [Fact]
		public void GettingANonExistingFontColorReturnsNull()
		{
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.GetFontByName("Bananas:H1").Should().BeNull();
		}

        [Fact]
		public void GettingANonExistingFontTagReturnsNull()
		{
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.GetFontByTag("Banana","a").Should().BeNull();
		}

        [Fact]
		public void GettingANonExistingTagReturnsNull()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            plugin.GetFontByTag("H1", "c").Should().BeNull();
		}

        [Fact]
		public void AfterAddingAFontWithTagFontShouldReturnAsTaggedWithTheRightTag()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));
			
            plugin.GetFontByName("H1").Should().NotBeNull();
            plugin.GetFontByName("Bold").Should().NotBeNull();
            plugin.GetFontByTag("H1","b").Should().NotBeNull();
            plugin.GetFontByTag("H1","b").Name.Should().Be("Bold");
		}

        [Fact]
		public void ClearingFontsClearsAllFontsAndTags()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            plugin.GetFontByName("H1").Should().NotBeNull();
            plugin.GetFontByName("Bold").Should().NotBeNull();

            plugin.ClearFonts();

            plugin.GetFontByName("H1").Should().BeNull();
            plugin.GetFontByName("Bold").Should().BeNull();
		}

        [Fact]
		public void AddingColorAddsColor()
		{
            AssetPlugin plugin = new TestAssetPlugin();
            var color = Color.FromArgb(10, 0, 10);

            plugin.GetColor("Main").Should().Be(Color.Empty);
            plugin.AddColor(color, "Main");
            plugin.GetColor("Main").Should().Be(color);
		}

        [Fact]
		public void ClearingColorClearsAllColors()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddColor(Color.FromArgb(10, 0, 10), "Main");

            plugin.GetColor("Main").Should().NotBe(Color.Empty);

            plugin.ClearColors();

            plugin.GetColor("Main").Should().Be(Color.Empty);
		}

        [Fact]
		public void AddingColorToTheBaseFontLookupOverridesTheColor()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddColor(Color.FromArgb(255, 0, 0), "Red");
            plugin.AddColor(Color.FromArgb(0, 0, 255), "Blue");
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf",Color=plugin.GetColor("Blue") });

            plugin.GetFontByName("Bold").Color.R.Should().Be(0);
            plugin.GetFontByName("Bold").Color.G.Should().Be(0);
            plugin.GetFontByName("Bold").Color.B.Should().Be(255);

            plugin.GetFontByName("Bold:Red").Color.R.Should().Be(255);
            plugin.GetFontByName("Bold:Red").Color.G.Should().Be(0);
            plugin.GetFontByName("Bold:Red").Color.G.Should().Be(0);
		}

        [Fact]
		public void AddingColorToTheFontLookupOverridesTheColor()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddColor(Color.FromArgb(255, 0, 0), "Red");
			plugin.AddColor(Color.FromArgb(0, 0, 255), "Blue");
			plugin.AddFont(new Font() { Name = "Bold", FontFilename = "Bold.otf", Color = plugin.GetColor("Blue") });

            plugin.GetFontByName("Bold").Color.R.Should().Be(0);
            plugin.GetFontByName("Bold").Color.G.Should().Be(0);
            plugin.GetFontByName("Bold").Color.B.Should().Be(255);

            plugin.GetFontByName("Bold:Red").Color.R.Should().Be(255);
            plugin.GetFontByName("Bold:Red").Color.G.Should().Be(0);
            plugin.GetFontByName("Bold:Red").Color.G.Should().Be(0);
		}

        [Fact]
        public void GettingFontWithoutFaultyColorsFallBackToDefaultColor()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddColor(Color.FromArgb(255, 0, 0), "Red");
            plugin.AddColor(Color.FromArgb(0, 0, 255), "Blue");
            plugin.AddFont(new Font() { Name = "Bold", FontFilename = "Bold.otf", Color = plugin.GetColor("Red") });

            plugin.GetFontByName("Bold:").Color.R.Should().Be(255);
        }

        [Fact]
        public void AddingPlatformSpecificFontsAreNotAddedByOtherPlatforms()
        {
            AssetPlugin plugin = new TestAssetPlugin();

            plugin.AddColor(Color.FromArgb(255, 0, 0), "Red");

            var font = new iOSFont() { Name = "Bold", FontFilename = "Bold.otf", Color = plugin.GetColor("Red") };

            plugin.CanAddFont(font).Should().Be(false);

            plugin.AddFont(font);

            plugin.GetFontByName("Bold").Should().BeNull();
        }

        [Fact]
        public void AddingFontsWithoutNameThrows()
        {
            AssetPlugin plugin = new TestAssetPlugin();

            plugin.AddColor(Color.FromArgb(255, 0, 0), "Red");

            Assert.Throws<Exception>(() => plugin.AddFont(new Font() { FontFilename = "Bold.otf", Color = plugin.GetColor("Red") }));
        }

        //TODO test caching of fonts
        //TODO test non-existing font/color combinations
    }
}
