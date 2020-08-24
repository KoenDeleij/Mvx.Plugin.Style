using System.Drawing;
using FluentAssertions;
using MvvmCross.Tests;
using Mvx.Plugin.Style.Models;
using Xunit;

namespace Mvx.Plugin.Style.Tests.Models
{
   
    public class ModelsTests : MvxIoCSupportingTest
    {

        [Fact]
        public void FontCanBeCopied()
        {
            Font f = new Font() { Name = "test", Color = Color.FromArgb(255, 0, 0), Size = 20, LineHeight = 10 };

            var newF = Font.CopyFont<Font, Font>(f, "test2");

            f.Color.Should().Be(newF.Color);
            newF.Name.Should().Be("test2");
            f.Alignment.Should().Be(newF.Alignment);
            f.LineHeight.Should().Be(newF.LineHeight);
        }

        [Fact]
        public void CopiedFontCanBeEasilyModified()
        {
            Font f = new Font() { Name = "test", Color = Color.FromArgb(255, 0, 0), Size = 20, LineHeight = 10 };

            var newF = Font.CopyFont<Font, Font>(f, "test2").SetLineHeight(30);

            f.Color.Should().Be(newF.Color);
            newF.Name.Should().Be("test2");
            f.Alignment.Should().Be(newF.Alignment);
            newF.LineHeight.Should().Be(30);
        }

        [Fact]
        public void CopiedFontCanBeEasilyModifiedBetweenTypes()
        {
            Font f = new Font() { Name = "test", Color = Color.FromArgb(255, 0, 0), Size = 20, LineHeight = 10 };

            var newF = (iOSFont) Font.CopyFont<Font, iOSFont>(f, "test2").SetLineHeight(30);

            newF.Should().NotBeNull();
        }
    }
}
