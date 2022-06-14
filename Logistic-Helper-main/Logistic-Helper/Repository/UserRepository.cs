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
            _db.Users.Update(obj);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
