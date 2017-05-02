using System.Threading.Tasks;
using Xamarin.Forms;

namespace MainBook.CustomControls
{
    public class BGPrevFrame : Frame
    {
        public BGPrevFrame()
        {
            VerticalOptions = LayoutOptions.Start;
            HorizontalOptions = LayoutOptions.Start;
            Padding = new Thickness(2);

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Task.Run(async () =>
                {
                    await this.ScaleTo(0.8, 100);
                    await this.ScaleTo(1, 100);
                    MessagingCenter.Send(this, "BGPrevFrame", BGName);
                }))
            });
        }

        public readonly static BindableProperty BGNameProperty = BindableProperty.Create("BGName", typeof(string), typeof(BGPrevFrame), "", BindingMode.OneWay, null, null, null, null, null);

        public string BGName
        {
            get
            {
                return (string)base.GetValue(BGPrevFrame.BGNameProperty);
            }
            set
            {
                base.SetValue(BGPrevFrame.BGNameProperty, value);
            }
        }
    }
}
