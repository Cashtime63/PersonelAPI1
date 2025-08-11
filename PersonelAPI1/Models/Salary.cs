using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PersonelAPI1.Data;
namespace PersonelAPI1.Models
{
    public class Salary
    {
        public int Id { get; set; }

        [Column("PersonelId")]
        public int EmployeeId { get; set; }

        [Column("NetMaas")]
        public decimal NetSalary { get; set; }

        [Column("BrutMaas")]
        public decimal GrossSalary { get; set; }

        [Column("MaasTarihi")]
        public DateTime SalaryDate { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public ICollection<ExtraPayment> ExtraPayments { get; set; } = new List<ExtraPayment>();
    }
}
