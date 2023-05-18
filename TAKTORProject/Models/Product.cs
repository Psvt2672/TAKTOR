using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TAKTORProject.Models
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public string Province { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
    }
}
