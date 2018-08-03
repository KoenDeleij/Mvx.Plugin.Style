using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Views;
using MvvmCross.Binding;
using MvvmCross.Binding.Binders;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Binding.Binders;
using Redhotminute.Mvx.Plugin.Style.Bindings;

namespace Redhotminute.Mvx.Plugin.Style.Droid.BindingSetup {
	public class MvxAndroidStyleViewBinder : MvxAndroidViewBinder{

		private readonly object _source;
		public MvxAndroidStyleViewBinder(object source):base(source) {
			_source = source;
		}

		public override void BindView(View view, Context context, IAttributeSet attrs) {
			
			using (
				var typedArray = context.ObtainStyledAttributes(attrs,MvxAndroidStyleBindingResource.Instance.BindingStylableGroupId)) {
				int numStyles = typedArray.IndexCount;

				var resource = (MvxAndroidStyleBindingResource.Instance as MvxAndroidStyleBindingResource);

				for (var i = 0; i < numStyles; ++i) {
					var attributeId = typedArray.GetIndex(i);

					if (attributeId == resource.BindingFontId) {
						this.ApplyFontBindingsFromAttribute(view,context, typedArray, attributeId);
					}
				}
				typedArray.Recycle();
				base.BindView(view, context, attrs);
			}
		}

		private void ApplyFontBindingsFromAttribute(View view,Context context, TypedArray typedArray, int attributeId) {
			try {
				var bindingText = typedArray.GetString(attributeId);
				var newBindings = (this.Binder as MvxFromTextExtendedBinder).FontBind(this._source, view, bindingText);
				StoreBindings(view, newBindings);
			}
			catch (Exception exception) {
                MvxBindingLog.Instance.Error(exception, "Exception thrown during the view font binding {0}",exception.ToLongString());
				throw;
			}
		}

		private void StoreBindings(View view, IEnumerable<IMvxUpdateableBinding> newBindings) {
			if (newBindings != null) {
				(this.CreatedBindings as List<KeyValuePair<object, IMvxUpdateableBinding>>).AddRange(newBindings.Select(b => new KeyValuePair<object, IMvxUpdateableBinding>(view, b)));
			}
		}
	}
}
