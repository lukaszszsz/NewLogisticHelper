using LogisticHelper.DataAccess;
using LogisticHelper.Repository.IRepository;

namespace LogisticHelper.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Terc = new TercRepository(_db);
        }

        public IUserRepository User
        { get; private set;   }
        public ITercRepository Terc
        { get; private set; } 
        public ITercRepository Simc
        { get; private set; }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
