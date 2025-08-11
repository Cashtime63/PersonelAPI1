using System.ComponentModel.DataAnnotations.Schema
using PersonelAPI1.Data;

namespace PersonelAPI1.Models
{
    public class ExtraPayment
    {
        public int Id { get; set; }

        [Column("MaasId")]
        public int SalaryId { get; set; }

        [Column("Tutar")]
        public decimal Amount { get; set; }

        [Column("Aciklama")]
        public string Description { get; set; }

        [ForeignKey("SalaryId")]
        public Salary? Salary { get; set; }
    }
}
