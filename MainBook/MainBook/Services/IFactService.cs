using MainBook.CustomControls;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Domain;

namespace MainBook.Services
{
    public interface IFactService
    {
        FactFrame GetFact();
        FactFrame GetReadedFact(int skip);
        FactFrame GetFavoriteFact(int skip);
        void SaveFact(FactEntity entity);
    }
}
