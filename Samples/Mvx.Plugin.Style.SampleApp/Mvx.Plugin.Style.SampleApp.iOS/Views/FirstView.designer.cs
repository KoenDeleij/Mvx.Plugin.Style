// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mvx.Plugin.Style.SampleApp.iOS.Views
{
	[Register ("FirstView")]
	partial class FirstView
	{
		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }

		[Outlet]
		UIKit.UITextView ContentText { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel2 { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel3 { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel4 { get; set; }

		[Outlet]
		UIKit.UITextView HeightTest1 { get; set; }

		[Outlet]
		UIKit.UITextView HeightTest2 { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint StoriesHeightConstraint { get; set; }

		[Outlet]
		UIKit.UITableView StoriesTable { get; set; }

		[Outlet]
		Mvx.Plugin.Style.SampleApp.iOS.Views.ColoredButton Style1Button { get; set; }

		[Outlet]
		Mvx.Plugin.Style.SampleApp.iOS.Views.ColoredButton Style2Button { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HeightTest1 != null) {
				HeightTest1.Dispose ();
				HeightTest1 = null;
			}

			if (HeightTest2 != null) {
				HeightTest2.Dispose ();
				HeightTest2 = null;
			}

			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}

			if (ContentText != null) {
				ContentText.Dispose ();
				ContentText = null;
			}

			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
			}

			if (HeaderLabel2 != null) {
				HeaderLabel2.Dispose ();
				HeaderLabel2 = null;
			}

			if (HeaderLabel3 != null) {
				HeaderLabel3.Dispose ();
				HeaderLabel3 = null;
			}

			if (HeaderLabel4 != null) {
				HeaderLabel4.Dispose ();
				HeaderLabel4 = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (StoriesHeightConstraint != null) {
				StoriesHeightConstraint.Dispose ();
				StoriesHeightConstraint = null;
			}

			if (StoriesTable != null) {
				StoriesTable.Dispose ();
				StoriesTable = null;
			}

			if (Style1Button != null) {
				Style1Button.Dispose ();
				Style1Button = null;
			}

			if (Style2Button != null) {
				Style2Button.Dispose ();
				Style2Button = null;
			}
		}
	}
}
