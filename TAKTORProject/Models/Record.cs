using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TAKTORProject.Models
{
    [Table("Record")]
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
        public string Time { get; set; }

    }
}