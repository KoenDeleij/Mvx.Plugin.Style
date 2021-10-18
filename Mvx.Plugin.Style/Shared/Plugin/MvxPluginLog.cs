using Microsoft.Extensions.Logging;
using MvvmCross.IoC;

namespace Mvx.Plugin.Style.Plugin
{
    internal static class MvxPluginLog
    {
        internal static ILogger Instance { get; } = MvxIoCProvider.Instance.Resolve<ILoggerFactory>().CreateLogger("MvxPlugin");
    }
}
