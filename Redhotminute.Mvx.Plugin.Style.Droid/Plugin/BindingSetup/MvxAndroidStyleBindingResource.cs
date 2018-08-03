using System;
using MvvmCross.Base;
using MvvmCross.Binding;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Binding.ResourceHelpers;

namespace Redhotminute.Mvx.Plugin.Style.Droid.BindingSetup {
	//TODO find a better way to extend the MvxAndroidBindingResource
    public class MvxAndroidStyleBindingResource
        : MvxSingleton<IMvxAndroidBindingResource>
        , IMvxAndroidBindingResource
    {
        public static void Initialize()
        {
            if (Instance != null)
                return;

            var androidBindingResource = new MvxAndroidStyleBindingResource();
        }

        private MvxAndroidStyleBindingResource()
        {
            var finder = MvvmCross.Mvx.Resolve<IMvxAppResourceTypeFinder>();
            var resourceType = finder.Find();
            try
            {
                var id = resourceType.GetNestedType("Id");
                BindingTagUnique = (int)SafeGetFieldValue(id, "MvxBindingTagUnique");

                var styleable = resourceType.GetNestedType("Styleable");

                ControlStylableGroupId =
                    (int[])SafeGetFieldValue(styleable, "MvxControl", new int[0]);
                TemplateId =
                    (int)SafeGetFieldValue(styleable, "MvxControl_MvxTemplate");

                BindingStylableGroupId =
                    (int[])SafeGetFieldValue(styleable, "MvxBinding", new int[0]);
                BindingBindId =
                    (int)SafeGetFieldValue(styleable, "MvxBinding_MvxBind");
                BindingLangId =
                    (int)SafeGetFieldValue(styleable, "MvxBinding_MvxLang");

                ListViewStylableGroupId =
                    (int[])SafeGetFieldValue(styleable, "MvxListView");
                ListItemTemplateId =
                    (int)
                    styleable
                        .GetField("MvxListView_MvxItemTemplate")
                        .GetValue(null);
                DropDownListItemTemplateId =
                    (int)
                    styleable
                        .GetField("MvxListView_MvxDropDownItemTemplate")
                        .GetValue(null);

                ExpandableListViewStylableGroupId =
                    (int[])SafeGetFieldValue(styleable, "MvxExpandableListView", new int[0]);
                GroupItemTemplateId =
                    (int)SafeGetFieldValue(styleable, "MvxExpandableListView_MvxGroupItemTemplate");

                //NOTE ADDITION
                this.BindingFontId =
                    (int)SafeGetFieldValue(styleable, "MvxBinding_MvxFont");
            }
            catch (Exception exception)
            {
                throw exception.MvxWrap(
                    "Error finding resource ids for MvxBinding - please make sure ResourcesToCopy are linked into the executable");
            }
        }

        private static object SafeGetFieldValue(Type styleable, string fieldName)
        {
            return SafeGetFieldValue(styleable, fieldName, 0);
        }

        private static object SafeGetFieldValue(Type styleable, string fieldName, object defaultValue)
        {
            var field = styleable.GetField(fieldName);
            if (field == null)
            {
                MvxBindingLog.Error("Missing stylable field {0}", fieldName);
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

        public int[] ListViewStylableGroupId { get; }
        public int ListItemTemplateId { get; }
        public int DropDownListItemTemplateId { get; }

        public int[] ExpandableListViewStylableGroupId { get; }
        public int GroupItemTemplateId { get; }

        //NOTE ADDITION
        public int BindingFontId { get; }
    }
}
