using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Terms.Models
{
    [Table("Instructors")]
    public class Instructor
    {
        [PrimaryKey,AutoIncrement]
        public int instructorId { get; set; }
        public string instructorName { get; set; }
        public string instructorPhone { get; set; }
        public string instructorEmail { get; set; }
    }
}
