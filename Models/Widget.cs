using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terms.Models
{
    [Table("Widget")]
    public class Widget
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int GadgetId { get; set; } //foreign key from the Gadget class/table
        public string Name { get; set; }
        public string Color { get; set; }
        public int InStock { get; set; }
        public decimal Price { get; set; }
        public bool StartNotification { get; set; }
        public DateTime CreationDate { get; set; }
        public string Notes { get; set; }

    }
}
