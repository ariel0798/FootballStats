using Android.Content;
using Android.Widget;
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
                var view = (CustomView)Element;
                if (view == null) return;

                SetNativeControl(new TextView(Context)
                {
                    Text = view.Text
                }) ;

                Control.SetTextColor(Android.Graphics.Color.Black);
                Control.SetTextSize(Android.Util.ComplexUnitType.Sp, 18);
                
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}