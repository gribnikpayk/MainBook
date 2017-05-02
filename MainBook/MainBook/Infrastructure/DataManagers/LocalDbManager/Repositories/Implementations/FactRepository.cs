using System;
using System.Collections.Generic;
using System.Linq;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Implementations
{
    public class FactRepository: BaseRepository<FactEntity>, IFactRepository
    {
        public void AddFact(FactEntity entity)
        {
            Create(entity);
        }

        public void UpdateFact(FactEntity entity)
        {
            Update(entity);
        }

        public List<FactEntity> GetAllFacts()
        {
            return GetQuery().ToList();
        }
    }
}
