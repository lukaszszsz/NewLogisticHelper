using LogisticHelper.Repository.IRepository;
using LogisticHelper.Models;
using LogisticHelper.DataAccess;

namespace LogisticHelper.Repository
{
    public class TercRepository : Repository<Terc>, ITercRepository

    {
        private ApplicationDbContext _db;

        public TercRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
 

        public void Update(Terc obj)
        {
            _db.Tercs.Update(obj);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
   
    }
}
