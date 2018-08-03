using System;
using MvvmCross.Logging;

namespace Redhotminute.Mvx.Plugin.Style.Plugin
{
    internal static class MvxPluginLog
    {
        internal static IMvxLog Instance { get; } = MvvmCross.Mvx.Resolve<IMvxLogProvider>().GetLogFor("MvxPlugin");
    }
}
