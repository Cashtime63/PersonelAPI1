using System;
using System.ComponentModel.DataAnnotations.Schema;
using PersonelAPI1.Data;

namespace PersonelAPI1.Models
{
    public class UsedLeaveDay
    {
        public int Id { get; set; }

        [Column("YillikIzinId")]
        public int AnnualLeaveId { get; set; }

        [Column("Tarih")]
        public DateTime Date { get; set; }

        [ForeignKey("AnnualLeaveId")]
        public AnnualLeave? AnnualLeave { get; set; }
    }
}
