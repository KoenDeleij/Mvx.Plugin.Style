using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS.Views {

	[Register("ColoredButton")]
	public class ColoredButton :UIButton {
		public ColoredButton() : base() {
			InitView();
		}

		public ColoredButton(IntPtr h): base(h){
			InitView();
		}
		protected virtual void InitView()
		{
			this.Layer.CornerRadius = 5f;
			this.ClipsToBounds = true;
			this.Layer.BorderWidth = 1.5f;

			this.TintColor = UIColor.Clear;
			this.BorderColor = UIColor.White.ColorWithAlpha(0.0f);
            this.Background = UIColor.Clear;
            this.SelectedBackground = UIColor.Clear;
		}

		public UIColor BorderColor
		{
			get => UIColor.FromCGColor(this.Layer.BorderColor);
			set => this.Layer.BorderColor = value.CGColor;
		}

        private UIColor _selectedBackground;
		public UIColor SelectedBackground
		{
            get{
                return _selectedBackground;
            }
            set{
                _selectedBackground = value;
                CheckSelectedBackground();
            }
		}

		public UIColor Background
		{
			get;
            set;
		}

        public override bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                base.Selected = value;
                CheckSelectedBackground();
            }
        }

		public string Text {
			get {
				return this.CurrentTitle;
			}
			set {
				this.SetTitle(value, UIControlState.Normal);
				this.SetTitle(value, UIControlState.Disabled);
                this.SetTitle(value, UIControlState.Selected);
			}
		}

        private void CheckSelectedBackground(){
            this.Layer.BackgroundColor = Selected ? SelectedBackground.CGColor:Background.CGColor;
        }
	}
}
