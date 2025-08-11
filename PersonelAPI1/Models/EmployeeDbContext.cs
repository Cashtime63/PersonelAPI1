using Microsoft.EntityFrameworkCore;
using PersonelAPI1.Models; // Modelleriniz bu namespace’de ise
using PersonelAPI1.Data;

namespace PersonelAPI1.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Personel> Personeller { get; set; }
        public DbSet<BankInfo> BankaBilgileri { get; set; }
        public DbSet<AnnualLeave> YillikIzinler { get; set; }
        public DbSet<UsedLeaveDay> KullanilanIzinGunleri { get; set; }
        public DbSet<UnpaidLeave> UcretsizIzinler { get; set; }
        public DbSet<Report> Raporlar { get; set; }
        public DbSet<Salary> Maaslar { get; set; }
        public DbSet<ExtraPayment> EkOdemeler { get; set; }
        public DbSet<User> Kullanicilar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Personel>().ToTable("Personel");
            modelBuilder.Entity<BankInfo>().ToTable("BankaBilgisi");
            modelBuilder.Entity<AnnualLeave>().ToTable("YillikIzin");
            modelBuilder.Entity<UsedLeaveDay>().ToTable("KullanilanIzinGunleri");
            modelBuilder.Entity<UnpaidLeave>().ToTable("UcretsizIzin");
            modelBuilder.Entity<Report>().ToTable("Rapor");
            modelBuilder.Entity<Salary>().ToTable("Maas");
            modelBuilder.Entity<ExtraPayment>().ToTable("EkOdeme");
            modelBuilder.Entity<User>().ToTable("Kullanici");

            // İlişkiler
            modelBuilder.Entity<BankInfo>()
                .HasOne(b => b.Personel)
                .WithOne(p => p.BankInfo)
                .HasForeignKey<BankInfo>(b => b.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.YillikIzinler)
                .WithOne(a => a.Personel)
                .HasForeignKey(a => a.PersonelId);

            modelBuilder.Entity<AnnualLeave>()
                .HasMany(a => a.KullanilanIzinGunleri)
                .WithOne(u => u.YillikIzin)
                .HasForeignKey(u => u.YillikIzinId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.UcretsizIzinler)
                .WithOne(u => u.Personel)
                .HasForeignKey(u => u.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.Raporlar)
                .WithOne(r => r.Personel)
                .HasForeignKey(r => r.PersonelId);

            modelBuilder.Entity<Personel>()
                .HasMany(p => p.Maaslar)
                .WithOne(m => m.Personel)
                .HasForeignKey(m => m.PersonelId);

            modelBuilder.Entity<Salary>()
                .HasMany(s => s.EkOdemeler)
                .WithOne(e => e.Maas)
                .HasForeignKey(e => e.MaasId);

            modelBuilder.Entity<Personel>()
                .HasOne(p => p.User)
                .WithOne(u => u.Personel)
                .HasForeignKey<User>(u => u.PersonelId);
        }
    }
}
