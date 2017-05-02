using System.IO;
using Windows.Storage;
using MainBook.Infrastructure.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(MainBook.UWP.DependencyServices.SQLite))]
namespace MainBook.UWP.DependencyServices
{
    public class SQLite : ISQLite
    {
        public SQLite() { }
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}