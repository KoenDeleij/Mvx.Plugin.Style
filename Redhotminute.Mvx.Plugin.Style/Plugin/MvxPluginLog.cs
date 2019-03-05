using System;
using MvvmCross.Logging;

namespace Redhotminute.Mvx.Plugin.Style.Plugin
{
    internal static class MvxPluginLog
    {
        internal static IMvxLog Instance { get; } = MvvmCross.Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor("MvxPlugin");
    }
}
