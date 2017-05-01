using System.Collections.Generic;
using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.UI;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
		[MvxInject]
		public IAssetPlugin AssetProvider { get; set; }

		[MvxInject]
		public IStoryService StoryService { get; set; }

		public void Init() {
			Stories = StoryService.GetStories();
			SelectStoryCommand = new MvxCommand<Story>((story) => {
				UpdateStory(story);
			});

			ChangeStyleCommand = new MvxCommand(() => {
				//TODO
			});
			SelectStoryCommand.Execute(Stories.FirstOrDefault());
			RaisePropertyChanged(() => Stories);
		}

		private void UpdateStory(Story story) {
			_selectedStory = story;
            RaisePropertyChanged(() => SelectedStoryTitle);
			RaisePropertyChanged(() => SelectedStoryParagraph);
		}

		private Story _selectedStory;
		public List<Story> Stories {
			get;
			internal set;
		}

		public string SelectedStoryTitle{
			get {
				return _selectedStory.Title;
			}
		}

		public string SelectedStoryParagraph {
			get {
				return _selectedStory.Paragraph;
			}
		}

		public MvxCommand<Story> SelectStoryCommand{
			get;
			internal set;
		}

		public MvxCommand ChangeStyleCommand {
			get;
			internal set;
		}
    }
}
