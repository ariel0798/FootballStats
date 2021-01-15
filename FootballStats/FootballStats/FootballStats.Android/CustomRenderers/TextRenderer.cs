using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using FootballStats.CustomElements;
using FootballStats.Droid.CustomRenderers;
using Xamarin.Forms;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomView),typeof(TextRenderer))]
namespace FootballStats.Droid.CustomRenderers
{
    public class TextRenderer : ViewRenderer<CustomView,TextView>
    {
        public TextRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<CustomView> e)
        {
            base.OnElementChanged(e);
            if(Control == null)
            {
                SetNativeControl(new TextView(Context)
                {
                    Text = "Football Stats was created to show statistics of football teams and football players.In that way anybody can know what thophies have their favorite player and how they are going this year.Besides that,it provides live games scores so you can know how are the games going."
                }) ;

                Control.SetTextSize(Android.Util.ComplexUnitType.Sp, 24);
                
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}