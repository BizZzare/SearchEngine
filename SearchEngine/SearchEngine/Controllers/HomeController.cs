using SearchEngine.Config;
using SearchEngine.Core.Models;
using SearchEngine.Core.Services;
using SearchEngine.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SearchEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchSites m_sites;
        private readonly CommonSearchService m_searchService;
        private readonly SearchEngineRepository m_repository;
        SearchEngineDbContext db;

        public HomeController()
        {
            db = new SearchEngineDbContext();
            m_searchService = new CommonSearchService();
            m_repository = new SearchEngineRepository();
            m_sites = new SearchSites
            {
                GoogleUri = "https://www.google.com/search?q=",
                BingUri = "https://www.bing.com/search?q=",
                YandexUri = "https://www.yandex.com/search/?text="
            };
        }

        public ActionResult Index()
        {
            return View();
        }

        #region WebSearch

        [HttpPost]
        public async Task<ActionResult> WebSearch(string searchString)
        {
            var query = new Query()
            {
                Body = searchString,
                Date = DateTime.Now
            };

            var list = await m_searchService.LookupAsync(query, m_sites);

            m_repository.Save(list, query);

            return View("Index",list);
        }

        #endregion

        #region DbSearch

        public ActionResult DbLookup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DbSearch(string searchString)
        {
            var list = m_repository.GetRecordsByQuery(searchString);

            return View("Index", list);
        }

        #endregion
    }
}