using System;
using System.ComponentModel.DataAnnotations.Schema;
using PersonelAPI1.Data;

namespace PersonelAPI1.Models
{
    public class UnpaidLeave
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

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}
