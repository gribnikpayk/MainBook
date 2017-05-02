
using System;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Implementations
{
    public class SettingsRepository : BaseRepository<SettingsEntity>, ISettingsRepository
    {
        public SettingsEntity GetSettings()
        {
            return GetQuery().FirstOrDefault();
        }

        public void SaveSettings(SettingsEntity entity)
        {
            DeleteAll();
            Create(entity);
        }
    }
}
