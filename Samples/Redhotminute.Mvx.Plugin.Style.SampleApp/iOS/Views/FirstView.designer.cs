// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS.Views
{
	[Register ("FirstView")]
	partial class FirstView
	{
		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel2 { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel3 { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel4 { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint StoriesHeightConstraint { get; set; }

		[Outlet]
		UIKit.UITableView StoriesTable { get; set; }

		[Outlet]
        ColoredButton Style1Button { get; set; }

		[Outlet]
		ColoredButton Style2Button { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HeaderLabel4 != null) {
				HeaderLabel4.Dispose ();
				HeaderLabel4 = null;
			}

			if (HeaderLabel3 != null) {
				HeaderLabel3.Dispose ();
				HeaderLabel3 = null;
			}

			if (HeaderLabel2 != null) {
				HeaderLabel2.Dispose ();
				HeaderLabel2 = null;
			}

			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}

			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
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
