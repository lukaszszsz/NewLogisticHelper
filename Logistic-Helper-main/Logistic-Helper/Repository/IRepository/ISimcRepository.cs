using LogisticHelper.Models;

namespace LogisticHelper.Repository.IRepository
{
    public interface ISimcRepository:IRepository<Simc>

    {
        void Update(Simc obj);
        void Save();
     
    }   
    
}
