using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
		[MvxInject]
		public IAssetPlugin AssetProvider { get; set; }

		private string _header = "Jules Verne, Around the world in 80 days";
		public string Header {
			get { return _header; }
			set { SetProperty(ref _header, value); }
		}

		private string _p1 = "That gentleman was really ruined, and that at the moment when he was about to attain his end. This arrest was fatal. Having arrived at <b>Liverpool</b> at twenty minutes before twelve on the 21st of December, he had till a quarter before nine that evening to reach the Reform Club, that is, nine hours and a quarter; the journey from <b>Liverpool</b> to London was six hours.\r\n\r\n<i>If anyone, at this moment, had entered the Custom House, he would have found Mr. Fogg seated, motionless, calm, and without apparent anger, upon a wooden bench.</i>\r\n\r\nHe was not, it is true, resigned; but this last blow failed to force him into an outward betrayal of any emotion. Was he being devoured by one of those secret rages, all the more terrible because contained, and which only burst forth, with an irresistible force, at the last moment? No one could tell. There he sat, calmly waiting—for what? Did he still cherish hope? Did he still believe, now that the door of this prison was closed upon him, that he would succeed?";
		public string Paragraph1
        { 
			get { return _p1; }
            set { SetProperty (ref _p1, value); }
        }
    }
}
