using LogisticHelper.Repository.IRepository;
using LogisticHelper.Models;
using LogisticHelper.DataAccess;

namespace LogisticHelper.Repository
{
    public class SimcRepository : Repository<Simc>, ISimcRepository

    {
        private ApplicationDbContext _db;

        public SimcRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
 

        public void Update(Simc obj)
        {
            _db.Simcs.Update(obj);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
   
    }
}
