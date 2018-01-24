using System;
using System.Linq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using Redhotminute.Mvx.Plugin.Style.Helpers;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

namespace Redhotminute.Mvx.Plugin.Style.Tests
{
    public class AttributedFontHelperTest : MvxIoCSupportingTest
    {
		[SetUp]
		public void Init()
		{
			base.Setup();
		}

		[Test]
		public void NoTagTextShouldResultInOneBlock()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });

            string text = "this is one block";
            string resultWithoutTags;
			var result = AttributedFontHelper.GetFontTextBlocks(text, "Bold",plugin,out resultWithoutTags);

            Assert.That(result.Count,Is.EqualTo(1));

            Assert.That(result.First().StartIndex, Is.EqualTo(0));
            Assert.That(result.First().EndIndex, Is.EqualTo(text.Length));
            Assert.That(result.First().FontTag, Is.EqualTo(string.Empty));//if the font is not overriden it's not set

            Assert.That(resultWithoutTags, Is.EqualTo(text));
		}

		[Test]
		public void OneTagInTheMiddleShouldResultInThreeBlocks()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            string expectedResultWithoutTags = "this is one block";
			string text = "this is <b>one</b> block";
			string resultWithoutTags;
			var result = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);

			Assert.That(result.Count, Is.EqualTo(3));

			Assert.That(result[0].StartIndex, Is.EqualTo(0));
			Assert.That(result[0].EndIndex, Is.EqualTo(8));
            Assert.That(result[0].FontTag, Is.EqualTo(string.Empty));

			Assert.That(result[1].StartIndex, Is.EqualTo(8));
			Assert.That(result[1].EndIndex, Is.EqualTo(11));
			Assert.That(result[1].FontTag, Is.EqualTo("b"));

			Assert.That(result[2].StartIndex, Is.EqualTo(11));
            Assert.That(result[2].EndIndex, Is.EqualTo(expectedResultWithoutTags.Length));
			Assert.That(result[2].FontTag, Is.EqualTo(string.Empty));

			Assert.That(resultWithoutTags, Is.EqualTo(expectedResultWithoutTags));
		}

        [Test]
        public void BeginningWithATagShouldHaveBeginTag()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            string expectedResultWithoutTags = "one block";
            string text = "<b>one</b> block";
            string resultWithoutTags;
            var result = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].StartIndex, Is.EqualTo(0));
            Assert.That(result[0].EndIndex, Is.EqualTo(3));
            Assert.That(result[0].FontTag, Is.EqualTo("b"));

            Assert.That(result[1].StartIndex, Is.EqualTo(3));
            Assert.That(result[1].EndIndex, Is.EqualTo(9));
            Assert.That(result[1].FontTag, Is.EqualTo(string.Empty));

            Assert.That(resultWithoutTags, Is.EqualTo(expectedResultWithoutTags));
        }

		[Test]
		public void UnregisteredFontTagsShouldBeIgnored()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "strong"));

            string text = "this is <strong>one <a href='http://www.google.com'>font</a> to rule them all</strong> block";
			string resultWithoutTags;

            AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);

            Assert.That(resultWithoutTags,Is.EqualTo("this is one <a href='http://www.google.com'>font</a> to rule them all block"));
		}

        [Test]
        public void UnknownTagsShouldBeFiltered()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "strong"));

            string text = "<p> this is <strong>one <a href='http://www.google.com'>font</a> to rule them all</strong> block </p>";
            string resultWithoutTags;

            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);

            Assert.That(resultWithoutTags, Is.EqualTo(" this is one <a href='http://www.google.com'>font</a> to rule them all block "));
        }

        [Test]
        public void TagWithoutClosingTagShould()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            string text = "this is <b>one<b> block";
            string resultWithoutTags;

            Assert.Throws(typeof(Exception), () => AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags));
        }

		[Test]
		public void DoubleTagsShouldBeFormatted()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "strong"));

			string expectedResultWithoutTags = "TestExclusieve scherpe prijsTest tot";
			string resultWithoutTags;
            string text = "Test<strong>Exclusieve scherpe prijs</strong>Test<strong> tot</strong>";

			AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
		    Assert.That(resultWithoutTags, Is.EqualTo(expectedResultWithoutTags));
        }

        

        //TODO Test no font
        //TODO Test no text
        //TODO Test tags
        //TODO Test tags with no closing tags

    }
}
