//-----------------------------------------------------------------------
// Репозиторий учетных записей пользователя интернет магазина.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMarket.DbInfrastructure;
using BookMarket.Models;

namespace BookMarket.Services
{

    public class UserProfileRepository : IDbModelRepository
    {
        public ModelToModel GetAll()
        {
            return null;
        }

        public object Add(object item)
        {
            return null;
        }

        public object FindById(Int32 id)
        {
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                var items = db.FindUserProfileById(id);
                var item = items.FirstOrDefault();
                return item;
            }
        }

        public UserProfile FindByUserName(string userName)
        {
            UserProfile item;

            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                var items = db.FindUserProfileByUserName(userName).ToList();
                item = items.FirstOrDefault<UserProfile>();
                return item;
            }
        }

        public IList<string> GetNames()
        {
            List<string> names;

            names = new List<string>();
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                foreach (var entry in db.GetUserProfileNames())
                    names.Add(entry);
            }
            return names;
        }

        public void Edit(UserProfile newUserProfile)
        {
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                db.EditUserProfile(newUserProfile.UserId, newUserProfile.UserName,
                    newUserProfile.IndividualId);
            }
        }
    }
}