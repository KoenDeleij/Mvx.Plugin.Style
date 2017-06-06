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

		private const string FontH1 = "H1";
		private const string FontRegular = "Regular";
		private const string FontItalic = "Italic";
		private const string FontBold = "Bold";
		private const string ColorPrimary = "Primary";
		private const string ColorSecondairy = "Secondairy";
		private const string ColorBackground = "Background";
		List<FontTag> _tags;

		public void Init() {
			Stories = StoryService.GetStories();

			_tags = new List<FontTag>();
			_tags.Add(new FontTag(FontItalic, "i"));
			_tags.Add(new FontTag(FontBold, "b"));

			SelectStoryCommand = new MvxCommand<Story>((story) => {
				UpdateStory(story);
			});

			ChangeStyleCommand = new MvxCommand<int>((styleNumber) => {
				Style1Selected = styleNumber==0;
				Style2Selected = !Style1Selected;

				AssetProvider.ClearFonts();
				AssetProvider.ClearColors();
				if (Style1Selected) {
					LoadStyle1();
				}
				else {
					LoadStyle2();
				}

				UpdateStyle = true;
				SelectStoryCommand.Execute(this._selectedStory);
			});

			ChangeStyleCommand.Execute(0);
			SelectStoryCommand.Execute(Stories.FirstOrDefault());
			RaisePropertyChanged(() => Stories);
		}

		private void LoadStyle1() {
			AssetProvider.AddColor(new MvxColor(42, 74, 99), ColorBackground)
			             .AddColor(new MvxColor(255, 255, 245), ColorPrimary)
			             .AddColor(new MvxColor(42, 183, 202), ColorSecondairy);
			AssetProvider.AddFont(new Font() { Name = FontH1, FontFilename = "JosefinSlab-Thin.ttf", FontPlatformName = "JosefinSlab-Thin", Size = 40, Color = AssetProvider.GetColor(ColorSecondairy) })
			             .AddFont(new Font() { Name = FontItalic, FontFilename = "Nunito-Italic.ttf", FontPlatformName = "Nunito-Italic", Size = 13, Color = AssetProvider.GetColor(ColorSecondairy), Alignment = TextAlignment.Right })
			             .AddFont(new Font() { Name = FontBold, FontFilename = "Nunito-Light.ttf", FontPlatformName = "Nunito-Light", Size = 13, Color = AssetProvider.GetColor(ColorSecondairy) })
			             .AddFont(new Font() { Name = FontRegular, FontFilename = "Nunito-Regular.ttf", FontPlatformName = "Nunito-Regular", Size = 13, Color = AssetProvider.GetColor(ColorPrimary),SelectedColor= AssetProvider.GetColor(ColorBackground), LineHeight = 20 },_tags);
		}

		private void LoadStyle2() {
			AssetProvider.AddColor(new MvxColor(0, 200, 190), ColorBackground)
			             .AddColor(new MvxColor(101, 18, 111), ColorPrimary)
			             .AddColor(new MvxColor(230, 229, 6), ColorSecondairy);
			AssetProvider.AddFont(new Font() { Name = FontH1, FontFilename = "JosefinSlab-Bold.ttf", FontPlatformName = "JosefinSlab-Bold", Size = 40, LineHeight = 50, Color = AssetProvider.GetColor(ColorSecondairy) })
			             .AddFont(new Font() { Name = FontItalic, FontFilename = "Nunito-Italic.ttf", FontPlatformName = "Nunito-Italic", Size = 15, Color = AssetProvider.GetColor(ColorSecondairy), Alignment = TextAlignment.Center })
			             .AddFont(new BaseFont() { Name = FontBold, FontFilename = "Nunito-Bold.ttf", FontPlatformName = "Nunito-Bold", Size = 15, Color = AssetProvider.GetColor(ColorSecondairy) })
			             .AddFont(new Font() { Name = FontRegular,FontFilename = "Nunito-Regular.ttf", FontPlatformName = "Nunito-Regular", Size = 13, LineHeight = 28, Color = AssetProvider.GetColor(ColorPrimary),SelectedColor= AssetProvider.GetColor(ColorBackground)},_tags);
				
		}

		private void UpdateStory(Story story) {
			SelectedStory = story;
		}

		private Story _selectedStory;
		public Story SelectedStory {
			get {
				return _selectedStory;
			}set {
                SetProperty(ref _selectedStory, value);
				RaisePropertyChanged(() => SelectedStoryTitle);
                RaisePropertyChanged(() => SelectedStoryParagraph);
			}
		}
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
