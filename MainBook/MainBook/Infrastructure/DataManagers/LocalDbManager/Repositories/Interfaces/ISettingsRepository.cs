

using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        SettingsEntity GetSettings();
        void SaveSettings(SettingsEntity entity);
    }
}
