using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace Terms.Models
{
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int assessmentId { get; set; }
        public int type { get; set; }
        public string assessmentName { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int startNotif { get; set; }
        public int endNotif { get; set; }
        public string assessmentDetails { get; set; }
        public int courseId { get; set; }
        public DateTime dueDate { get; set; }
    }
}
