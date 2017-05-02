using SQLite;

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Domain
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
    }
}