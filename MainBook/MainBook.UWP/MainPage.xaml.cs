

using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace MainBook.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(480, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            var isStatusBarPresent = ApiInformation.IsTypePresent(typeof(StatusBar).ToString());
            if (isStatusBarPresent)
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.HideAsync();
            }
            LoadApplication(new MainBook.App());
        }

    }
}
