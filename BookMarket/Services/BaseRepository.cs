//-----------------------------------------------------------------------
// Базовый репозиторий. Можно удалить.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMarket.DbInfrastructure;

namespace BookMarket.Services
{
    public class BaseRepository : BookMarketDbContext
    {
    }
}