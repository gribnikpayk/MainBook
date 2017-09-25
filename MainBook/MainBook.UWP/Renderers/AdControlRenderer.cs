using Windows.UI;
using Windows.UI.Xaml.Media;
using MainBook.CustomControls;
using MainBook.UWP.Renderers;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(AdControl), typeof(AdControlRenderer))]

namespace MainBook.UWP.Renderers
{
    public class AdControlRenderer : ViewRenderer<AdControl, Microsoft.Advertising.WinRT.UI.AdControl>
    {
        private AdControl _control;

        protected override void OnElementChanged(ElementChangedEventArgs<AdControl> e)
        {
            base.OnElementChanged(e);
            _control = e.NewElement as AdControl;
            if (_control != null)
            {
                var nativeAdControl = new Microsoft.Advertising.WinRT.UI.AdControl
                {
                    ApplicationId = _control.ApplicationId,
                    AdUnitId = _control.AdUnitId,
                    Height = _control.HeightRequest,
                    Width = _control.WidthRequest
                };
                base.SetNativeControl(nativeAdControl);
            }
        }
    }
}
