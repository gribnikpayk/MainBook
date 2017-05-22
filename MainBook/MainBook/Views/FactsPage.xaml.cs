using System.Linq;
using System.Threading.Tasks;
using MainBook.CustomControls;
using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.Constants;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.Enums;
using MainBook.Infrastructure.Navigation;
using MainBook.Infrastructure.Resourses;
using MainBook.ViewModels;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace MainBook.Views
{
    public partial class FactsPage : ContentPage
    {
        private FactsViewModel _viewModel;
        private TypeOfFact _factType;
        private bool _isNoFrames;
        private const int _defaultFrameCount = 5;
        public FactsPage(TypeOfFact factType)
        {
            InitializeComponent();
            _factType = factType;
            _viewModel = App.Container.Resolve(typeof(FactsViewModel), "factsViewModel") as FactsViewModel;
            BindingContext = _viewModel;
            Title = _viewModel.GetTitle(_factType);
            BackgroundImage = CommonData.IsNightMode ? MediaResoursesHelper.GetMediaPath("bg_dark.jpg") : MediaResoursesHelper.GetMediaPath(CommonData.BGSource);

            _viewModel.IsLoading = true;
            Task.Run(() => { InitialFirstFacts(); });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<NextButton_Right>(this, MessagingCenterConstants.NextButtonPushed, (sender) =>
            {
                var frame = GetDisplayedFrame();
                if (frame != null)
                {
                    FactReadingProcess(frame, false);
                }
            });
            MessagingCenter.Subscribe<NextButton_Left>(this, MessagingCenterConstants.NextButtonPushed, (sender)=>
            {
                var frame = GetDisplayedFrame();
                if (frame != null)
                {
                    FactReadingProcess(frame, true);
                }
            });
            MessagingCenter.Subscribe<ShareButton>(this,MessagingCenterConstants.ShareButtonPushed, (sender) =>
            {
                ShareAction();
            });
            MessagingCenter.Subscribe<LikeButton>(this,MessagingCenterConstants.LikeButtonPushed, (sender) =>
            {
                FavoriteAction();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<NextButton_Right>(this, MessagingCenterConstants.NextButtonPushed);
            MessagingCenter.Unsubscribe<NextButton_Left>(this, MessagingCenterConstants.NextButtonPushed);
            MessagingCenter.Unsubscribe<ShareButton>(this, MessagingCenterConstants.ShareButtonPushed);
            MessagingCenter.Unsubscribe<LikeButton>(this, MessagingCenterConstants.LikeButtonPushed);
        }

        public void FactReadingProcess(FactFrame factFrame, bool leftDirection)
        {
            factFrame.FactIsSwipped = true;
            if (!factFrame.FactIsReaded)
            {
                factFrame.FactIsReaded = true;
                var factEntity = new FactEntity
                {
                    Id = factFrame.Id,
                    IsFavorite = factFrame.IsFavorite,
                    ReadedFactName = factFrame.FactId
                };
                CommonData.AllReadedFacts.Add(factEntity);
                Task.Run(() =>
                {
                    _viewModel.SaveFactToStorage(factEntity);
                });
            }
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var fact = GetFactFrame(MainWrapper.Children.Count(x => x is FactFrame));
                    if (fact != null)
                    {
                        fact.Opacity = 0;
                        MainWrapper.Children.Insert(0, fact);
                        fact.FadeTo(1, 500);
                        var visibleFrame =
                            MainWrapper.Children.LastOrDefault(
                                x => (x as FactFrame) != null && !(x as FactFrame).FactIsSwipped);
                        SetToolbar(visibleFrame as FactFrame);
                    }
                    else if (!MainWrapper.Children.Any(x => (x as EmptyLabel) != null))
                    {
                        MainWrapper.Children.Insert(0, new EmptyLabel());
                    }
                });
            });
            if (leftDirection)
            {
                factFrame.RaiseSwipedLeft();
            }
            else
            {
                factFrame.RaiseSwipedRight();
            }

        }

        #region PrivateMethods
        private void InitialFirstFacts()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                for (int i = 0; i < _defaultFrameCount && !_isNoFrames; i++)
                {
                    var frame = GetFactFrame(MainWrapper.Children.Count(x => x is FactFrame));
                    if (frame == null)
                    {
                        _isNoFrames = true;
                        MainWrapper.Children.Insert(0, new EmptyLabel());
                        _viewModel.IsLoading = false;
                    }
                    else
                    {
                        MainWrapper.Children.Insert(0, frame);
                    }
                }
                if (MainWrapper.Children.Any(x => x is FactFrame))
                {
                    SetToolbar((MainWrapper.Children.LastOrDefault(x => (x as FactFrame) != null) as FactFrame));
                }
                _viewModel.IsLoading = false;
            });
        }

        private FactFrame GetFactFrame(int skip)
        {
            var frame = _viewModel.GetFact(_factType, skip);
            //if (frame != null)
            //{
            //    frame.SwipedLeft += (sender, args) => { FactReadingProcess(frame); };
            //    frame.SwipedRight += (sender, args) => { FactReadingProcess(frame); };
            //    frame.SwipDeltaY += (sender, args) => { MoveScrollView(frame, args); };
            //}
            var lastFact = (MainWrapper.Children[0] as FactFrame);
            if (_factType != TypeOfFact.All &&
                lastFact != null &&
                frame != null &&
                lastFact.Id == frame.Id)
            {
                return null;
            }
            return frame;
        }

        private void SetToolbar(FactFrame fact)
        {
            NaviagationService.SetToolbarItems(fact, ToolbarItems, FavoriteAction, async () => { await ShareAction(); });
        }

        private ShareMessage GetShareMessage()
        {
            var frame = GetDisplayedFrame();
            return frame != null ? new ShareMessage { Text = frame.Text, Title = "Интересный факт!" } : null;
        }
        private FactFrame GetDisplayedFrame()
        {
            return (MainWrapper.Children.LastOrDefault(x => (x as FactFrame) != null && !(x as FactFrame).FactIsSwipped) as
                    FactFrame);
        }

        //private void MoveScrollView(FactFrame factFrame, double y_delta)
        //{
        //    var scrollView = factFrame.Content as ScrollView;
        //    scrollView?.ScrollToAsync(0, y_delta, false);
        //}

        private async Task ShareAction()
        {
            var message = GetShareMessage();
            if (message != null)
            {
                await CrossShare.Current.Share(GetShareMessage());
            }
        }

        private void FavoriteAction()
        {
            var frame = GetDisplayedFrame();
            if (frame != null)
            {
                frame.IsFavorite = !frame.IsFavorite;
                frame.FactIsReaded = true;
                var factEntity = new FactEntity
                {
                    Id = frame.Id,
                    IsFavorite = frame.IsFavorite,
                    ReadedFactName = frame.FactId
                };

                var readedFact = CommonData.AllReadedFacts.FirstOrDefault(x => x.ReadedFactName == frame.FactId);
                if (readedFact != null)
                {
                    readedFact.IsFavorite = frame.IsFavorite;
                }
                else
                {
                    CommonData.AllReadedFacts.Add(factEntity);
                }
                Task.Run(() => { _viewModel.SaveFactToStorage(factEntity); });
                SetToolbar(frame);
            }
        }
        #endregion
    }
}
