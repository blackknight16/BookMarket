//-----------------------------------------------------------------------
// Репозиторий  книги.
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
    public class BookRepository : IDbModelRepository
    {
        public ModelToModel GetAll()
        {
            ModelToModel mm;

            mm = new ModelToModel();
            mm.books = new List<Book>();
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                foreach (Book entry in db.GetAllBooks())
                {
                    mm.books.Add(entry);
                }
            }
            return mm;
        }

        public object Add(object item)
        {
            Book book = (Book)item;

            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                var ret = db.AddBook(book.Name, book.Year, book.PageCount, book.ImageName,
                    book.Description, book.LanguageId);
            }
            return GetAll().books.Last();
        }

        public object FindById(Int32 id)
        {
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                var items = db.FindBookById(id);
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
                foreach (var entry in db.GetBookNames())
                    names.Add(entry);
            }
            return names;
        }

        public List<SelectListItem> GetSelectItemList(
            List<Book> values
            )
        {
            Int32 i = 0;
            List<SelectListItem> items;
            IEnumerable<SelectListItem> selectList;

            items = new List<SelectListItem>();
            foreach (Book value in values)
            {
                items.Add(new SelectListItem
                {
                    Value = Convert.ToString(value.Id),
                    Text = value.Name
                });
                i++;
            }
            return items;
        }

        public IList<Book> GetAllBooksByTagId(Int32 tagId)
        {
            List<Book> books;
            BookTag tag;

            books = new List<Book>();
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                foreach (Book entry in db.GetAllBooksByTagId(tagId))
                    books.Add(entry);
            }

            return books;
        } 

        public IList<Book> GetAllBooksByTagName(string tagName)
        {
            List<Book> books;
            BookTag tag;

            books = new List<Book>();
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                foreach (Book entry in db.GetAllBooksByTagName(tagName))
                    books.Add(entry);
            }

            return books;
        } 

    }
}