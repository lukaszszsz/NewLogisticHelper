using LogisticHelper.Repository.IRepository;
using LogisticHelper.Models;
using LogisticHelper.DataAccess;

namespace LogisticHelper.Repository
{
    public class UserRepository : Repository<User>, IUserRepository

    {
        private ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
 

        public void Update(User obj)
        {
            _db.User.Update(obj);
        }
    }
}
