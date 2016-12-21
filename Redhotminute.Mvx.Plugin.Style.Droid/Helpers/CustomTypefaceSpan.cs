using System;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;

namespace Redhotminute.Mvx.Plugin.Style.Droid {
	public class CustomTypefaceSpan :TypefaceSpan{
		private Typeface _newType;

		public CustomTypefaceSpan(String family, Typeface type):base(family) {
			_newType = type;
		}

		public override void UpdateDrawState(TextPaint ds) {
			ApplyCustomTypeFace(ds, _newType);
		}

		public override void UpdateMeasureState(TextPaint paint) {
			ApplyCustomTypeFace(paint, _newType);
		}

		private static void ApplyCustomTypeFace(Paint paint, Typeface tf) {
			
			paint.SetTypeface(tf);
		}
	}
}
