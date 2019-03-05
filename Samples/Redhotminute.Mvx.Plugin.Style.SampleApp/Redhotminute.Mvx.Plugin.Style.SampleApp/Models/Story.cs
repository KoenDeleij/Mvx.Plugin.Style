using Redhotminute.Mvx.Plugin.Style.Models;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp
{
    public class Story :IStylable{
		public string Title{
			get;
			set;
		}

		public string Subtitle {
			get;
			set;
		}

		public string Paragraph {
			get;
			set;
		}

		public string Empty {
			get;
			set;
		}
	}
}
