using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Terms.Models
{
    [Table("Course")]
    public class Course()
    {
        [PrimaryKey, AutoIncrement]
        public int courseId { get; set; }
        public int termID { get; set; }
        public string termName { get; set; }
        public string instructorName { get; set; }
        public string instructorPhone { get; set; }
        public string instructorEmail { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string paName { get; set; }
        public string oaName { get; set; }
        public DateTime paStart { get; set; }
        public DateTime paEnd { get; set; }
        public DateTime oaStart { get; set; }
        public DateTime oaEnd { get; set; }
        public bool paStartNotification { get; set; }
        public bool oaStartNotification { get; set; }
        public bool paEndNotification { get; set; }
        public bool oaEndNotification { get; set; }
        public bool startNotification {  get; set; }
        public bool endNotification { get; set; }
        public string noteDetails {  get; set; }
    }
}
