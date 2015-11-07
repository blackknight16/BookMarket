//-----------------------------------------------------------------------
// Контроллер предназначен для вывода каталога товаров магазина (книг).
// Он также является первым контроллером с которым начинает работать
// пользователь. Поэтому, логику создания БД поместил сюда (хотя такой
// код нужно переместить в отдельный класс).
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookMarket.DbInfrastructure;
using BookMarket.Models;
using BookMarket.Models.ViewModels;
using BookMarket.Services;

namespace BookMarket.Controllers
{
    public class ProductController : Controller
    {
        private static Int32 _dbCounter = 0;
        private IDbModelRepository _bookRepository = new BookRepository();

        public ProductController()
        {
            using (var context = new BookMarketDbContext())
            {
                //необходимо убрать _dbCounter из условия при развертывании 
                //приложения
                if (!context.Database.Exists() )//|| _dbCounter == 0)
                {
                    MarketDbInitializer.DB_WAS_NOT_EXIST = true;
                    _dbCounter++;
                    ((IObjectContextAdapter)context)
                        .ObjectContext.CreateDatabase();

                }
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        //В пагинации всегда выполняются 2 запроса, которых не избежать:
        //1.SELECT COUNT(*)
        //2.SELECT ... LIMIT offset, rowCount
        public ActionResult CatalogView(Int32 bookTagId, Int32 page = 1)
        {
            IList<BookView> bookViews;
            IEnumerable<BookView> booksPerPages;
            PageInfo pageInfo;
            IndexViewModel indexViewModel;

            //Получаем данные для модели
            var books = ((BookRepository)this._bookRepository).GetAllBooksByTagId(
                 bookTagId);
            bookViews = (from b in books 
                select new BookView(b)).ToList<BookView>();
            //Реализуем пагинацию страниц книг
            booksPerPages = bookViews.Skip((page - 1) * PageInfo.PAGE_SIZE)
                .Take(PageInfo.PAGE_SIZE);
            pageInfo = new PageInfo(page, bookViews.Count);
            indexViewModel = new IndexViewModel(
                pageInfo, booksPerPages, bookTagId);

            return View(indexViewModel);
        }

        public ActionResult ProductDetail(Int32 id)
        {
            Book book;
            BookView bookView;

            book = (Book)this._bookRepository.FindById(id);
            bookView = new BookView(book);

            return View(bookView);
        }
    }
}
