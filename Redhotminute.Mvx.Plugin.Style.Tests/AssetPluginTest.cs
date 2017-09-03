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
    }
}
