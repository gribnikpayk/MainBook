using System;
using System.Linq;
using MainBook.CustomControls;
using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;
using MainBook.Infrastructure.Resourses;
using Xamarin.Forms;

namespace MainBook.Services
{
    public class FactService:IFactService
    {
        private const int maxPercentOfReadedFacts = 70;
        private Random _rnd;
        private double _fontSize;

        private IFactRepository _factRepository;

        public FactService(IFactRepository factRepository)
        {
            _rnd = new Random();
            _fontSize = Device.GetNamedSize(NamedSize.Medium, typeof (Label));
            _factRepository = factRepository;
        }

        public FactFrame GetFact()
        {
            var factName = _rnd.Next(0, CommonData.FactCount);
            var s = (Facts.ResourceManager.GetString(factName.ToString()));
            if (FactMustBeUnique())
            {
                while (CommonData.AlreadyGeneratedNames.Any(x => x == factName) && CommonData.AllReadedFacts.Any(x => x.ReadedFactName == factName))
                {
                    factName += 1;
                }
            }
            CommonData.AlreadyGeneratedNames.Add(factName);
            var storedFact = CommonData.AllReadedFacts.FirstOrDefault(x => x.ReadedFactName == factName);

            return storedFact != null
                ? new FactFrame(Facts.ResourceManager.GetString(factName.ToString()),_fontSize,factName,storedFact.IsFavorite,true,storedFact.Id)
                : new FactFrame(Facts.ResourceManager.GetString(factName.ToString()), _fontSize, factName);
        }

        public FactFrame GetReadedFact(int skip)
        {
            var fact = CommonData.AllReadedFacts.Skip(skip).FirstOrDefault();
            return fact != null
                ? new FactFrame(Facts.ResourceManager.GetString(fact.ReadedFactName.ToString()), _fontSize,
                    fact.ReadedFactName, fact.IsFavorite, true, fact.Id)
                : null;
        }

        public FactFrame GetFavoriteFact(int skip)
        {
            var fact = CommonData.AllReadedFacts.Where(x => x.IsFavorite).Skip(skip).FirstOrDefault();
            return fact != null
                ? new FactFrame(Facts.ResourceManager.GetString(fact.ReadedFactName.ToString()), _fontSize,
                    fact.ReadedFactName, fact.IsFavorite, true, fact.Id)
                : null;
        }

        public void SaveFact(FactEntity entity)
        {
            if (entity.Id != 0)
            {
                _factRepository.UpdateFact(entity);
            }
            else
            {
                _factRepository.AddFact(entity);
            }
        }

        private bool FactMustBeUnique()
        {
            var percentOfReadedFacts = CommonData.ReadedFactCount/CommonData.FactCount*100;
            return percentOfReadedFacts < maxPercentOfReadedFacts;
        }
    }
}
