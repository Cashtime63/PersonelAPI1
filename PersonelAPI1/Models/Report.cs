using PersonelAPI1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelAPI1.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Column("PersonelId")]
        public int EmployeeId { get; set; }

        [Column("BaslangicTarihi")]
        public DateTime StartDate { get; set; }

        [Column("BitisTarihi")]
        public DateTime EndDate { get; set; }

        [Column("Aciklama")]
        public string Description { get; set; }

        public Employee? Employee { get; set; }
    }
}
