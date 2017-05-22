using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.Constants;
using MainBook.Infrastructure.Resourses;
using Xamarin.Forms;

namespace MainBook.CustomControls
{
    public class LikeButton : Image
    {
        private const int weight = 65;
        public LikeButton()
        {
            WidthRequest = weight;
            HeightRequest = weight;
            VerticalOptions = LayoutOptions.Center;
            Source = CommonData.IsNightMode ? MediaResoursesHelper.GetMediaPath("like_dark_icon.png") : MediaResoursesHelper.GetMediaPath("like_light_icon.png");
            GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(async () =>
                    {
                        MessagingCenter.Send<LikeButton>(this, MessagingCenterConstants.LikeButtonPushed);
                        await this.ScaleTo(0.95, 100);
                        await this.ScaleTo(1, 100);
                    })
                });
        }
    }
}
