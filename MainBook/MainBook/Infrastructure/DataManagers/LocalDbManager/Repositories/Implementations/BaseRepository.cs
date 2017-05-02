using System;
using System.Linq.Expressions;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;
using MainBook.Infrastructure.DependencyService;
using SQLite;

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Implementations
{
    public class BaseRepository<EntityType> : IBaseRepository<EntityType> where EntityType : BaseEntity, new()
    {
        private static object _locker = new object();

        protected SQLiteConnection Database;

        public BaseRepository()
        {
            string databasePath = Xamarin.Forms.DependencyService.Get<ISQLite>().GetDatabasePath("FactsDB.db");
            Database = new SQLiteConnection(databasePath);
            Database.CreateTable<EntityType>();
        }

        public EntityType GetById(int id)
        {
            lock (_locker)
            {
                return Database.Get<EntityType>(id);
            }
        }

        public int DeleteAll()
        {
            lock (_locker)
            {
                return Database.DeleteAll<EntityType>();
            }
        }

        public int Update(EntityType entity)
        {
            lock (_locker)
            {
                return Database.Update(entity);
            }
        }

        public int Create(EntityType entity)
        {
            lock (_locker)
            {
                return Database.Insert(entity);
            }
        }

        public int Delete(int id)
        {
            lock (_locker)
            {
                return Database.Delete<EntityType>(id);
            }
        }

        public TableQuery<EntityType> GetQuery(Expression<Func<EntityType, bool>> expression = null)
        {
            lock (_locker)
            {
                return expression == null ? Database.Table<EntityType>() : Database.Table<EntityType>().Where(expression);
            }
        }
    }
}