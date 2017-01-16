using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
		[MvxInject]
		public IAssetPlugin AssetProvider { get; set; }

        private string _epicText = "Hello *EPIC* style plugin.";
        public string AttributedText
        { 
            get { return _epicText; }
            set { SetProperty (ref _epicText, value); }
        }
    }
}
