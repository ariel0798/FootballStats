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
                var view = (CustomView)Element;
                if (view == null) return;

                UILabel label = new UILabel
                {
                    Text = view.Text,
                    TextColor = UIKit.UIColor.Black,
                    Font = UIFont.SystemFontOfSize(18)
                };

                SetNativeControl(label);
            }
        }
    }
}