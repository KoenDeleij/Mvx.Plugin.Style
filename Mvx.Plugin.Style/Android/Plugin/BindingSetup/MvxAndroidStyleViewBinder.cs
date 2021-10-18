using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Views;
using Microsoft.Extensions.Logging;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings;
using MvvmCross.Exceptions;
using MvvmCross.IoC;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Binding.Binders;
using MvvmCross.Platforms.Android.Binding.ResourceHelpers;
using Mvx.Plugin.Style.Bindings;

namespace Mvx.Plugin.Style.Droid.BindingSetup {
	public class MvxAndroidStyleViewBinder : MvxAndroidViewBinder{

		private readonly Lazy<IMvxAndroidBindingResource> mvxAndroidBindingResource
            = new Lazy<IMvxAndroidBindingResource>(() => MvxIoCProvider.Instance.GetSingleton<IMvxAndroidBindingResource>());
		private readonly object _source;
		public MvxAndroidStyleViewBinder(object source):base(source) {
			_source = source;
		}

		public override void BindView(View view, Context context, IAttributeSet attrs)
		{

			var resource = (mvxAndroidBindingResource.Value as MvxAndroidStyleBindingResource);

			if (resource != null)
			{
				using (var typedArray = context.ObtainStyledAttributes(attrs, mvxAndroidBindingResource.Value.BindingStylableGroupId))
				{

					int numStyles = typedArray.IndexCount;
					for (var i = 0; i < numStyles; ++i)
					{
						var attributeId = typedArray.GetIndex(i);

						if (attributeId == resource.BindingFontId)
						{
							this.ApplyFontBindingsFromAttribute(view, context, typedArray, attributeId);
						}
					}
					typedArray.Recycle();
				}
			}
			base.BindView(view, context, attrs);
		}

		private void ApplyFontBindingsFromAttribute(View view,Context context, TypedArray typedArray, int attributeId) {
			try {
				var bindingText = typedArray.GetString(attributeId);
				var newBindings = (this.Binder as MvxFromTextExtendedBinder).FontBind(this._source, view, bindingText);
				StoreBindings(view, newBindings);
			}
			catch (Exception exception) {
                MvxBindingLog.Instance.Log(LogLevel.Error, exception, "Exception thrown during the view font binding {0}",exception.ToLongString());
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
