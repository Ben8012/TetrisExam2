using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisExam.Appli.Model.SqlLite
{
    [Table("User")]
    public class UserSqlLite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250),Unique]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        public int Point { get; set; }

        public bool? IsActive { get; set; }

    }
}
