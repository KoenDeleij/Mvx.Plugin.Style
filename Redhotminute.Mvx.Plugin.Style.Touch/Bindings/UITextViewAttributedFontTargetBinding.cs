using System;
using Foundation;
using UIKit;

namespace Redhotminute.Mvx.Plugin.Style.Touch.Bindings
{
    public class UITextViewAttributedFontTargetBinding : BaseStyleBinding<UITextView>
    {
        public UITextViewAttributedFontTargetBinding(UITextView target)
            : base(target)
        {
        }

        protected override void SetValueImpl(object target, object value)
        {
            var textView = (UITextView)target;
            if (value != null)
            {
                return;
            }

            try
            {
                var text = value as NSAttributedString;

                if(text == null){
                    return;
                }

                if (textView.TextAlignment != UITextAlignment.Natural && text.Length > 0)
                {
                    text.EnumerateAttributes(new NSRange(0, text.Length - 1), NSAttributedStringEnumeration.None, new NSAttributedRangeCallback(ResetAlignment));
                }

                text.EnumerateAttributes(new NSRange(0, text.Length - 1), NSAttributedStringEnumeration.None, new NSAttributedRangeCallback(FindLink));
            
                textView.AttributedText = text;
            }
            catch (Exception e)
            {
                MvvmCross.Platform.Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font+language to UITextView. Binded value is null or not an AttributedString.");
            }
        }

        private void FindLink(NSDictionary dic, NSRange range, ref bool stop)
        {
            if (dic.ContainsKey(new NSString("NSLink")))
            {
                NSObject val;
                dic.TryGetValue(new NSString("NSColor"), out val);
                UIColor color = val as UIColor;
                if(color!= null){
                    (Target as UITextView).TintColor = color;
                }
            }
        }

        private void ResetAlignment(NSDictionary dic, NSRange range, ref bool stop)
        {
            if (dic.ContainsKey(new NSString("NSParagraphStyle")))
            {
                //set the alignment to the alignment of the label
                NSObject val;
                dic.TryGetValue(new NSString("NSParagraphStyle"), out val);
                NSParagraphStyle style = val as NSParagraphStyle;
                if (style != null)
                {
                    style.Alignment = (this.Target as UITextView).TextAlignment;
                }
            }
        }
    }
}
