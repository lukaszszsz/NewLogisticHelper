using LogisticHelper.Models;

namespace LogisticHelper.Repository.IRepository
{
    public interface ITercRepository:IRepository<Terc>

    {
        void Update(Terc obj);
        void Save();
     
    }   
    
}
