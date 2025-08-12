using PersonelAPI1.Models;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Employee? Employee { get; set; }

        public ICollection<UsedLeaveDay> UsedLeaveDays { get; set; } = new List<UsedLeaveDay>();
    }
}
