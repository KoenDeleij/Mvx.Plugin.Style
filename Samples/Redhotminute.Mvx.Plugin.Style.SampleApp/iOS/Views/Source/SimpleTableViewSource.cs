using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using System.Linq;
using MvvmCross.Binding.ExtensionMethods;

namespace Redhotminute.Mvx.Plugin.Style.SampleApp.iOS.Views {
	/// <summary>
	/// Set your contstraint to a fixed height. this will make sure your tableview with dynamic cells will extend fully
	/// </summary>
	public class SimpleTableViewSource : MvxSimpleTableViewSource {

		NSLayoutConstraint _heightConstraint;
		float _contraintOffset = 0.0f;
		public SimpleTableViewSource(UITableView tableView,Type cellType,NSString cellKey,NSLayoutConstraint heightConstraint = null,float contraintOffset = 0.0f)
			: base(tableView,cellType, cellKey) {
			tableView.RegisterNibForCellReuse(UINib.FromName(cellKey, NSBundle.MainBundle), cellKey);

			_contraintOffset = contraintOffset;
			_heightConstraint = heightConstraint;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item) {
			var cell = base.GetOrCreateCellFor(tableView, indexPath, item);
			cell.UpdateConstraintsIfNeeded();
			return cell;
		}


		protected override void CollectionChangedOnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args) {
			base.CollectionChangedOnCollectionChanged(sender, args);
			UpdateHeight();
		}

		public override System.Collections.IEnumerable ItemsSource {
			get {
				return base.ItemsSource;
			}
			set {
				//this makes sure that if the tableview is put in a scrollview, it scales the tableview based on the constraint to the full inset height
				//the constraint is only necessary when you want to expand the tableview within another tableview
				InvokeOnMainThread(() => {
					base.ItemsSource = value;
					this.TableView.LayoutIfNeeded();
					//note if you're using an observable, the itemsource will not be set
					UpdateHeight();
				});
			}
		}

		public void UpdateHeight() {
			var inset = this.TableView.ContentSize;
			if (inset.Height != 0 && _heightConstraint != null) {

				var offset = 0.0f;
				if (ItemsSource != null) {
					int items = this.ItemsSource.Count();
					if (_contraintOffset != 0 && items > 0) {
						offset = _contraintOffset * items;
					}
				}

				_heightConstraint.Constant = inset.Height + offset;
			}
		}
	}
}


