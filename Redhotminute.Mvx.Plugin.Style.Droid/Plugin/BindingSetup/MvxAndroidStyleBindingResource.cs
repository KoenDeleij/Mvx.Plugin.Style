using System;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.ResourceHelpers;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;

namespace Redhotminute.Mvx.Plugin.Style.Droid.BindingSetup {
	//TODO find a better way to extend the MvxAndroidBindingResource
public class MvxAndroidStyleBindingResource
		: MvxSingleton<IMvxAndroidBindingResource>
		, IMvxAndroidBindingResource {
		public static void Initialize() {
			if (Instance != null)
				return;

			var androidBindingResource = new MvxAndroidStyleBindingResource();
		}

		private MvxAndroidStyleBindingResource() {
			var finder = MvvmCross.Platform.Mvx.Resolve<IMvxAppResourceTypeFinder>();
			var resourceType = finder.Find();
			try {
				var id = resourceType.GetNestedType("Id");
				this.BindingTagUnique = (int)SafeGetFieldValue(id, "MvxBindingTagUnique");

				var styleable = resourceType.GetNestedType("Styleable");

				this.ControlStylableGroupId =
					(int[])SafeGetFieldValue(styleable, "MvxControl", new int[0]);
				this.TemplateId =
					(int)SafeGetFieldValue(styleable, "MvxControl_MvxTemplate");

				this.BindingStylableGroupId =
					(int[])SafeGetFieldValue(styleable, "MvxBinding", new int[0]);
				this.BindingBindId =
					(int)SafeGetFieldValue(styleable, "MvxBinding_MvxBind");
				this.BindingLangId =
					(int)SafeGetFieldValue(styleable, "MvxBinding_MvxLang");
				//NOTE ADDITION
				this.BindingFontId =
					(int)SafeGetFieldValue(styleable, "MvxBinding_MvxFont");
				
				this.ImageViewStylableGroupId =
					(int[])SafeGetFieldValue(styleable, "MvxImageView", new int[0]);
				this.SourceBindId =
					(int)
					SafeGetFieldValue(styleable, "MvxImageView_MvxSource");

				this.ListViewStylableGroupId =
					(int[])SafeGetFieldValue(styleable, "MvxListView");
				this.ListItemTemplateId =
					(int)
					styleable
						.GetField("MvxListView_MvxItemTemplate")
						.GetValue(null);
				this.DropDownListItemTemplateId =
					(int)
					styleable
						.GetField("MvxListView_MvxDropDownItemTemplate")
						.GetValue(null);

				this.ExpandableListViewStylableGroupId =
					(int[])SafeGetFieldValue(styleable, "MvxExpandableListView", new int[0]);
				this.GroupItemTemplateId =
					(int)SafeGetFieldValue(styleable, "MvxExpandableListView_MvxGroupItemTemplate");
			}
			catch (Exception exception) {
				throw exception.MvxWrap(
					"Error finding resource ids for MvxBinding - please make sure ResourcesToCopy are linked into the executable");
			}
		}

		private static object SafeGetFieldValue(Type styleable, string fieldName) {
			return SafeGetFieldValue(styleable, fieldName, 0);
		}

		private static object SafeGetFieldValue(Type styleable, string fieldName, object defaultValue) {
			var field = styleable.GetField(fieldName);
			if (field == null) {
				MvxBindingTrace.Trace(MvxTraceLevel.Error, "Missing stylable field {0}", fieldName);
				return defaultValue;
			}

			return field.GetValue(null);
		}

		public int BindingTagUnique { get; }

		public int[] BindingStylableGroupId { get; }
		public int BindingBindId { get; }
		public int BindingLangId { get; }

		public int[] ControlStylableGroupId { get; }
		public int TemplateId { get; }

		public int[] ImageViewStylableGroupId { get; }
		public int SourceBindId { get; }

		public int[] ListViewStylableGroupId { get; }
		public int ListItemTemplateId { get; }
		public int DropDownListItemTemplateId { get; }

		public int[] ExpandableListViewStylableGroupId { get; }
		public int GroupItemTemplateId { get; }

		//NOTE ADDITION
		public int BindingFontId { get; }
	}
}
