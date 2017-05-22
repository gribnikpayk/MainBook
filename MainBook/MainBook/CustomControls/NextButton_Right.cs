using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.Constants;
using MainBook.Infrastructure.Resourses;
using Xamarin.Forms;

namespace MainBook.CustomControls
{
    public class NextButton_Right : Image
    {
        private const int weight = 80;
        public NextButton_Right()
        {
            Source = CommonData.IsNightMode ? MediaResoursesHelper.GetMediaPath("next_dark_icon.png") : MediaResoursesHelper.GetMediaPath("next_light_icon.png");
            WidthRequest = weight;
            HeightRequest = weight;
            VerticalOptions = LayoutOptions.Center;

            GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(async () =>
                    {
                        MessagingCenter.Send<NextButton_Right>(this, MessagingCenterConstants.NextButtonPushed);
                        await this.ScaleTo(0.95, 100);
                        await this.ScaleTo(1, 100);
                    })
                });
        }
    }
}
