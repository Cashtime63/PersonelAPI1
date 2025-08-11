using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PersonelAPI1.Data;

namespace PersonelAPI1.Models
{
    public class AnnualLeave
    {
        public int Id { get; set; }

        [Column("PersonelId")]
        public int EmployeeId { get; set; }

        [Column("HakedilenGun")]
        public int EntitledDays { get; set; }

        [Column("KullanimYili")]
        public int UsageYear { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        [Column("KullanilanGunler")]
        public ICollection<UsedLeaveDay> UsedLeaveDays { get; set; } = new List<UsedLeaveDay>();
    }
}
