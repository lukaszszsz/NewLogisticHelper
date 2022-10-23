using LogisticHelper.DataAccess;

namespace LogisticHelper.Repository.IRepository
{
    
    public interface IUnitOfWork
    {
        IUserRepository User{get;}
        ITercRepository Terc { get;}
        ISimcRepository Simc { get;}
        IUlicRepository Ulic{ get;}

    void Save();

    }
}
