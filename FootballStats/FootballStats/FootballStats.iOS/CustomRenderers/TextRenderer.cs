using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Label),typeof(LabelRenderer))]
namespace FootballStats.iOS.CustomRenderers
{
   public class TextRenderer : Xamarin.Forms.Platform.iOS.LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGB(66, 66, 66);
             
            }
        }
    }
}