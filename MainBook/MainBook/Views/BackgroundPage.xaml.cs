using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainBook.CustomControls;
using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.Resourses;
using MainBook.ViewModels;
using Xamarin.Forms;

namespace MainBook.Views
{
    public partial class BackgroundPage : ContentPage
    {
        private BackgroundPageViewModel _viewModel;
        private const string defaultMsg = "Выберите фон и нажмите 'Установить'";
        public BackgroundPage()
        {
            InitializeComponent();
            BackgroundImage = MediaResoursesHelper.GetMediaPath(CommonData.BGSource);
            _viewModel = App.Container.Resolve(typeof(BackgroundPageViewModel), "backgroundPageViewModel") as BackgroundPageViewModel;
            _viewModel.Msg = defaultMsg;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<BGPrevFrame, string>(this, "BGPrevFrame", (sender, args) =>
            {
                SetChoosenBG(args);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<BGPrevFrame, string>(this, "BGPrevFrame");
        }

        private void SetChoosenBG(string name)
        {
            var frames = MainWrapper.Children.Where(x => x is BGPrevFrame);
            _viewModel.BGName = name;
            _viewModel.Msg = defaultMsg;
            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (BGPrevFrame frame in frames)
                {
                    frame.BackgroundColor = frame.BGName == name
                        ? Color.FromHex("#6f43bd")
                        : Color.Transparent;
                }
                BackgroundImage = MediaResoursesHelper.GetMediaPath(name);
            });
        }
    }
}
