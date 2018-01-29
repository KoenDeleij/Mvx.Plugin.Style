using System;
using System.Collections.Generic;
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
            Assert.That(result.First().FontTag, Is.Null);//if the font is not overriden it's not set

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
            Assert.That(result[0].FontTag, Is.Null);

			Assert.That(result[1].StartIndex, Is.EqualTo(8));
			Assert.That(result[1].EndIndex, Is.EqualTo(11));
			Assert.That(result[1].FontTag.Tag, Is.EqualTo("b"));

			Assert.That(result[2].StartIndex, Is.EqualTo(11));
            Assert.That(result[2].EndIndex, Is.EqualTo(expectedResultWithoutTags.Length));
            Assert.That(result[2].FontTag, Is.Null);

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
            Assert.That(result[0].FontTag.Tag, Is.EqualTo("b"));

            Assert.That(result[1].StartIndex, Is.EqualTo(3));
            Assert.That(result[1].EndIndex, Is.EqualTo(9));
            Assert.That(result[1].FontTag, Is.Null);

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
        public void UnknownTagsShouldBeIgnored()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "strong"));

            string resultWithoutTags;

            string text = "las <p> this is <strong>one all</strong> block </p>";

            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));
            Assert.That(resultWithoutTags, Is.EqualTo("las <p> this is one all block </p>"));

            text = "las <strong>this is <p>one all</p> block</strong>";

            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(2));
            Assert.That(resultWithoutTags, Is.EqualTo("las this is <p>one all</p> block"));

            text = " this is <strong>one <a href='http://www.google.com'>font</a> to rule <a>them</a> all</strong> block ";
            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));
            Assert.That(resultWithoutTags, Is.EqualTo(" this is one <a href='http://www.google.com'>font</a> to rule <a>them</a> all block "));

            text = "<p> this is <strong>one <a href='http://www.google.com'>font</a> to rule <a>them</a> all</strong> block </p>";
            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));
            Assert.That(resultWithoutTags, Is.EqualTo("<p> this is one <a href='http://www.google.com'>font</a> to rule <a>them</a> all block </p>"));
        }
        /*
        [Test]
        public void TagsWithinTagsShouldResultInFlatListWithAllG()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });

            List<FontTag> tags = new List<FontTag>();
            tags.Add(new FontTag("Bold", "strong"));
            tags.Add(new FontTag("Bold", "a", FontTagAction.Link));

            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" },tags );

            string resultWithoutTags;

            string text = "las <strong> this is <a href='http://www.google.com'>one all</a> block </strong>";

            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));
            Assert.That(resultWithoutTags, Is.EqualTo("las this is one all block "));
        }
*/
        [Test]
        public void LinksShouldBeFlattenedAndPropertiesStripped()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a",FontTagAction.Link));

            string resultWithoutTags;

            //standard link
            string text = "this is one <a href=http://www.google.com>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties.Keys.Contains("href"), Is.True);
            Assert.That(blocks[1].TagProperties.Values.Contains("http://www.google.com"), Is.True);
            Assert.That(blocks[2].TagProperties, Is.Null);

            Assert.That(resultWithoutTags, Is.EqualTo("this is one font to block"));
        }

        [Test]
        public void LinksWithoutHrefShouldBeFlattenedAndPropertiesStripped()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link without attributes 
            var text = "this is one <a>http://www.google.com</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties, Is.Null);
            Assert.That(blocks[1].FontTag.FontAction, Is.EqualTo(FontTagAction.Link));

            Assert.That(resultWithoutTags, Is.EqualTo("this is one http://www.google.com to block"));
        }

        [Test]
        public void LinksWithQuotesShouldBeFlattenedAndPropertiesStripped()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='http://www.google.com'>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties.Keys.Contains("href"), Is.True);
            Assert.That(blocks[1].TagProperties.Values.Contains("http://www.google.com"), Is.True);
            Assert.That(blocks[1].FontTag.FontAction, Is.EqualTo(FontTagAction.Link));

            Assert.That(resultWithoutTags, Is.EqualTo("this is one font to block"));

            //link with double quotes 
            text = "this is one <a href=\"http://www.google.com\">font</a> to block";
            blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties.Keys.Contains("href"), Is.True);
            Assert.That(blocks[1].TagProperties.Values.Contains("http://www.google.com"), Is.True);
            Assert.That(blocks[1].FontTag.FontAction, Is.EqualTo(FontTagAction.Link));

            Assert.That(resultWithoutTags, Is.EqualTo("this is one font to block"));
        }

        [Test]
        public void LinksWithEqualSignShouldBeParsedAsFull()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='http://www.google.com/maps/la=3'>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties.Keys.Contains("href"), Is.True);
            Assert.That(blocks[1].TagProperties.Values.Contains("http://www.google.com/maps/la=3"), Is.True);
            Assert.That(blocks[1].FontTag.FontAction, Is.EqualTo(FontTagAction.Link));

            Assert.That(resultWithoutTags, Is.EqualTo("this is one font to block"));
        }


        [Test]
        public void LinkWithPlusAndMinosPropertiesAreParsedAsFull()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='https://itunes.apple.com/cz/app/skoda-media-services/id420627875?mt=8&utm_source=32449-Importers&utm_medium=email&utm_term=1908124336&utm_content=iOS&utm_campaign=Modern+and+intuitive:+SKODA+Media+Services+app+featuring+a+new+design+and+additional+functions-2'>fiets</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties.Keys.Contains("href"), Is.True);
            Assert.That(blocks[1].TagProperties.Values.Contains("https://itunes.apple.com/cz/app/skoda-media-services/id420627875?mt=8&utm_source=32449-Importers&utm_medium=email&utm_term=1908124336&utm_content=iOS&utm_campaign=Modern+and+intuitive:+SKODA+Media+Services+app+featuring+a+new+design+and+additional+functions-2"), Is.True);
            Assert.That(blocks[1].FontTag.FontAction, Is.EqualTo(FontTagAction.Link));

            Assert.That(resultWithoutTags, Is.EqualTo("this is one fiets to block"));
        }


        //
        [Test]
        public void LinksSlashAtEndAreResolvedLikeAnyLink()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "a", FontTagAction.Link));

            string resultWithoutTags;

            //link with quotes 
            string text = "this is one <a href='http://www.google.com/maps/'>font</a> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties.Keys.Contains("href"), Is.True);
            Assert.That(blocks[1].TagProperties.Values.Contains("http://www.google.com/maps/"), Is.True);
            Assert.That(blocks[1].FontTag.FontAction, Is.EqualTo(FontTagAction.Link));

            Assert.That(resultWithoutTags, Is.EqualTo("this is one font to block"));
        }

        [Test]
        public void TagsCanContainMultipleAttributes()
        {
            AssetPlugin plugin = new AssetPlugin();
            plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
            plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "q"));

            string resultWithoutTags;

            string text = "this is one <q la=1 la2=2>font</q> to block";
            var blocks = AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags);
            Assert.That(blocks.Count, Is.EqualTo(3));

            Assert.That(blocks[1].TagProperties.Keys.Contains("la"), Is.True);
            Assert.That(blocks[1].TagProperties.Keys.Contains("la2"), Is.True);

            Assert.That(resultWithoutTags, Is.EqualTo("this is one font to block"));
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
