
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            
            RegisterAppStart<ViewModels.FirstViewModel>();
        }

    }
}
