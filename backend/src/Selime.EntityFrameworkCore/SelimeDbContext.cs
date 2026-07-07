using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore;
using Selime.Entities;

namespace Selime.EntityFrameworkCore
{
    public class SelimeDbContext : AbpDbContext
    {
        public DbSet<AircraftType> AircraftTypes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<AtaChapter> AtaChapters { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<PartCatalog> PartCatalogs { get; set; }
        public DbSet<SnagReport> SnagReports { get; set; }
        public DbSet<SnagReportPart> SnagReportParts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ApprovalRecord> ApprovalRecords { get; set; }
        public DbSet<StatusChangeLog> StatusChangeLogs { get; set; }


        public SelimeDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AircraftType 1:N Aircraft
            modelBuilder.Entity<Aircraft>()
                .HasOne(x => x.AircraftType)
                .WithMany(x => x.Aircrafts)
                .HasForeignKey(x => x.AircraftTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Station 1:N Aircraft
            modelBuilder.Entity<Aircraft>()
                .HasOne(x => x.Station)
                .WithMany(x => x.Aircrafts)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Aircraft 1:N SnagReport
            modelBuilder.Entity<SnagReport>()
                .HasOne(x => x.Aircraft)
                .WithMany(x => x.SnagReports)
                .HasForeignKey(x => x.AircraftId)
                .OnDelete(DeleteBehavior.Restrict);

            // AtaChapter 1:N SnagReport
            modelBuilder.Entity<SnagReport>()
                .HasOne(x => x.AtaChapter)
                .WithMany(x => x.SnagReports)
                .HasForeignKey(x => x.AtaChapterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Station 1:N SnagReport
            modelBuilder.Entity<SnagReport>()
                .HasOne(x => x.Station)
                .WithMany(x => x.SnagReports)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            // SnagReport 1:N SnagReportPart
            modelBuilder.Entity<SnagReportPart>()
                .HasOne(x => x.SnagReport)
                .WithMany(x => x.SnagReportParts)
                .HasForeignKey(x => x.SnagReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // PartCatalog 1:N SnagReportPart
            modelBuilder.Entity<SnagReportPart>()
                .HasOne(x => x.PartCatalog)
                .WithMany(x => x.SnagReportParts)
                .HasForeignKey(x => x.PartCatalogId)
                .OnDelete(DeleteBehavior.Restrict);


            // RBAC: AppUser N:N AppRole via UserRole junction
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();

            // RolePermission: AppRole 1:N RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionName })
                .IsUnique();

            // AppRole.Name unique
            modelBuilder.Entity<AppRole>()
                .HasIndex(r => r.Name)
                .IsUnique();

        }
    }
}
