using LogisticHelper.Models;

namespace LogisticHelper.Repository.IRepository
{
    public interface IUserRepository:IRepository<User>

    {
        void Update(User obj);
        void Save();

    }
    
}
//Add void Save9)
//Repair Repository
//Why GetAll doesnt work
//What the hell has happened?