using LogisticHelper.Models;

namespace LogisticHelper.Repository.IRepository
{
    public interface IUserRepository:IRepository<User>

    {
        void Update(User obj);

    }
}
