using System;
using MvvmCross.Test.Core;
using NUnit.Framework;

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
    }
}
