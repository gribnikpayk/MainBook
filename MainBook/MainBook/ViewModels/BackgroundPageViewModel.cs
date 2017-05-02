using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;
using Xamarin.Forms;

namespace MainBook.ViewModels
{
    public class BackgroundPageViewModel:BaseViewModel
    {
        private ISettingsRepository _settingsRepository;

        public ICommand SetBackground { get; set; }
        public string BGName { get; set; }
        private string _msg;
        public BackgroundPageViewModel(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            SetBackground = new Command(() =>
            {
                CommonData.BGSource = BGName;
                Msg = "Установлено!";
                _settingsRepository.SaveSettings(new SettingsEntity { BGSource = CommonData.BGSource, IsNightMode = CommonData.IsNightMode });
            });
        }

        public string Msg
        {
            set { SetProperty(ref _msg, value); }
            get { return _msg; }
        }
    }
}
