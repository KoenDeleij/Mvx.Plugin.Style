using System;
using MvvmCross.IoC;
using MvvmCross.Logging;

namespace Mvx.Plugin.Style.Droid.Plugin
{
    internal static class MvxPluginLog
    {
        internal static IMvxLog Instance { get; } = MvxIoCProvider.Instance.Resolve<IMvxLogProvider>().GetLogFor("StylePlugin");
    }
}
