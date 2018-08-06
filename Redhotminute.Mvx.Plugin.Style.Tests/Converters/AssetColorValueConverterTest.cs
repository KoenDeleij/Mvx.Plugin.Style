using MvvmCross.Tests;
using MvvmCross.UI;
using NUnit.Framework;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;

namespace Redhotminute.Mvx.Plugin.Style.Tests.Converters
{
    [TestFixture]
    public class AssetColorValueConverterTest : MvxIoCSupportingTest
    {
        private AssetPlugin _plugin;
        //private Font _fontToAdd;

        [SetUp]
        public void Init()
        {
            base.Setup();

            _plugin = new TestAssetPlugin();
            _plugin.AddColor(new MvxColor(255, 0, 0), "Red");
            _plugin.AddColor(new MvxColor(0, 0, 255), "Blue");

            Ioc.RegisterSingleton<IAssetPlugin>(_plugin);
        }

        [Test]
        public void ShouldConstruct()
        {
            AssetColorValueConverter conv = new AssetColorValueConverter();
            Assert.That(conv, Is.Not.Null);
        }

        [Test]
        public void IfNoPluginIsPassedItsResolved()
        {
            AssetColorValueConverter conv = new AssetColorValueConverter();
            var font = conv.Convert(_plugin, typeof(MvxColor), "Red", null);
            Assert.That(font, Is.Null);
        }

        [Test]
        public void IfColorIsNotFoundReturnNull()
        {
            AssetColorValueConverter conv = new AssetColorValueConverter();
            var font = conv.Convert(_plugin, typeof(MvxColor), "11", null);
            Assert.That(font, Is.Null);
        }
    }
}
