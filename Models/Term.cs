using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terms.Models
{
    [Table("Term")]
    public class Term
    {
        [PrimaryKey,AutoIncrement]
        public int termId { get; set; }
        public string termName { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
