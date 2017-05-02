

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Domain
{
    public class SettingsEntity:BaseEntity
    {
        public bool IsNightMode { get; set; }
        public string BGSource { get; set; }
    }
}
