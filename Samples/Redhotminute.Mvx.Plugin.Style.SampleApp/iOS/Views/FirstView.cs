using System.Linq;
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

			//TODO remove me, test
            // this.View.AddGestureRecognizer(new UITapGestureRecognizer((obj) => {
			//	this.ClearAllBindings();
			//	InitializeBinding();
			//}));
        }

		private void InitializeBinding() {
			var set = this.CreateBindingSet<FirstView, FirstViewModel>();

			//Style selection
			this.BindFont(Style1Button, "Regular");
			set.Bind(Style1Button).For(v => v.Selected).To(vm => vm.Style1Selected);
			set.Bind(Style1Button).To(vm => vm.ChangeStyleCommand).CommandParameter(0);
			
			this.BindFont(Style2Button, "Regular");
			set.Bind(Style2Button).For(v => v.Selected).To(vm => vm.Style2Selected);
			set.Bind(Style2Button).To(vm => vm.ChangeStyleCommand).CommandParameter(1);

			set.Bind(this).For(v =>v.UpdateStyles).To(vm => vm.UpdateStyle);

			//Story
			set.Bind(HeaderLabel).For(v => v.AttributedText).To(vm => vm.SelectedStoryTitle).WithConversion("AttributedFontText", "H1");
			set.Bind(ContentLabel).For(v => v.AttributedText).To(vm => vm.SelectedStoryParagraph).WithConversion("AttributedFontText", "Regular");
			set.Bind(View).For(v => v.BackgroundColor).To(vm => vm.AssetProvider).WithConversion("AssetColor", "Background");

			//Story selection
			set.Bind(_storiesSource).To(vm => vm.Stories);
			set.Bind(_storiesSource).For(v=>v.SelectionChangedCommand).To(vm => vm.SelectStoryCommand);
			set.Apply();
		}

		public bool UpdateStyles {
			get {
				return false;
			}set {
				if (value) {
					this.ClearAllBindings();
					InitializeBinding();
				}
			}
		}

		private void SetupTableSource() {
			_storiesSource = new SimpleTableViewSource(StoriesTable, typeof(StoryCell),StoryCell.Key,StoriesHeightConstraint);
			StoriesTable.Source = _storiesSource;
			StoriesTable.RowHeight = 70;
			StoriesTable.TranslatesAutoresizingMaskIntoConstraints = false; 
		}
    }
}
