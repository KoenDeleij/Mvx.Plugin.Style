using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Commands;
using MvvmCross.IoC;
using MvvmCross.UI;
using MvvmCross.ViewModels;
using Redhotminute.Mvx.Plugin.Style.Models;
using Redhotminute.Mvx.Plugin.Style.Plugin;

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
        private const string FontRegularSmall = "RegularSmall";
        private const string FontItalic = "Italic";
        private const string FontBold = "Bold";

        private const string ColorPrimary = "Primary";
        private const string ColorSecondairy = "Secondairy";
        private const string ColorBackground = "Background";
        private const string ColorMarked = "Marked";
        List<FontTag> _tags;

        public override void Prepare() {
            Stories = StoryService.GetStories();

            _tags = new List<FontTag>();
            _tags.Add(new FontTag(FontItalic, "i"));
            _tags.Add(new FontTag(FontBold, "b"));
            _tags.Add(new FontTag(FontBold, "a",FontTagAction.Link));

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

            base.Prepare();
        }

        private void LoadStyle1() {
            
            AssetProvider.AddColor(new MvxColor(42, 74, 99), ColorBackground)
                         .AddColor(new MvxColor(255, 255, 245), ColorPrimary)
                         .AddColor(new MvxColor(42, 183, 202), ColorSecondairy)
                         .AddColor(new MvxColor(42, 183, 202), ColorMarked);

            var regularFont = new iOSFont() { Name = FontRegularSmall, FontFilename = "Nunito-Regular.ttf", FontPlatformName = "Nunito-Regular", Size = 12, LineHeight = 12, LineHeightMultiplier = 1.33f, Color = AssetProvider.GetColor(ColorPrimary), SelectedColor = AssetProvider.GetColor(ColorBackground) };
            var regularFontAndroid = Font.CopyFont<iOSFont, AndroidFont>(regularFont, FontRegularSmall);
            regularFontAndroid.LineHeightMultiplier = 1.16f;


            AssetProvider.AddFont(new Font() { Name = FontH1, FontFilename = "JosefinSlab-Thin.ttf", FontPlatformName = "JosefinSlab-Thin", Size = 40, LineHeight = 20, Color = AssetProvider.GetColor(ColorSecondairy),LineBreakMode= LineBreakMode.TruncateTail })
                         .AddFont(new Font() { Name = FontItalic, FontFilename = "Nunito-Italic.ttf", FontPlatformName = "Nunito-Italic", Size = 13, Color = AssetProvider.GetColor(ColorSecondairy), Alignment = TextAlignment.Right })
                         .AddFont(new Font() { Name = FontBold, FontFilename = "Nunito-Light.ttf", FontPlatformName = "Nunito-Light", Size = 13, Color = AssetProvider.GetColor(ColorSecondairy) })
                         .AddFont(new Font() { Name = FontRegular, FontFilename = "Nunito-Regular.ttf", FontPlatformName = "Nunito-Regular", Size = 16, LineHeight = 12, Color = AssetProvider.GetColor(ColorPrimary), SelectedColor = AssetProvider.GetColor(ColorBackground), }, _tags)//10 13
                         .AddFont(regularFont, _tags)
                         .AddFont(regularFontAndroid, _tags);
        }                               

        private void LoadStyle2() {
            AssetProvider.AddColor(new MvxColor(0, 200, 190), ColorBackground)
                         .AddColor(new MvxColor(101, 18, 111), ColorPrimary)
                         .AddColor(new MvxColor(230, 229, 6), ColorSecondairy)
                         .AddColor(new MvxColor(101, 18, 111), ColorMarked);
			AssetProvider.AddFont(new Font() { Name = FontH1, FontFilename = "JosefinSlab-Bold.ttf", FontPlatformName = "JosefinSlab-Bold", Size = 40, LineHeight = 50,LineHeightMultiplier=1.0f, Color = AssetProvider.GetColor(ColorSecondairy) })
                         .AddFont(new Font() { Name = FontItalic, FontFilename = "Nunito-Italic.ttf", FontPlatformName = "Nunito-Italic", Size = 15, Color = AssetProvider.GetColor(ColorSecondairy), Alignment = TextAlignment.Center })
                         .AddFont(new BaseFont() { Name = FontBold, FontFilename = "Nunito-Bold.ttf", FontPlatformName = "Nunito-Bold", Size = 15, Color = AssetProvider.GetColor(ColorSecondairy) })
                         .AddFont(new Font() { Name = FontRegular,FontFilename = "Nunito-Regular.ttf", FontPlatformName = "Nunito-Regular", Size = 13, LineHeight = 28, Color = AssetProvider.GetColor(ColorPrimary),SelectedColor= AssetProvider.GetColor(ColorBackground)},_tags)
                         .AddFont(new Font() { Name = FontRegularSmall, FontFilename = "Nunito-Regular.ttf", FontPlatformName = "Nunito-Regular", Size = 10, LineHeight = 32, Color = AssetProvider.GetColor(ColorPrimary), SelectedColor = AssetProvider.GetColor(ColorBackground) }, _tags);
        }

        public string ColorNameBindingTest => ColorMarked;

        private void UpdateStory(Story story) {
            SelectedStory = story;
        }

        private Story _selectedStory;
        public Story SelectedStory {
            get => _selectedStory;
            set {
                SetProperty(ref _selectedStory, value);
                RaisePropertyChanged(() => SelectedStoryTitle);
                RaisePropertyChanged(() => SelectedStoryParagraph);
            }
        }
        public List<Story> Stories {
            get;
            internal set;
        }

        public string SelectedStoryTitle=> _selectedStory.Title;

        public string TestEnters => $"LLL{Environment.NewLine}LYYDL{Environment.NewLine}LYYP";

        public string SelectedStoryParagraph => _selectedStory.Paragraph;

        private bool _style1Selected;
        public bool Style1Selected {
            get =>_style1Selected;
            set {
                SetProperty(ref _style1Selected, value);
            }
        }

        private bool _style2Selected;
        public bool Style2Selected {
            get =>_style2Selected;
            set => SetProperty(ref _style2Selected, value);
        }

        private bool _updateStyle = false;
        public bool UpdateStyle {
            get {
                var returnVal = _updateStyle;
                _updateStyle = false;
                return returnVal;
            }internal set {
                _updateStyle = value;
                RaisePropertyChanged(()=>UpdateStyle);
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

        public string Style1Text => "Style 1";

        public string Style2Text => "Style 2";
    }
}
