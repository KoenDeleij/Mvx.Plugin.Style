using System.Collections.Generic;
using System.Linq;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.UI;
using Redhotminute.Mvx.Plugin.Style.SampleApp.ViewModels;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS.Views
{
    public partial class FirstView : MvxViewController
    {
		MvxSimpleTableViewSource _storiesSource;
        public FirstView() : base("FirstView", null)
        {
        }

		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);

			if (NavigationController != null) {
				this.NavigationController.NavigationBarHidden = true;
			}
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			SetupTableSource();
			InitializeBinding();
        }

		private void InitializeBinding() {
			var set = this.CreateBindingSet<FirstView, FirstViewModel>();

			//Style selection
			this.BindFont(Style1Button, "Regular");
			set.Bind(Style1Button).For(v => v.Selected).To(vm => vm.Style1Selected);
			set.Bind(Style1Button).To(vm => vm.ChangeStyleCommand).CommandParameter(0);
            //set.Bind(Style1Button).For(v => v.TintColor).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Primary");
            set.Bind(Style1Button).For(v=>v.Text).To(vm => vm.Style1Text);
            set.Bind(Style1Button).For(v => v.SelectedBackground).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Secondairy");


			this.BindFont(Style2Button, "Regular");
			set.Bind(Style2Button).For(v => v.Selected).To(vm => vm.Style2Selected);
			set.Bind(Style2Button).To(vm => vm.ChangeStyleCommand).CommandParameter(1);
			//set.Bind(Style2Button).For(v => v.TintColor).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Primary");
            set.Bind(Style2Button).For(v => v.Text).To(vm => vm.Style2Text);
			set.Bind(Style2Button).For(v => v.SelectedBackground).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Secondairy");

			set.Bind(this).For(v =>v.UpdateStyles).To(vm => vm.UpdateStyle);

			//Story
			set.Bind(this).For(v => v.SelectedStory).To(vm => vm.SelectedStory);
			
            //Regular attributed binding
            set.Bind(HeaderLabel).For(v => v.AttributedText).To(vm => vm.SelectedStoryTitle).WithConversion("AttributedFontText", "H1");
			
            //Binding with color override
            set.Bind(HeaderLabel2).For(v => v.Text).To(vm => vm.SelectedStoryTitle);
            this.BindFont(HeaderLabel2,"Regular","Marked");

            //Binding with bindable color
			set.Bind(HeaderLabel3).For(v => v.Text).To(vm => vm.SelectedStoryTitle);
			this.BindFont(HeaderLabel3, "Regular");
            set.Bind(HeaderLabel3).For(v=>v.TextColor).To(vm=>vm.ColorNameBindingTest).WithConversion("AssetColor");

            //Binding with color override (different syntax)
			set.Bind(HeaderLabel4).For(v => v.Text).To(vm => vm.SelectedStoryTitle);
			this.BindFont(HeaderLabel4, "Regular:Marked");


			set.Bind(ContentLabel).For(v => v.AttributedText).To(vm => vm.SelectedStoryParagraph).WithConversion("AttributedFontText", "Regular");
			set.Bind(View).For(v => v.BackgroundColor).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Background");

			//Story selection
            //set.Bind(_storiesSource).To(vm => vm.Stories);
			set.Bind(this).For(v=>v.Stories).To(vm => vm.Stories);
			set.Bind(_storiesSource).For(v=>v.SelectionChangedCommand).To(vm => vm.SelectStoryCommand);
			set.Apply();
		}

        private List<Story> _stories;
        public List<Story> Stories
        {
            get{
                return _stories;
            }
            set{
                _stories = value;
                _storiesSource.ItemsSource = _stories;
                StoriesTable.ReloadData();
                SelectStory(_stories, _selectedStory);
            }
        }

        private Story _selectedStory;
		public Story SelectedStory {
			get {
				return _selectedStory;
			}set {
                _selectedStory = value;
				SelectStory(_stories, _selectedStory);
			}
		}

        private void SelectStory(List<Story> stories,Story story){
			if (stories != null)
			{
				var index = stories.IndexOf(story);
				var path = NSIndexPath.FromRowSection((System.nint)index, 0);
				StoriesTable.SelectRow(path, true, UITableViewScrollPosition.Top);
			}
        }

		public bool UpdateStyles {
			get {
				return false;
			}set {
				if (value) {
					ContentLabel.Text = string.Empty;
					HeaderLabel.Text = string.Empty;
					this.ClearAllBindings();
					InitializeBinding();
				}
			}
		}

		private void SetupTableSource() {
			_storiesSource = new SimpleTableViewSource(StoriesTable, typeof(StoryCell),StoryCell.Key,StoriesHeightConstraint);
			StoriesTable.Source = _storiesSource;
			StoriesTable.RowHeight = 50;
			StoriesTable.TranslatesAutoresizingMaskIntoConstraints = false; 
		}
    }
}
