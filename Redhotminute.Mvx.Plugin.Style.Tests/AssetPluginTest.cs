using System;
using MvvmCross.Test.Core;
using NUnit.Framework;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Tests
{
    [TestFixture]
    public class AssetPluginTest : MvxIoCSupportingTest
    {
        [SetUp]
        public void Init()
        {
            base.Setup();
        }

        [Test]
        public void ShouldConstruct()
        {
            AssetPlugin plugin = new AssetPlugin();
            Assert.That(plugin,Is.Not.Null);
        }

        [Test]
        public void PlatformSizeIsModifiedByFontSizeFactor()
		{
			AssetPlugin plugin = new AssetPlugin();

            Assert.That(AssetPlugin.GetPlatformFontSize(10.0f), Is.EqualTo(10.0f));

            AssetPlugin.FontSizeFactor = 2.0f;
            Assert.That(AssetPlugin.GetPlatformFontSize(10.0f), Is.EqualTo(20.0f));
		}

		[Test]
		public void AddingAFontWithoutNameShouldThrow()
		{
			AssetPlugin plugin = new AssetPlugin();
            Assert.Throws<Exception>(()=>plugin.AddFont(new BaseFont()));
		}

		[Test]
		public void AddingAFontWithoutFileNameShouldThrow()
		{
			AssetPlugin plugin = new AssetPlugin();
			Assert.Throws<Exception>(() => plugin.AddFont(new BaseFont() { Name = "H1" }));
		}

        [Test]
        public void AfterAddingAFontWithNameAndFileNameShouldBeStored()
		{
			AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "H1",FontFilename="H1.otf" });

            Assert.That(plugin.GetFontByName("H1"), Is.Not.Null);
		}

		[Test]
		public void GettingANonExistingFontReturnsNull()
		{
			AssetPlugin plugin = new AssetPlugin();
			Assert.That(plugin.GetFontByName("H1"), Is.Null);
		}

		[Test]
		public void GettingANonExistingColorReturnsNull()
		{
			AssetPlugin plugin = new AssetPlugin();
            Assert.That(plugin.GetColor("H1"), Is.Null);
		}

		[Test]
		public void GettingANonExistingFontColorReturnsNull()
		{
			AssetPlugin plugin = new AssetPlugin();
            Assert.That(plugin.GetFontByName("Bananas:H1"), Is.Null);
		}

		[Test]
		public void GettingANonExistingFontTagReturnsNull()
		{
			AssetPlugin plugin = new AssetPlugin();
            Assert.That(plugin.GetFontByTag("Banana","a"), Is.Null);
		}

		[Test]
		public void GettingANonExistingTagReturnsNull()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

			Assert.That(plugin.GetFontByTag("H1", "c"), Is.Null);
		}

		[Test]
		public void AfterAddingAFontWithTagFontShouldReturnAsTaggedWithTheRightTag()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));
			
			Assert.That(plugin.GetFontByName("H1"), Is.Not.Null);
            Assert.That(plugin.GetFontByName("Bold"), Is.Not.Null);
            Assert.That(plugin.GetFontByTag("H1","b"), Is.Not.Null);
            Assert.That(plugin.GetFontByTag("H1","b").Name, Is.EqualTo("Bold"));
		}

		[Test]
		public void ClearingFontsClearsAllFontsAndTags()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

			Assert.That(plugin.GetFontByName("H1"), Is.Not.Null);
			Assert.That(plugin.GetFontByName("Bold"), Is.Not.Null);

            plugin.ClearFonts();

			Assert.That(plugin.GetFontByName("H1"), Is.Null);
			Assert.That(plugin.GetFontByName("Bold"), Is.Null);
		}

		[Test]
		public void AddingColorAddsColor()
		{
			AssetPlugin plugin = new AssetPlugin();

            Assert.That(plugin.GetColor("Main"), Is.Null);

            plugin.AddColor(new MvvmCross.Platform.UI.MvxColor(10,0,10),"Main");

            Assert.That(plugin.GetColor("Main"), Is.Not.Null);
		}

		[Test]
		public void ClearingColorClearsAllColors()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddColor(new MvvmCross.Platform.UI.MvxColor(10, 0, 10), "Main");

			Assert.That(plugin.GetColor("Main"), Is.Not.Null);

            plugin.ClearColors();

            Assert.That(plugin.GetColor("Main"), Is.Null);
		}

		[Test]
		public void AddingColorToTheBaseFontLookupOverridesTheColor()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddColor(new MvvmCross.Platform.UI.MvxColor(255, 0, 0), "Red");
            plugin.AddColor(new MvvmCross.Platform.UI.MvxColor(0, 0, 255), "Blue");
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf",Color=plugin.GetColor("Blue") });
           
            Assert.That(plugin.GetFontByName("Bold").Color.R, Is.EqualTo(0));
            Assert.That(plugin.GetFontByName("Bold").Color.G, Is.EqualTo(0));
            Assert.That(plugin.GetFontByName("Bold").Color.B, Is.EqualTo(255));

			Assert.That(plugin.GetFontByName("Bold:Red").Color.R, Is.EqualTo(255));
            Assert.That(plugin.GetFontByName("Bold:Red").Color.G, Is.EqualTo(0));
            Assert.That(plugin.GetFontByName("Bold:Red").Color.G, Is.EqualTo(0));
		}

		[Test]
		public void AddingColorToTheFontLookupOverridesTheColor()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddColor(new MvvmCross.Platform.UI.MvxColor(255, 0, 0), "Red");
			plugin.AddColor(new MvvmCross.Platform.UI.MvxColor(0, 0, 255), "Blue");
			plugin.AddFont(new Font() { Name = "Bold", FontFilename = "Bold.otf", Color = plugin.GetColor("Blue") });

			Assert.That(plugin.GetFontByName("Bold").Color.R, Is.EqualTo(0));
			Assert.That(plugin.GetFontByName("Bold").Color.G, Is.EqualTo(0));
			Assert.That(plugin.GetFontByName("Bold").Color.B, Is.EqualTo(255));

			Assert.That(plugin.GetFontByName("Bold:Red").Color.R, Is.EqualTo(255));
			Assert.That(plugin.GetFontByName("Bold:Red").Color.G, Is.EqualTo(0));
			Assert.That(plugin.GetFontByName("Bold:Red").Color.G, Is.EqualTo(0));
		}

        //TODO test caching of fonts
        //TODO test non-existing font/color combinations
        //TODO test font: and no color behind it (faulty formats with : in the wrong place)
    }
}
