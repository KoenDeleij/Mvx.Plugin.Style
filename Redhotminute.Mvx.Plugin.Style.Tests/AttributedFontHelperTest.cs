﻿using System;
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
		public void TagWithoutClosingTagShould()
		{
			AssetPlugin plugin = new AssetPlugin();
			plugin.AddFont(new BaseFont() { Name = "Bold", FontFilename = "Bold.otf" });
			plugin.AddFont(new BaseFont() { Name = "H1", FontFilename = "H1.otf" }, new FontTag("Bold", "b"));

			string text = "this is <b>one<b> block";
			string resultWithoutTags;

            Assert.Throws(typeof(Exception),()=>AttributedFontHelper.GetFontTextBlocks(text, "H1", plugin, out resultWithoutTags));
		}

        //TODO Test no font
        //TODO Test no text
        //TODO Test tags
        //TODO Test tags with no closing tags

    }
}
