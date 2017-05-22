using System.Collections.Generic;
using System.Linq;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;

namespace MainBook.Infrastructure.CommonData
{
    public class CommonData
    {
        public static bool IsNightMode;
        public static int FactCount { get; set; }
        public static int ReadedFactCount => AllReadedFacts.Count;
        public static int FavoriteFactCount => AllReadedFacts.Count(x => x.IsFavorite);
        public static string BGSource { get; set; }
        public static List<FactEntity> AllReadedFacts;
        public static List<int> AlreadyGeneratedNames; 

        private IFactRepository _factRepository;
        private ISettingsRepository _settingsRepository;
        
        public CommonData(IFactRepository factRepository, ISettingsRepository settingsRepository)
        {
            _factRepository = factRepository;
            _settingsRepository = settingsRepository;
            FactCount = 411;
            AllReadedFacts = _factRepository.GetAllFacts();
            AlreadyGeneratedNames = new List<int>();
            var settings = _settingsRepository.GetSettings();
            IsNightMode = settings != null && settings.IsNightMode;
            BGSource = settings != null
                ? settings.BGSource
                : "BG_3.jpg";
        }
    }
}
