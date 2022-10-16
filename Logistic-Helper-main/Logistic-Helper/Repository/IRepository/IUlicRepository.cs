using LogisticHelper.Models;

namespace LogisticHelper.Repository.IRepository
{
    public interface IUlicRepository:IRepository<Ulic>

    {
        void Update(Ulic obj);
        void Save();
     
    }   
    
}
