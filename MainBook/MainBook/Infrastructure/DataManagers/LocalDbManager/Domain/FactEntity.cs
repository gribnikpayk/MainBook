namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Domain
{
    public class FactEntity:BaseEntity
    {
        public int ReadedFactName { get; set; }
        public bool IsFavorite { get; set; }
    }
}
