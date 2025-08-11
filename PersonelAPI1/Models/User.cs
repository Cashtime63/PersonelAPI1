using System.ComponentModel.DataAnnotations.Schema;
using PersonelAPI1.Data;

namespace PersonelAPI1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Column("KullaniciAdi")]
        public string Username { get; set; }

        [Column("SifreHash")]
        public string PasswordHash { get; set; }

        [Column("Rol")]
        public string Role { get; set; }

        [Column("PersonelId")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}
