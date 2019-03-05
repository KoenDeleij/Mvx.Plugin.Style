using System;
using System.Linq;
using FluentAssertions;
using MvvmCross.Tests;
using Redhotminute.Mvx.Plugin.Style.Helpers;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;
using Redhotminute.Mvx.Plugin.Style.Tests.Helpers;
using Xunit;

namespace Redhotminute.Mvx.Plugin.Style.Tests
{
    public class AttributedFontHelperTest : MvxIoCSupportingTest
    {
		[Fact]
		public void NoTagTextShouldResultInOneBlock()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });

            string text = "this is one block";
            string resultWithoutTags;
			var result = AttributedFontHelper.GetFontTextBlocks(text, "Bold",plugin,out resultWithoutTags);

            result.Count.Should().Be(1);

            result.First().StartIndex.Should().Be(0);
            result.First().EndIndex.Should().Be(text.Length);
            result.First().FontTag.Should().BeNull();//if the font is not overriden it's not set

            resultWithoutTags.Should().Be(text);
		}

		[Fact]
		public void OneTagInTheMiddleShouldResultInThreeBlocks()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            string expectedResultWithoutTags = "this is one block";
			string text = "this is <b>one</b> block";
			string resultWithoutTags;
			var result = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);

			result.Count.Should().Be(3);

			result[0].StartIndex.Should().Be(0);
			result[0].EndIndex.Should().Be(8);
            result[0].FontTag.Should().BeNull();

			result[1].StartIndex.Should().Be(8);
			result[1].EndIndex.Should().Be(11);
			result[1].FontTag.Tag.Should().Be("b");

			result[2].StartIndex.Should().Be(11);
            result[2].EndIndex.Should().Be(expectedResultWithoutTags.Length);
            result[2].FontTag.Should().BeNull();

			resultWithoutTags.Should().Be(expectedResultWithoutTags);
		}

        [Fact]
        public void BeginningWithATagShouldHaveBeginTag()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            string expectedResultWithoutTags = "one block";
            string text = "<b>one</b> block";
            string resultWithoutTags;
            var result = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);

            result.Count.Should().Be(2);

            result[0].StartIndex.Should().Be(0);
            result[0].EndIndex.Should().Be(3);
            result[0].FontTag.Tag.Should().Be("b");

            result[1].StartIndex.Should().Be(3);
            result[1].EndIndex.Should().Be(9);
            result[1].FontTag.Should().BeNull();

            resultWithoutTags.Should().Be(expectedResultWithoutTags);
        }

		[Fact]
		public void UnregisteredFontTagsShouldBeIgnored()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "strong"));

            string text = "this is <strong>one <a href='http://www.google.com'>font</a> to rule them all</strong> block";
			string resultWithoutTags;

            AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);

            resultWithoutTags.Should().Be("this is one <a href='http://www.google.com'>font</a> to rule them all block");
		}

        [Fact]
        public void UnknownTagsShouldBeIgnored()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "strong"));

            string resultWithoutTags;

            string text = "las <p> this is <strong>one all</strong> block </p>";

            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);
            resultWithoutTags.Should().Be("las <p> this is one all block </p>");

            text = "las <strong>this is <p>one all</p> block</strong>";

            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(2);
            resultWithoutTags.Should().Be("las this is <p>one all</p> block");

            text = "las <p>lala</p><p>lala</p> test <strong>this is <p>one all</p> block</strong>";

            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(2);
            resultWithoutTags.Should().Be("las <p>lala</p><p>lala</p> test this is <p>one all</p> block");


            text = " this is <strong>one <a href='http://www.google.com'>font</a> to rule <a>them</a> all</strong> block ";
            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);
            resultWithoutTags.Should().Be(" this is one <a href='http://www.google.com'>font</a> to rule <a>them</a> all block ");

            text = "<p> this is <strong>one <a href='http://www.google.com'>font</a> to rule <a>them</a> all</strong> block </p>";
            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);
            resultWithoutTags.Should().Be("<p> this is one <a href='http://www.google.com'>font</a> to rule <a>them</a> all block </p>");
        }

        [Fact]
        public void LinksShouldBeFlattenedAndPropertiesStripped()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a",FontTagAction.Link));

            string resultWithoutTags;

            //standard link
            string text = "this is one <a href=http://www.google.com>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Keys.Contains("href").Should().BeTrue();
            blocks[1].TagProperties.Values.Contains("http://www.google.com").Should().BeTrue();
            blocks[2].TagProperties.Should().BeNull();

            resultWithoutTags.Should().Be("this is one font to block");
        }

        [Fact]
        public void LinksWithoutHrefShouldBeFlattenedAndPropertiesStripped()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link without attributes 
            var text = "this is one <a>http://www.google.com</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Should().BeNull();
            blocks[1].FontTag.FontAction.Should().Be(FontTagAction.Link);

            resultWithoutTags.Should().Be("this is one http://www.google.com to block");
        }

        [Fact]
        public void LinksWithQuotesShouldBeFlattenedAndPropertiesStripped()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='http://www.google.com'>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Keys.Contains("href").Should().BeTrue();
            blocks[1].TagProperties.Values.Contains("http://www.google.com").Should().BeTrue();
            blocks[1].FontTag.FontAction.Should().Be(FontTagAction.Link);

            resultWithoutTags.Should().Be("this is one font to block");

            //link with double quotes 
            text = "this is one <a href=\"http://www.google.com\">font</a> to block";
            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Keys.Contains("href").Should().BeTrue();
            blocks[1].TagProperties.Values.Contains("http://www.google.com").Should().BeTrue();
            blocks[1].FontTag.FontAction.Should().Be(FontTagAction.Link);

            resultWithoutTags.Should().Be("this is one font to block");
        }

        [Fact]
        public void LinksWithEqualSignShouldBeParsedAsFull()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='http://www.google.com/maps/la=3'>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Keys.Contains("href").Should().BeTrue();
            blocks[1].TagProperties.Values.Contains("http://www.google.com/maps/la=3").Should().BeTrue();
            blocks[1].FontTag.FontAction.Should().Be(FontTagAction.Link);

            resultWithoutTags.Should().Be("this is one font to block");
        }


        [Fact]
        public void LinkWithPlusAndMinosPropertiesAreParsedAsFull()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='https://itunes.apple.com/cz/app/skoda-media-services/id420627875?mt=8&utm_source=32449-Importers&utm_medium=email&utm_term=1908124336&utm_content=iOS&utm_campaign=Modern+and+intuitive:+SKODA+Media+Services+app+featuring+a+new+design+and+additional+functions-2'>fiets</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Keys.Contains("href").Should().BeTrue();
            blocks[1].TagProperties.Values.Contains("https://itunes.apple.com/cz/app/skoda-media-services/id420627875?mt=8&utm_source=32449-Importers&utm_medium=email&utm_term=1908124336&utm_content=iOS&utm_campaign=Modern+and+intuitive:+SKODA+Media+Services+app+featuring+a+new+design+and+additional+functions-2").Should().BeTrue();
            blocks[1].FontTag.FontAction.Should().Be(FontTagAction.Link);

            resultWithoutTags.Should().Be("this is one fiets to block");
        }


        //
        [Fact]
        public void LinksSlashAtEndAreResolvedLikeAnyLink()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='http://www.google.com/maps/'>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Keys.Contains("href").Should().BeTrue();
            blocks[1].TagProperties.Values.Contains("http://www.google.com/maps/").Should().BeTrue();
            blocks[1].FontTag.FontAction.Should().Be(FontTagAction.Link);

            resultWithoutTags.Should().Be("this is one font to block");
        }

        [Fact]
        public void TagsCanContainMultipleAttributes()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "q"));

            string resultWithoutTags;

            string text = "this is one <q la=1 la2=2>font</q> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            blocks.Count.Should().Be(3);

            blocks[1].TagProperties.Keys.Contains("la").Should().BeTrue();
            blocks[1].TagProperties.Keys.Contains("la2").Should().BeTrue();

            resultWithoutTags.Should().Be("this is one font to block");
        }

        [Fact]
        public void TagWithoutClosingTagShould()
        {
            AssetPlugin plugin = new TestAssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

            string text = "this is <b>one<b> block";
            string resultWithoutTags;

            Assert.Throws(typeof(Exception), () => AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags));
        }

		[Fact]
		public void DoubleTagsShouldBeFormatted()
		{
            AssetPlugin plugin = new TestAssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "strong"));

			string expectedResultWithoutTags = "TestExclusieve scherpe prijsTest tot";
			string resultWithoutTags;
            string text = "Test<strong>Exclusieve scherpe prijs</strong>Test<strong> tot</strong>";

			AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
		    resultWithoutTags.Should().Be(expectedResultWithoutTags);
        }

        //TODO Test no font
        //TODO Test no text
        //TODO Test tags
        //TODO Test tags with no closing tags

    }
}
