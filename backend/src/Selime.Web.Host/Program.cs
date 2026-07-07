using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Selime.EntityFrameworkCore;
using Selime.EntityFrameworkCore.Seed;
using Selime.Entities;

namespace Selime.Web.Host
{
    /// <summary>
    /// Background service that runs migration + seed once at startup
    /// without blocking the HTTP pipeline.
    /// </summary>
    public class MigrationHostedService : IHostedService
    {
        private readonly IConfiguration _config;

        public MigrationHostedService(IConfiguration config)
        {
            _config = config;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var connStr = _config.GetConnectionString("Default") ?? "";
            if (string.IsNullOrEmpty(connStr)) return;

            // Run in background to avoid blocking host startup (prevents EF tooling timeout)
            _ = Task.Run(async () =>
            {
                // Small delay to ensure host is fully started before DB operations
                await Task.Delay(1000, cancellationToken);
                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder.UseNpgsql(connStr);

                    using (var db = new SelimeDbContext(optionsBuilder.Options))
                    {
                        db.Database.EnsureCreated();
                        Console.WriteLine("[Migration] Database is up to date.");

                        // Seed sample data — wrapped in its own try so a failure here doesn't block RBAC seed below.
                        try
                        {
                    if (!db.AircraftTypes.Any())
                    {
                        db.AircraftTypes.AddRange(
                    new AircraftType { Id = 1, TypeCode = "ABC-001", Manufacturer = "Sample Item 1", SeatCapacity = 42 },
                    new AircraftType { Id = 2, TypeCode = "XYZ-002", Manufacturer = "Sample Item 2", SeatCapacity = 17 }
                        );
                    }
                    if (!db.Stations.Any())
                    {
                        db.Stations.AddRange(
                    new Station { Id = 3, Code = "ABC-001", Name = "Alice Johnson", City = "New York" },
                    new Station { Id = 4, Code = "XYZ-002", Name = "Bob Smith", City = "London" }
                        );
                    }
                    if (!db.AtaChapters.Any())
                    {
                        db.AtaChapters.AddRange(
                    new AtaChapter { Id = 5, AtaNumber = "ABC-001", Name = "Alice Johnson" },
                    new AtaChapter { Id = 6, AtaNumber = "XYZ-002", Name = "Bob Smith" }
                        );
                    }
                    if (!db.Aircrafts.Any())
                    {
                        db.Aircrafts.AddRange(
                    new Aircraft { Id = 7, Registration = "Sample Item 1", ManufacturingYear = 42, Status = (Status)0, AircraftTypeId = 1, StationId = 3 },
                    new Aircraft { Id = 8, Registration = "Sample Item 2", ManufacturingYear = 17, Status = (Status)1, AircraftTypeId = 2, StationId = 4 }
                        );
                    }
                    if (!db.PartCatalogs.Any())
                    {
                        db.PartCatalogs.AddRange(
                    new PartCatalog { Id = 9, PartNumber = "ABC-001", Description = "Lorem ipsum dolor sit amet", UnitOfMeasure = "Sample Item 1", StockQuantity = 42 },
                    new PartCatalog { Id = 10, PartNumber = "XYZ-002", Description = "Consectetur adipiscing elit", UnitOfMeasure = "Sample Item 2", StockQuantity = 17 }
                        );
                    }
                    if (!db.SnagReports.Any())
                    {
                        db.SnagReports.AddRange(
                    new SnagReport { Id = 11, ReportNumber = "ABC-001", Title = "Introduction to Physics", Description = "Lorem ipsum dolor sit amet", Severity = (Severity)0, ReportedBy = "Sample Item 1", DetectionDate = new DateTime(2024, 3, 15), ActionTaken = "Sample Item 1", CrsNumber = "ABC-001", RevisionNote = "Lorem ipsum dolor sit amet", Status = (Status)0, LineMechanicId = 1000L, CertifyingStaffId = 1000L, AircraftId = 7, AtaChapterId = 5, StationId = 3 },
                    new SnagReport { Id = 12, ReportNumber = "XYZ-002", Title = "Advanced Mathematics", Description = "Consectetur adipiscing elit", Severity = (Severity)1, ReportedBy = "Sample Item 2", DetectionDate = new DateTime(2024, 6, 20), ActionTaken = "Sample Item 2", CrsNumber = "XYZ-002", RevisionNote = "Consectetur adipiscing elit", Status = (Status)1, LineMechanicId = 2000L, CertifyingStaffId = 2000L, AircraftId = 8, AtaChapterId = 6, StationId = 4 }
                        );
                    }
                    if (!db.SnagReportParts.Any())
                    {
                        db.SnagReportParts.AddRange(
                    new SnagReportPart { Id = 13, SerialNumber = "ABC-001", QuantityUsed = 42, SnagReportId = 11, PartCatalogId = 9 },
                    new SnagReportPart { Id = 14, SerialNumber = "XYZ-002", QuantityUsed = 17, SnagReportId = 12, PartCatalogId = 10 }
                        );
                    }
                            db.SaveChanges();
                            Console.WriteLine("[Seed] Sample data created.");
                        }
                        catch (Exception sampleEx)
                        {
                            Console.WriteLine($"[Seed] Sample data skipped: {sampleEx.GetType().Name}: {sampleEx.Message}");
                            // Carry on — RBAC seed must still run so admin/123qwe is usable.
                        }
                        // Sync identity sequences to MAX(Id). Seeded rows carry explicit Ids which do NOT
                        // advance Postgres identity sequences → nextval collides with a seed row and the
                        // first few inserts fail with a duplicate-key 500. Runs every startup; idempotent.
                        try
                        {
                            db.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"AircraftTypes\"', 'Id'), (SELECT COALESCE(MAX(\"Id\"), 0) FROM \"AircraftTypes\") + 1, false);");
                            db.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"Stations\"', 'Id'), (SELECT COALESCE(MAX(\"Id\"), 0) FROM \"Stations\") + 1, false);");
                            db.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"AtaChapters\"', 'Id'), (SELECT COALESCE(MAX(\"Id\"), 0) FROM \"AtaChapters\") + 1, false);");
                            db.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"Aircrafts\"', 'Id'), (SELECT COALESCE(MAX(\"Id\"), 0) FROM \"Aircrafts\") + 1, false);");
                            db.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"PartCatalogs\"', 'Id'), (SELECT COALESCE(MAX(\"Id\"), 0) FROM \"PartCatalogs\") + 1, false);");
                            db.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"SnagReports\"', 'Id'), (SELECT COALESCE(MAX(\"Id\"), 0) FROM \"SnagReports\") + 1, false);");
                            db.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"SnagReportParts\"', 'Id'), (SELECT COALESCE(MAX(\"Id\"), 0) FROM \"SnagReportParts\") + 1, false);");
                            Console.WriteLine("[Seed] Identity sequences synced.");
                        }
                        catch (Exception seqEx)
                        {
                            Console.WriteLine($"[Seed] Sequence sync skipped: {seqEx.GetType().Name}: {seqEx.Message}");
                        }
                    }
                    // RBAC seed (Admin/User roles + permissions + admin user) runs through ABP DI
                    // so PermissionRegistry can be injected. SeedHelper is idempotent.
                    SeedHelper.SeedHostDb(Abp.Dependency.IocManager.Instance);
                    Console.WriteLine("[Seed] RBAC seed complete (Admin role + admin user).");
                }
                catch (Exception ex)
                {
                    // Full diagnostic — surface the real cause so silent seed failures are debuggable.
                    Console.WriteLine($"[Migration] FAILED: {ex.GetType().Name}: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"[Migration] InnerException: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                    Console.WriteLine("[Migration] StackTrace:");
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("[Migration] App continues without migration — admin user will not exist.");
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

    public class Program
    {
        // Runtime entry: WebHost is required because ABP Startup returns IServiceProvider.
        public static void Main(string[] args)
        {
            // Npgsql 7+ requires UTC DateTimes — enable legacy behavior for ABP compatibility
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

        // Design-time entry for EF Core tools (dotnet ef migrations).
        // Without this, EF tools wait 5 minutes for IHost build (resolver default timeout)
        // and then SIGTERM any running dotnet process — killing live dev servers.
        // We expose a minimal IHost that EF tools resolve in milliseconds; the actual
        // DbContext is built by IDesignTimeDbContextFactory in the EntityFrameworkCore project.
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);
    }
}
