using Abp.Authorization;
using Abp.Localization;

namespace Selime.Authorization
{
    public class SelimeAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));

            // AircraftType
            pages.CreateChildPermission(PermissionNames.AircraftType_Read, L("AircraftType.Read"));
            pages.CreateChildPermission(PermissionNames.AircraftType_Create, L("AircraftType.Create"));
            pages.CreateChildPermission(PermissionNames.AircraftType_Update, L("AircraftType.Update"));
            pages.CreateChildPermission(PermissionNames.AircraftType_Delete, L("AircraftType.Delete"));

            // Station
            pages.CreateChildPermission(PermissionNames.Station_Read, L("Station.Read"));
            pages.CreateChildPermission(PermissionNames.Station_Create, L("Station.Create"));
            pages.CreateChildPermission(PermissionNames.Station_Update, L("Station.Update"));
            pages.CreateChildPermission(PermissionNames.Station_Delete, L("Station.Delete"));

            // AtaChapter
            pages.CreateChildPermission(PermissionNames.AtaChapter_Read, L("AtaChapter.Read"));
            pages.CreateChildPermission(PermissionNames.AtaChapter_Create, L("AtaChapter.Create"));
            pages.CreateChildPermission(PermissionNames.AtaChapter_Update, L("AtaChapter.Update"));
            pages.CreateChildPermission(PermissionNames.AtaChapter_Delete, L("AtaChapter.Delete"));

            // Aircraft
            pages.CreateChildPermission(PermissionNames.Aircraft_Read, L("Aircraft.Read"));
            pages.CreateChildPermission(PermissionNames.Aircraft_Create, L("Aircraft.Create"));
            pages.CreateChildPermission(PermissionNames.Aircraft_Update, L("Aircraft.Update"));
            pages.CreateChildPermission(PermissionNames.Aircraft_Delete, L("Aircraft.Delete"));

            // PartCatalog
            pages.CreateChildPermission(PermissionNames.PartCatalog_Read, L("PartCatalog.Read"));
            pages.CreateChildPermission(PermissionNames.PartCatalog_Create, L("PartCatalog.Create"));
            pages.CreateChildPermission(PermissionNames.PartCatalog_Update, L("PartCatalog.Update"));
            pages.CreateChildPermission(PermissionNames.PartCatalog_Delete, L("PartCatalog.Delete"));

            // SnagReport
            pages.CreateChildPermission(PermissionNames.SnagReport_Read, L("SnagReport.Read"));
            pages.CreateChildPermission(PermissionNames.SnagReport_Create, L("SnagReport.Create"));
            pages.CreateChildPermission(PermissionNames.SnagReport_Update, L("SnagReport.Update"));
            pages.CreateChildPermission(PermissionNames.SnagReport_Delete, L("SnagReport.Delete"));
            pages.CreateChildPermission(PermissionNames.SnagReport_ChangeStatus, L("SnagReport.ChangeStatus"));

            // SnagReportPart
            pages.CreateChildPermission(PermissionNames.SnagReportPart_Read, L("SnagReportPart.Read"));
            pages.CreateChildPermission(PermissionNames.SnagReportPart_Create, L("SnagReportPart.Create"));
            pages.CreateChildPermission(PermissionNames.SnagReportPart_Update, L("SnagReportPart.Update"));
            pages.CreateChildPermission(PermissionNames.SnagReportPart_Delete, L("SnagReportPart.Delete"));

            // RBAC
            pages.CreateChildPermission(PermissionNames.AppUser_Read, L("AppUser.Read"));
            pages.CreateChildPermission(PermissionNames.AppRole_Read, L("AppRole.Read"));
            pages.CreateChildPermission(PermissionNames.AppUser_Create, L("AppUser.Create"));
            pages.CreateChildPermission(PermissionNames.AppRole_Create, L("AppRole.Create"));
            pages.CreateChildPermission(PermissionNames.AppUser_Update, L("AppUser.Update"));
            pages.CreateChildPermission(PermissionNames.AppRole_Update, L("AppRole.Update"));
            pages.CreateChildPermission(PermissionNames.AppUser_Delete, L("AppUser.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_Delete, L("AppRole.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_AssignPermissions, L("AppRole.AssignPermissions"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SelimeConsts.LocalizationSourceName);
        }
    }
}
