using System;
using MvvmCross.Platform.UI;
using MvvmCross.Test.Core;
using NUnit.Framework;
using Redhotminute.Mvx.Plugin.Style.Converters;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;

namespace Redhotminute.Mvx.Plugin.Style.Tests.Models
{
    [TestFixture]
    public class ModelsTests : MvxIoCSupportingTest
    {
        [SetUp]
        public void Init()
        {
            base.Setup();
        }

        [Test]
        public void FontCanBeCopied()
        {
            Font f = new Font() { Name = "test", Color = new MvxColor(255, 0, 0), Size = 20, LineHeight = 10 };

            var newF = Font.CopyFont<Font, Font>(f, "test2");

            Assert.That(f.Color, Is.EqualTo(newF.Color));
            Assert.That(newF.Name, Is.EqualTo("test2"));
            Assert.That(f.Alignment, Is.EqualTo(newF.Alignment));
            Assert.That(f.LineHeight, Is.EqualTo(newF.LineHeight));
        }

        [Test]
        public void CopiedFontCanBeEasilyModified()
        {
            Font f = new Font() { Name = "test", Color = new MvxColor(255, 0, 0), Size = 20, LineHeight = 10 };

            var newF = Font.CopyFont<Font, Font>(f, "test2").SetLineHeight(30);

            Assert.That(f.Color, Is.EqualTo(newF.Color));
            Assert.That(newF.Name, Is.EqualTo("test2"));
            Assert.That(f.Alignment, Is.EqualTo(newF.Alignment));
            Assert.That(newF.LineHeight, Is.EqualTo(30));
        }

        [Test]
        public void CopiedFontCanBeEasilyModifiedBetweenTypes()
        {
            Font f = new Font() { Name = "test", Color = new MvxColor(255, 0, 0), Size = 20, LineHeight = 10 };

            var newF = (iOSFont) Font.CopyFont<Font, iOSFont>(f, "test2").SetLineHeight(30);

            Assert.That(newF,Is.Not.Null);
        }
    }
}
