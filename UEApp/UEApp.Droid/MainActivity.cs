using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

/* MainActivity handles some unique android only procedures
 */

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(UEApp.Droid.FlatButtonRenderer))]
[assembly: ExportRenderer(typeof(Xamarin.Forms.Switch), typeof(UEApp.Droid.CustomSwitchRendererDroid))]

namespace UEApp.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "campusLoop", Icon = "@drawable/ic_campusloop_launcher",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }

    // Makes buttons not have shadow around them
    public class FlatButtonRenderer : ButtonRenderer
    {
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
        }
    }

    // Custom renderer for switches
    public class CustomSwitchRendererDroid : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            Control.ShowText = false;

            if (Control.Checked)
                Control.ThumbDrawable.SetColorFilter(new Android.Graphics.Color(Android.Graphics.Color.ParseColor("#FFC107")), PorterDuff.Mode.SrcAtop);
            else
                Control.ThumbDrawable.SetColorFilter(new Android.Graphics.Color(245, 245, 245), PorterDuff.Mode.SrcAtop);

            Control.CheckedChange += (sender, e2) =>
        {
            ((IElementController)base.Element).SetValueFromRenderer(Xamarin.Forms.Switch.IsToggledProperty, Control.Checked);
            if (Control.Checked){
                Control.ThumbDrawable.SetColorFilter(new Android.Graphics.Color(Android.Graphics.Color.ParseColor("#FFC107")), PorterDuff.Mode.SrcAtop);
            }
            else{
                Control.ThumbDrawable.SetColorFilter(new Android.Graphics.Color(222, 222, 222), PorterDuff.Mode.SrcAtop);
            }

        };
        }
    }
    
}

