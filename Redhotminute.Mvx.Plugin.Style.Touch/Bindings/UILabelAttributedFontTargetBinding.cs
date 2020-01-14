using System;
using UIKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross;
using Foundation;
using MvvmCross.Localization;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Bindings
{
	public class UILabelAttributedFontTargetBinding : BaseStyleBinding<UILabel>
	{
		public UILabelAttributedFontTargetBinding(UILabel target)
			: base(target)
		{
		}

		protected override void SetValueImpl(object target, object value)
		{
			var label = (UILabel)target;
            if (value == null)
            {
                label.AttributedText = new NSAttributedString();
                return;
            }

			try 
            {
				var text = value as NSAttributedString;

                if(text == null){
                    label.AttributedText = new NSAttributedString();
                    return;
                }

				//override the textalignment in case the view is not natural so that we can use the designer to deside the alignment (what you want in most cases)
                if (label.TextAlignment != UITextAlignment.Natural && text.Length > 0) {
					text.EnumerateAttributes(new NSRange(0, text.Length - 1), NSAttributedStringEnumeration.None, new NSAttributedRangeCallback(ResetAlignment));
				}
				label.AttributedText = text;
			}
			catch (Exception e) 
            {
                MvxBindingLog.Error("Failed to set font+language to UILabel. Binded value is null or not an AttributedString.");
			}
			
		}

		private void ResetAlignment(NSDictionary dic, NSRange range, ref bool stop) {
			if (dic.ContainsKey(new NSString("NSParagraphStyle"))) {
				//set the alignment to the alignment of the label
				NSObject val;
				dic.TryGetValue(new NSString("NSParagraphStyle"), out val);
				NSParagraphStyle style = val as NSParagraphStyle;
				if (style != null) {
					style.Alignment = (this.Target as UILabel).TextAlignment;
				}
			}
		}
	}
}