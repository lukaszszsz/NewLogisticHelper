using LogisticHelper.Repository.IRepository;
using LogisticHelper.Models;
using LogisticHelper.DataAccess;
using System.Linq.Expressions;

namespace LogisticHelper.Repository
{
    public class UlicRepository : Repository<Ulic>, IUlicRepository

    {
        private ApplicationDbContext _db;

        public UlicRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }


        public void Update(Ulic obj)
        {
            _db.Ulics.Update(obj);
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        

    }
}
