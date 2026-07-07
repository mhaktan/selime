using System.Collections.Generic;
using Abp.Dependency;

namespace Selime.Authorization
{
    /// <summary>Single permission descriptor — name, group (entity), description.</summary>
    public class PermissionInfo
    {
        public string Name { get; }
        public string Group { get; }
        public string Description { get; }
        public bool IsRbac { get; }

        public PermissionInfo(string name, string group, string description, bool isRbac)
        {
            Name = name; Group = group; Description = description; IsRbac = isRbac;
        }
    }

    public interface IPermissionRegistry
    {
        IReadOnlyList<PermissionInfo> All { get; }
    }

    public class PermissionRegistry : IPermissionRegistry, ISingletonDependency
    {
        public IReadOnlyList<PermissionInfo> All { get; } = new List<PermissionInfo>
        {
            new PermissionInfo("AircraftType.Read", "AircraftType", "Read AircraftType", false),
            new PermissionInfo("AircraftType.Create", "AircraftType", "Create AircraftType", false),
            new PermissionInfo("AircraftType.Update", "AircraftType", "Update AircraftType", false),
            new PermissionInfo("AircraftType.Delete", "AircraftType", "Delete AircraftType", false),
            new PermissionInfo("Station.Read", "Station", "Read Station", false),
            new PermissionInfo("Station.Create", "Station", "Create Station", false),
            new PermissionInfo("Station.Update", "Station", "Update Station", false),
            new PermissionInfo("Station.Delete", "Station", "Delete Station", false),
            new PermissionInfo("AtaChapter.Read", "AtaChapter", "Read AtaChapter", false),
            new PermissionInfo("AtaChapter.Create", "AtaChapter", "Create AtaChapter", false),
            new PermissionInfo("AtaChapter.Update", "AtaChapter", "Update AtaChapter", false),
            new PermissionInfo("AtaChapter.Delete", "AtaChapter", "Delete AtaChapter", false),
            new PermissionInfo("Aircraft.Read", "Aircraft", "Read Aircraft", false),
            new PermissionInfo("Aircraft.Create", "Aircraft", "Create Aircraft", false),
            new PermissionInfo("Aircraft.Update", "Aircraft", "Update Aircraft", false),
            new PermissionInfo("Aircraft.Delete", "Aircraft", "Delete Aircraft", false),
            new PermissionInfo("PartCatalog.Read", "PartCatalog", "Read PartCatalog", false),
            new PermissionInfo("PartCatalog.Create", "PartCatalog", "Create PartCatalog", false),
            new PermissionInfo("PartCatalog.Update", "PartCatalog", "Update PartCatalog", false),
            new PermissionInfo("PartCatalog.Delete", "PartCatalog", "Delete PartCatalog", false),
            new PermissionInfo("SnagReport.Read", "SnagReport", "Read SnagReport", false),
            new PermissionInfo("SnagReport.Create", "SnagReport", "Create SnagReport", false),
            new PermissionInfo("SnagReport.Update", "SnagReport", "Update SnagReport", false),
            new PermissionInfo("SnagReport.Delete", "SnagReport", "Delete SnagReport", false),
            new PermissionInfo("SnagReport.ChangeStatus", "SnagReport", "Change SnagReport status", false),
            new PermissionInfo("SnagReportPart.Read", "SnagReportPart", "Read SnagReportPart", false),
            new PermissionInfo("SnagReportPart.Create", "SnagReportPart", "Create SnagReportPart", false),
            new PermissionInfo("SnagReportPart.Update", "SnagReportPart", "Update SnagReportPart", false),
            new PermissionInfo("SnagReportPart.Delete", "SnagReportPart", "Delete SnagReportPart", false),
            new PermissionInfo("AppUser.Read", "AppUser", "Read users", true),
            new PermissionInfo("AppRole.Read", "AppRole", "Read roles", true),
            new PermissionInfo("AppUser.Create", "AppUser", "Create users", true),
            new PermissionInfo("AppRole.Create", "AppRole", "Create roles", true),
            new PermissionInfo("AppUser.Update", "AppUser", "Update users", true),
            new PermissionInfo("AppRole.Update", "AppRole", "Update roles", true),
            new PermissionInfo("AppUser.Delete", "AppUser", "Delete users", true),
            new PermissionInfo("AppRole.Delete", "AppRole", "Delete roles", true),
            new PermissionInfo("AppRole.AssignPermissions", "AppRole", "Assign permissions to roles", true),
        };
    }
}
