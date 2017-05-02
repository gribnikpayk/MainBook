namespace MainBook.Infrastructure.DependencyService
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}