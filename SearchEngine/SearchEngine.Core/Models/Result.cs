using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Core.Models
{
    public class Result
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Link { get; set; }
        public string Snippet { get; set; }
        public string Source { get; set; }
        public int Index { get; set; }

        [ForeignKey("Query")]
        public int QueryId { get; set; }
        public Query Query { get; set; }
    }
}
