using MvvmCross.Tests;
using MvvmCross.UI;
using NUnit.Framework;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;

namespace Redhotminute.Mvx.Plugin.Style.Tests.Converters
{
    [TestFixture]
    public class FontConverterTests : MvxIoCSupportingTest
    {
        private AssetPlugin _plugin;
        private Font _fontToAdd;

        [SetUp]
        public void Init()
        {
            base.Setup();

            _plugin = new TestAssetPlugin();
            _plugin.AddColor(new MvxColor(255, 0, 0), "Red");
            _plugin.AddColor(new MvxColor(0, 0, 255), "Blue");

            _fontToAdd = new Font() { Name = "Bold", FontFilename = "Bold.otf", Color = _plugin.GetColor("Blue") };
            _plugin.AddFont(_fontToAdd);

            Ioc.RegisterSingleton<IAssetPlugin>(_plugin);
        }

        [Test]
        public void ShouldConstruct()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            Assert.That(conv, Is.Not.Null);
        }

        [Test]
        public void ConverterConvertsFontNameToFontObject()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_plugin, typeof(Font), "Bold", null);

            Assert.That(font, Is.EqualTo(_fontToAdd));
        }

        [Test]
        public void IfNoFontNameIsGivenReturnNull()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(null, typeof(Font), "Bold", null);
            Assert.That(font, Is.EqualTo(_fontToAdd));
        }

        [Test]
        public void IfNoPluginIsPassedItsResolved()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_plugin, typeof(Font), "", null);
            Assert.That(font, Is.Null);
        }

        [Test]
        public void IfFontIsNotFoundReturnNull()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert(_plugin, typeof(Font), "11", null);
            Assert.That(font, Is.Null);
        }

        [Test]
        public void IfParameterIsStringCombineItToCreateFontColorCombo()
        {
            FontResourceValueConverter conv = new FontResourceValueConverter();
            var font = conv.Convert("Blue", typeof(Font), "Bold", null) as Font;

            Assert.That(font.Color.B,Is.EqualTo(255));
        }
    }
}
