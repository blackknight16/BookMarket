//-----------------------------------------------------------------------
// Репозиторий оплаты в WalletOne.
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
    public class W1PaymentRepository : IDbModelRepository
    {
        public ModelToModel GetAll()
        {
            ModelToModel mm;

            mm = new ModelToModel();

            mm.w1Payments = new List<W1Payment>();
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                foreach (W1Payment entry in db.GetAllW1Payments())
                {
                    mm.w1Payments.Add(entry);
                }
            }
            return mm;
        }

        public object Add(object item)
        {
            W1Payment w1Payment = (W1Payment)item;

            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                db.AddW1Payment(
                    w1Payment.WMI_MERCHANT_ID,
                    w1Payment.WMI_CURRENCY_ID, w1Payment.WMI_DESCRIPTION,
                    w1Payment.WMI_EXPIRED_DATE, w1Payment.WMI_PAYMENT_AMOUNT,
                    w1Payment.WMI_SIGNATURE);
            }
            return GetAll().w1Payments.Last();
        }

        public object FindById(Int32 id)
        {
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                var items = db.FindW1PaymentById(id);
                var item = items.FirstOrDefault();
                return item;
            }
        }

        public IList<string> GetNames()
        {
            return null;
        }

        public void Edit(W1Payment newW1Payment)
        {
            using (BookMarketDbContext db = new BookMarketDbContext())
            {
                db.EditW1Payment(newW1Payment.WMI_PAYMENT_NO,
                    newW1Payment.WMI_MERCHANT_ID, newW1Payment.WMI_CURRENCY_ID,
                    newW1Payment.WMI_DESCRIPTION, newW1Payment.WMI_EXPIRED_DATE,
                    newW1Payment.WMI_PAYMENT_AMOUNT, newW1Payment.WMI_SIGNATURE);
            }
        }
        
    }
}