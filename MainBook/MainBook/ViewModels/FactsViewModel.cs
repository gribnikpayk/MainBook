using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MainBook.CustomControls;
using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Implementations;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;
using MainBook.Infrastructure.Enums;
using MainBook.Infrastructure.Resourses;
using MainBook.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainBook.ViewModels
{
    public class FactsViewModel : BaseViewModel
    {
        private IFactService _factService;
        private Color _activityColor;
        private bool _isLoading;
        public List<FactEntity> AllFacts;
        public FactsViewModel(IFactService factService)
        {
            _factService = factService;
        }

        public Color ActivityColor
        {
            set { SetProperty(ref _activityColor, value); }
            get { return Color.White; }
        }

        public bool IsLoading
        {
            set { SetProperty(ref _isLoading, value); }
            get { return _isLoading; }
        }
        public FactFrame GetFact(TypeOfFact factType, int skip = 0)
        {
            switch (factType)
            {
                case TypeOfFact.All:
                    return _factService.GetFact();
                case TypeOfFact.Favorite:
                    return _factService.GetFavoriteFact(skip);
                case TypeOfFact.Readed:
                    return _factService.GetReadedFact(skip);
                default: return null;
            }
        }

        public string GetTitle(TypeOfFact factType)
        {
            switch (factType)
            {
                case TypeOfFact.All:
                    return "Все факты";
                case TypeOfFact.Favorite:
                    return "Избранное";
                case TypeOfFact.Readed:
                    return "Прочитанные";
                default: return "Все факты";
            }
        }
        public void SaveFactToStorage(FactEntity entity)
        {
            _factService.SaveFact(entity);
        }
    }
}
