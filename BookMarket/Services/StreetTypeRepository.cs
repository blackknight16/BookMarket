//-----------------------------------------------------------------------
// Репозиторий типа улицы.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMarket.DbInfrastructure;
using BookMarket.Models;

namespace BookMarket.Services
{
    public class StreetTypeRepository : IDbModelRepository
    {
        public ModelToModel GetAll()
        {
            ModelToModel mm;

            mm = new ModelToModel();

            mm.streetTypes = new List<StreetType>();
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                foreach (StreetType entry in db.GetAllStreetTypes())
                {
                    mm.streetTypes.Add(entry);
                }
            }
            return mm;
        }

        public object Add(object item)
        {
            return null;
        }

        public object FindById(Int32 id)
        {
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                var items = db.FindStreetTypeById(id);
                var item = items.FirstOrDefault();
                return item;
            }
        }

        public IList<string> GetNames()
        {
            List<string> names;

            names = new List<string>();
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                foreach (var entry in db.GetStreetTypeNames())
                    names.Add(entry);
            }
            return names;
        }

    }
}