using MvvmCross.Commands;
using MvvmCross.IoC;
using MvvmCross.Tests;
using Mvx.Plugin.Style.Models;
using Mvx.Plugin.Style.Plugin;
using Mvx.Plugin.Style.Tests.Helpers;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Mvx.Plugin.Style.Tests
{
    public class CustomTestFixture : MvxTestFixture
    {
        public readonly MvxUnitTestCommandHelper UnitTestCommandHelper;
        public readonly Font FontToAdd;

        public CustomTestFixture()
        {
            UnitTestCommandHelper = new MvxUnitTestCommandHelper();
            Ioc.RegisterSingleton<IMvxCommandHelper>(UnitTestCommandHelper);

            var plugin = new TestAssetPlugin();
            plugin.AddColor(System.Drawing.Color.FromArgb(255, 0, 0), "Red");
            plugin.AddColor(System.Drawing.Color.FromArgb(0, 0, 255), "Blue");
            FontToAdd = new Font() { Name = "Bold", FontFilename = "Bold.otf", Color = plugin.GetColor("Blue") };
            plugin.AddFont(FontToAdd);

            Ioc.RegisterSingleton<IAssetPlugin>(plugin);

        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions()
            {
                PropertyInjectorOptions =
                MvxPropertyInjectorOptions.MvxInject
            };
        }
    }
}