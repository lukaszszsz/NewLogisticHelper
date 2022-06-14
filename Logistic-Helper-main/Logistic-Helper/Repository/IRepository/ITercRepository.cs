using LogisticHelper.Models;

namespace LogisticHelper.Repository.IRepository
{
    public interface ITercRepository:IRepository<Terc>

    {
        void Update(Terc obj);
        void Save();

    }
    
}
//Add void Save9)
//Repair Repository
//Why GetAll doesnt work
//What the hell has happened?