using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Core.Interfaces
{
    interface ISearchService
    {
        Task<IEnumerable<Result>> LookupAsync (Query request, SearchSites sites);
    }
}
