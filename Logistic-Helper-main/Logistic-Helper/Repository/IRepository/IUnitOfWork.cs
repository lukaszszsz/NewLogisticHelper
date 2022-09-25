using LogisticHelper.DataAccess;

namespace LogisticHelper.Repository.IRepository
{
    
    public interface IUnitOfWork
    {
        IUserRepository User{get;}
        ITercRepository Terc { get;}
        ISimcRepository Simc{ get;}

    void Save();

    }
}
