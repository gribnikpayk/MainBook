﻿using System.Threading.Tasks;
using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.Enums;
using MainBook.Infrastructure.Navigation;
using MainBook.ViewModels;
using Xamarin.Forms;

namespace MainBook.Views.MasterDetailPage
{
    public partial class MainPage : Xamarin.Forms.MasterDetailPage
    {
        private MasterPageViewModel _viewModel;
        private Label AllFactsTitle, ReadedFactsTitle, FavoriteFactsTitle, NightModeTitle, BackgroundTitle;
        public MainPage()
        {

            InitializeComponent();

            Detail = NaviagationService.CreateNavigationPage(new FactsPage(TypeOfFact.All));
            if (Device.OS != TargetPlatform.Android)
            {
                Icon = "Assets/burger.png";
            }
            MasterBehavior = MasterBehavior.Popover;
            _viewModel = App.Container.Resolve(typeof(MasterPageViewModel), "masterPageViewModel") as MasterPageViewModel;
            BindingContext = _viewModel;
            SetMenuPanel();
            IsPresentedChanged += (sender, args) => { SetMenuPanel(); };
            AllFactsTitle = new Label
            {
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                GestureRecognizers = { new TapGestureRecognizer {Command = new Command(() =>
                {
                    Task.Run(async () =>
                    {
                        await AllFactsTitle.FadeTo(0.5);
                        await AllFactsTitle.FadeTo(1);
                    });
                    Device.BeginInvokeOnMainThread(() =>
                        {
                            Detail = NaviagationService.CreateNavigationPage(new FactsPage(TypeOfFact.All));
                            IsPresented = false;
                        });
                })}}
            };
            ReadedFactsTitle = new Label
            {
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                GestureRecognizers = { new TapGestureRecognizer {Command = new Command(() =>
                {
                    Task.Run(async () =>
                    {
                        await ReadedFactsTitle.FadeTo(0.5);
                        await ReadedFactsTitle.FadeTo(1);
                    });
                    Device.BeginInvokeOnMainThread(() =>
                        {
                            Detail = NaviagationService.CreateNavigationPage(new FactsPage(TypeOfFact.Readed));
                            IsPresented = false;
                        });
                })}}
            };
            FavoriteFactsTitle = new Label
            {
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                GestureRecognizers = { new TapGestureRecognizer {Command = new Command(() =>
                {
                    Task.Run(async () =>
                    {
                        await FavoriteFactsTitle.FadeTo(0.5);
                        await FavoriteFactsTitle.FadeTo(1);
                    });
                    Device.BeginInvokeOnMainThread(() =>
                        {
                            Detail = NaviagationService.CreateNavigationPage(new FactsPage(TypeOfFact.Favorite));
                            IsPresented = false;
                        });
                })}}
            };
            NightModeTitle = new Label
            {
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                GestureRecognizers = { new TapGestureRecognizer {Command = new Command(() =>
                {
                    Task.Run(async () =>
                    {
                        await NightModeTitle.FadeTo(0.5);
                        await NightModeTitle.FadeTo(1);
                    });
                    CommonData.IsNightMode = !CommonData.IsNightMode;
                    SetMenuPanel();
                    Task.Run(() =>
                    {
                        _viewModel.SaveSettings();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Detail = NaviagationService.CreateNavigationPage(new FactsPage(TypeOfFact.All));
                            IsPresented = false;
                        });
                    });
                })}}
            };

            BackgroundTitle = new Label
            {
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = "Установить фон",
                GestureRecognizers = { new TapGestureRecognizer {Command = new Command(() =>
                {
                    Task.Run(async () =>
                    {
                        await BackgroundTitle.FadeTo(0.5);
                        await BackgroundTitle.FadeTo(1);
                    });
                    Task.Run(() =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Detail = NaviagationService.CreateNavigationPage(new BackgroundPage());
                            IsPresented = false;
                        });
                    });
                })}}
            };

            AllFactsTitle.SetBinding(Label.TextProperty, "AllFactsTitle");
            ReadedFactsTitle.SetBinding(Label.TextProperty, "ReadedFactsTitle");
            FavoriteFactsTitle.SetBinding(Label.TextProperty, "FavoriteFactsTitle");
            NightModeTitle.SetBinding(Label.TextProperty, "NightModeTitle");
            BackgroundTitle.SetBinding(Label.IsVisibleProperty, "BackgroundTitleIsVisible");

            Wrapper.Children.Add(AllFactsTitle);
            Wrapper.Children.Add(ReadedFactsTitle);
            Wrapper.Children.Add(FavoriteFactsTitle);
            Wrapper.Children.Add(NightModeTitle);
            Wrapper.Children.Add(BackgroundTitle);
        }

        public void SetMenuPanel()
        {
            var _turnOff_OnName = CommonData.IsNightMode ? "вкл." : "выкл.";
            _viewModel.BackgroundTitleIsVisible = !CommonData.IsNightMode;
            _viewModel.ReadedFactsTitle = $"Прочитанные ({CommonData.ReadedFactCount})";
            _viewModel.AllFactsTitle = $"Все факты ({CommonData.FactCount})";
            _viewModel.FavoriteFactsTitle = $"Избранное ({CommonData.FavoriteFactCount})";
            _viewModel.NightModeTitle = $"Ночной режим ({_turnOff_OnName})";
            _viewModel.MasterPageBackgroundColor = CommonData.IsNightMode ? Color.FromHex("#262b31") : Color.FromHex("#6f43bd");
        }
    }
}