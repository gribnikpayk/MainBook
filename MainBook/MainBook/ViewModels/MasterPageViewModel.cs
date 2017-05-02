using System.Windows.Input;
using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;
using Xamarin.Forms;

namespace MainBook.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {
        private Color _masterPageBackgroundColor;
        private string _allFactsTitle, _readedFactsTitle, _favoriteFactsTitle, _nightModeTitle;
        private bool _backgroundTitleIsVisible;
        public ICommand AllFactsCommand { get; set; }
        public ICommand ReadedFactsCommand { get; set; }
        public ICommand FavoriteFactsCommand { get; set; }
        public ICommand NightModeCommand { get; set; }
        private ISettingsRepository _settingsRepository;
        public MasterPageViewModel(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }
        public Color MasterPageBackgroundColor
        {
            set { SetProperty(ref _masterPageBackgroundColor, value); }
            get { return _masterPageBackgroundColor; }
        }

        public string AllFactsTitle
        {
            set { SetProperty(ref _allFactsTitle, value); }
            get { return _allFactsTitle; }
        }

        public string ReadedFactsTitle
        {
            set { SetProperty(ref _readedFactsTitle, value); }
            get { return _readedFactsTitle; }
        }

        public bool BackgroundTitleIsVisible
        {
            set { SetProperty(ref _backgroundTitleIsVisible, value); }
            get { return _backgroundTitleIsVisible; }
        }
        public string FavoriteFactsTitle
        {
            set { SetProperty(ref _favoriteFactsTitle, value); }
            get { return _favoriteFactsTitle; }
        }

        public string NightModeTitle
        {
            set { SetProperty(ref _nightModeTitle, value); }
            get { return _nightModeTitle; }
        }

        public void SaveSettings()
        {
            _settingsRepository.SaveSettings(new SettingsEntity { IsNightMode = CommonData.IsNightMode, BGSource = CommonData.BGSource });
        }
    }
}
