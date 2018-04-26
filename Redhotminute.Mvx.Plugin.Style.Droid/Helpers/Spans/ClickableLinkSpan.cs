﻿using System;
using Android.Content;
using Android.Text.Style;
using Android.Views;
using MvvmCross.Platform.Droid.Platform;

namespace Redhotminute.Mvx.Plugin.Style.Droid.Helpers.Spans
{
    public class ClickableLinkSpan : ClickableSpan
    {
        public string Link
        {
            get;
            set;
        }

        public override void OnClick(View widget)
        {
            if (!string.IsNullOrEmpty(Link))
            {
                string url = Link;
                Intent i = new Intent(Intent.ActionView);
                i.SetData(Android.Net.Uri.Parse(url));

                var top = MvvmCross.Platform.Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
                top.Activity.StartActivity(i);
            }
        }
    }
}
