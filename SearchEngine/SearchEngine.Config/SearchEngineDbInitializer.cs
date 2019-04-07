using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Config
{
    public class SearchEngineDbInitializer : DropCreateDatabaseAlways<SearchEngineDbContext> //TODO DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(SearchEngineDbContext context)
        {
            context.Queries.Add(new Query { Id = 1, Body = "The weather in Ukraine" });

            context.Results.Add(new Result { QueryId = 1, Title = "Sinoptik" });
            
            base.Seed(context);
        }
    }
}
