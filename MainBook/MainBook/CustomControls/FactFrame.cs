
using System;
using Windows.UI.Xaml.Documents;
using MainBook.Infrastructure.CommonData;
using Xamarin.Forms;

namespace MainBook.CustomControls
{
    public class FactFrame : CustomFrame
    {
        public bool FactIsReaded { get; set; }
        public bool FactIsSwipped { get; set; }
        public int Id { get; set; }
        public int FactId { get; set; }
        public bool IsFavorite { get; set; }
        public string Text { get; set; }
        public FactFrame(string text, double fontSize,int factId, bool isFavorite = false, bool isReaded = false,int id = 0)
        {
            FactId = factId;
            IsFavorite = isFavorite;
            FactIsReaded = isReaded;
            Id = id;
            Rotation = GetRotation();
            FrameRotation = Rotation;
            InlineColor = CommonData.IsNightMode ? Color.FromHex("#15161a") : Color.White;
            OutlineColor = CommonData.IsNightMode ? Color.FromHex("#ce9e70") : Color.FromHex("#6433bb");
            BorderRadius = 10;
            BorderWidth = 2;
            Text = text;

            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(.5, .5, 400, 300));
            Content =
                    new ScrollView
                    {
                        Rotation = FrameRotation * (-1),
                        Content = new Label
                        {
                            TextColor = CommonData.IsNightMode ? Color.FromHex("#626368") : Color.FromHex("#4e5153"),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Text = text,
                            FontSize = fontSize
                        }
                    };
        }


        private double GetRotation()
        {
            Random random = new Random();
            return random.NextDouble() * (5 - -5) + -5;
        }
    }
}
