using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TAKTORProject.Models
{
    [Table("Order")]
    public class Order //Model for Table Order
    {
        [PrimaryKey, AutoIncrement]
        public int OrderId { get; set; }

        [Indexed]
        public int ProductId { get; set; }
        [Indexed]
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public Uri Image { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
    }
}
