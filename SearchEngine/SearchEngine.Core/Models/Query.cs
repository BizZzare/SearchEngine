using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Core.Models
{
    public class Query
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime? Date { get; set; }

        public ICollection<Result> Results { get; set; }
    }
}
