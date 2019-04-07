using SearchEngine.Config;
using SearchEngine.Core.Models;
using SearchEngine.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Repositories
{
    public class SearchEngineRepository : ISearchEngineRepository
    {
        public IEnumerable<Result> GetAllResults()
        {
            using (var db = new SearchEngineDbContext())
            {
                var results = db.Results.ToList();
                return results;
            }
        }

        public Result GetRecordById(int Id)
        {
            using (var db = new SearchEngineDbContext())
            {
                var result = db.Results.Where(r => r.Id == Id).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Result> GetRecordsByQuery(string query)
        {
            using (var db = new SearchEngineDbContext())
            {
                var results = db.Results.Where(r => r.Title.Contains(query) || r.Snippet.Contains(query)).ToList();
                return results;
            }
        }

        public bool Save(IEnumerable<Result> resultList, Query query)
        {
            using (var db = new SearchEngineDbContext())
            {
                try
                {
                    foreach (var result in resultList)
                    {
                        result.QueryId = query.Id;
                        result.Query = query;
                    }

                    db.Queries.Add(query);
                    db.Results.AddRange(resultList);

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool Save(Result result, Query query)
        {
            using (var db = new SearchEngineDbContext())
            {
                try
                {
                    result.QueryId = query.Id;
                    result.Query = query;
                    
                    db.Queries.Add(query);
                    db.Results.Add(result);

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
