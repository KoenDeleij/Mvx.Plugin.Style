// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
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
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint StoriesHeightConstraint { get; set; }

		[Outlet]
		UIKit.UITableView StoriesTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
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

			if (StoriesTable != null) {
				StoriesTable.Dispose ();
				StoriesTable = null;
			}

			if (StoriesHeightConstraint != null) {
				StoriesHeightConstraint.Dispose ();
				StoriesHeightConstraint = null;
			}
		}
	}
}
