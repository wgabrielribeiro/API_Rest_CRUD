using RestWithAspNET.API.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNET.API.Model
{
    [Table("Person")]
    public class Person : BaseEntity
    {
        [Column("first_name")]
        public string Firstname { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("Enabled")]
        public bool Enabled { get; set; }
    }
}
