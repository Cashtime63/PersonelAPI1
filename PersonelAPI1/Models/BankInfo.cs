using System.ComponentModel.DataAnnotations.Schema;
using PersonelAPI1.Data;

namespace PersonelAPI1.Models
{
    public class BankInfo
    {
        public int Id { get; set; }

        [Column("PersonelId")]
        public int EmployeeId { get; set; }

        [Column("BankaAdi")]
        public string BankName { get; set; }

        public string IBAN { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}
