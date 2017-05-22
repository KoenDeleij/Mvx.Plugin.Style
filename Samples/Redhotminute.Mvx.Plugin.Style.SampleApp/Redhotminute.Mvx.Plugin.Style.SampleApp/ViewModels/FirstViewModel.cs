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

			ChangeStyleCommand = new MvxCommand<int>((styleNumber) => {
				Style1Selected = styleNumber==0;
				Style2Selected = !Style1Selected;

				AssetProvider.ClearFonts();
				AssetProvider.AddFont(new Font() { Name = "H1", FontFilename = "JosefinSlab-Thin.ttf", FontPlatformName = "JosefinSlab-Thin", Size = 40, Color = AssetProvider.GetColor("Secondairy") })
			      .AddFont(new Font() { Name = "Italic", FontFilename = "Nunito-Italic.ttf", FontPlatformName = "Nunito-Italic", Size = 13, Color = AssetProvider.GetColor("Primary"), Alignment = TextAlignment.Right }, "i")
				  .AddFont(new Font() { Name = "Bold", FontFilename = "Nunito-Light.ttf", FontPlatformName = "Nunito-Light", Size = 13, Color = AssetProvider.GetColor("Primary") }, "b")
				  .AddFont(new Font() { Name = "Regular", FontFilename = "Nunito-Regular.ttf", FontPlatformName = "Nunito-Regular", Size = 13, Color = AssetProvider.GetColor("Secondairy"), LineHeight = 20 });


				UpdateStyle = true;
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

		private bool _style1Selected;
		public bool Style1Selected {
			get {
				return _style1Selected;
			}set {
				SetProperty(ref _style1Selected, value);
			}
		}

		private bool _style2Selected;
		public bool Style2Selected {
			get {
				return _style2Selected;
			}
			set {
				SetProperty(ref _style2Selected, value);
			}
		}

		private bool _updateStyle = false;
		public bool UpdateStyle {
			get {
				var returnVal = _updateStyle;
				_updateStyle = false;
				return returnVal;
			}internal set {
				SetProperty(ref _updateStyle, value);
			}
		}

		public MvxCommand<Story> SelectStoryCommand{
			get;
			internal set;
		}

		public MvxCommand<int> ChangeStyleCommand {
			get;
			internal set;
		}
    }
}
