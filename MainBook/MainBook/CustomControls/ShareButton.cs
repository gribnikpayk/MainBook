using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.Constants;
using MainBook.Infrastructure.Resourses;
using Xamarin.Forms;

namespace MainBook.CustomControls
{
    public class ShareButton : Image
    {
        private const int weight = 65;
        public ShareButton()
        {
            WidthRequest = weight;
            HeightRequest = weight;
            VerticalOptions = LayoutOptions.Center;
            Source = CommonData.IsNightMode ? MediaResoursesHelper.GetMediaPath("share_dark_icon.png") : MediaResoursesHelper.GetMediaPath("share_light_icon.png");
            GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(async () =>
                    {
                        MessagingCenter.Send<ShareButton>(this, MessagingCenterConstants.ShareButtonPushed);
                        await this.ScaleTo(0.95, 100);
                        await this.ScaleTo(1, 100);
                    })
                });
        }
    }
}
