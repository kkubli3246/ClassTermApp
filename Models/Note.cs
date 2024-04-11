using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Terms.Models
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey,AutoIncrement]
        public int noteId { get; set; }
        public int courseId { get; set; }
        public string content { get; set; }
    }
}
