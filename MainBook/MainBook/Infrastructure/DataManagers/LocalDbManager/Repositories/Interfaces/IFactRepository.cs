using System.Collections.Generic;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces
{
    public interface IFactRepository
    {
        void AddFact(FactEntity entity);
        void UpdateFact(FactEntity entity);
        List<FactEntity> GetAllFacts();
    }
}
