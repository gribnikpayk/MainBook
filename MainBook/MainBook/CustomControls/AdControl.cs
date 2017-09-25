using Xamarin.Forms;

namespace MainBook.CustomControls
{
    public class AdControl: View
    {
        public static readonly BindableProperty ApplicationIdProperty =
            BindableProperty.Create("ApplicationId", typeof(string), typeof(AdControl));

        public static readonly BindableProperty AdUnitIdProperty =
            BindableProperty.Create("AdUnitId", typeof(string), typeof(AdControl));

        public string ApplicationId
        {
            get => (string)GetValue(ApplicationIdProperty);
            set => SetValue(ApplicationIdProperty, value);
        }

        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }
    }
}
