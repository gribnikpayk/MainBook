using System;
using System.Linq.Expressions;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;
using SQLite;

namespace MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces
{
    public interface IBaseRepository<EntityType> where EntityType : BaseEntity, new()
    {
        EntityType GetById(int id);
        int Update(EntityType entity);
        int Delete(int id);
        int Create(EntityType entity);
        TableQuery<EntityType> GetQuery(Expression<Func<EntityType, bool>> expression = null);
        int DeleteAll();
    }
}