using System;
using System.IO;
using MainBook.Infrastructure.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(App1.DependencyServices.SQLite))]
namespace App1.DependencyServices
{
    public class SQLite: ISQLite
    {
        public SQLite() { }
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }
    }
}