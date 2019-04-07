using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.IRepositories
{
    public interface ISearchEngineRepository
    {
        IEnumerable<Result> GetAllResults();
        Result GetRecordById(int Id);
        IEnumerable<Result> GetRecordsByQuery(string query);
        bool Save(IEnumerable<Result> resultList, Query query);
        bool Save(Result result, Query query);
    }
}
