using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FootballStats.CustomElements;
using FootballStats.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomView),typeof(TextRenderer))]
namespace FootballStats.iOS.CustomRenderers
{
   public class TextRenderer : ViewRenderer<CustomView,UILabel>
    {
        public TextRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                UILabel label = new UILabel
                {
                    Text = "Football Stats was created to show statistics of football teams and football players.In that way anybody can know what thophies have their favorite player and how they are going this year.Besides that, it provides live games scores so you can know how are the games going.",
                    Font = UIFont.SystemFontOfSize(24)
                };

                SetNativeControl(label);
            }
        }
    }
}