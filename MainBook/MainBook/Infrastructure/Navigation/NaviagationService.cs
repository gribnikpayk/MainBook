using System;
using System.Collections.Generic;
using System.Linq;
using MainBook.CustomControls;
using Xamarin.Forms;

namespace MainBook.Infrastructure.Navigation
{
    public static class NaviagationService
    {
        private static bool? _isFavorite = null;
        public static NavigationPage CreateNavigationPage(Page page)
        {
            var navPage = new NavigationPage(page)
            {
                BarBackgroundColor = CommonData.CommonData.IsNightMode ? Color.FromHex("#262b31") : Color.FromHex("#6f43bd"),
                HeightRequest = 50,
                BarTextColor = Color.White
            };
            return navPage;
        }
        public static void SetToolbarItems(FactFrame factFrame, IList<ToolbarItem> toolbarItems, Action favoriteAction, Action shareAction)
        {
            if (factFrame != null)
            {
                if (factFrame.IsFavorite == _isFavorite && toolbarItems.Any())
                {
                    toolbarItems[0].Command = new Command(favoriteAction);
                    toolbarItems[1].Command = new Command(shareAction);
                }
                else
                {
                    _isFavorite = factFrame.IsFavorite;
                    toolbarItems.Clear();
                    AddToolbarItems(factFrame, toolbarItems, favoriteAction, shareAction);
                }
            }
        }

        private static void AddToolbarItems(FactFrame factFrame, IList<ToolbarItem> toolbarItems, Action favoriteAction,
            Action shareAction)
        {
            toolbarItems.Add(new ToolbarItem
            {
                Icon = factFrame.IsFavorite ? "Assets/removeFavorite.png" : "Assets/addFavorite.png",
                Text = factFrame.IsFavorite ? "Удалить из избранных" : "Добавить в избранное",
                Command = new Command(favoriteAction)

            });
            toolbarItems.Add(new ToolbarItem
            {
                Icon = "Assets/share.png",
                Text = "Поделиться",
                Command = new Command(shareAction)
            });
        }
    }
}
