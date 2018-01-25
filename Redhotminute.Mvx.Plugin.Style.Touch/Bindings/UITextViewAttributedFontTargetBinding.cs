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
            var label = (UITextView)target;
            if (value != null)
            {
                try
                {
                    var text = value as NSAttributedString;

                    text.EnumerateAttributes(new NSRange(0, text.Length - 1), NSAttributedStringEnumeration.None, new NSAttributedRangeCallback(FindLink));
                
                    label.AttributedText = text;
                }
                catch (Exception e)
                {
                    MvvmCross.Platform.Mvx.Trace(MvvmCross.Platform.Platform.MvxTraceLevel.Error, "Failed to set font+language to UITextView. Binded value is null or not an AttributedString.");
                }
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
    }
}
