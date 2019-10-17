namespace MyCompanyName.AbpZeroTemplate.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        public const string Pages_OrderedProducts = "Pages.OrderedProducts";
        public const string Pages_OrderedProducts_Create = "Pages.OrderedProducts.Create";
        public const string Pages_OrderedProducts_Edit = "Pages.OrderedProducts.Edit";
        public const string Pages_OrderedProducts_Delete = "Pages.OrderedProducts.Delete";

        public const string Pages_QuotationItems = "Pages.QuotationItems";
        public const string Pages_QuotationItems_Create = "Pages.QuotationItems.Create";
        public const string Pages_QuotationItems_Edit = "Pages.QuotationItems.Edit";
        public const string Pages_QuotationItems_Delete = "Pages.QuotationItems.Delete";

        public const string Pages_QuotationUnitPrices = "Pages.QuotationUnitPrices";
        public const string Pages_QuotationUnitPrices_Create = "Pages.QuotationUnitPrices.Create";
        public const string Pages_QuotationUnitPrices_Edit = "Pages.QuotationUnitPrices.Edit";
        public const string Pages_QuotationUnitPrices_Delete = "Pages.QuotationUnitPrices.Delete";

        public const string Pages_ClientUnitPrices = "Pages.ClientUnitPrices";
        public const string Pages_ClientUnitPrices_Create = "Pages.ClientUnitPrices.Create";
        public const string Pages_ClientUnitPrices_Edit = "Pages.ClientUnitPrices.Edit";
        public const string Pages_ClientUnitPrices_Delete = "Pages.ClientUnitPrices.Delete";

        public const string Pages_ProductSubCategories = "Pages.ProductSubCategories";
        public const string Pages_ProductSubCategories_Create = "Pages.ProductSubCategories.Create";
        public const string Pages_ProductSubCategories_Edit = "Pages.ProductSubCategories.Edit";
        public const string Pages_ProductSubCategories_Delete = "Pages.ProductSubCategories.Delete";

        public const string Pages_ProductCategories = "Pages.ProductCategories";
        public const string Pages_ProductCategories_Create = "Pages.ProductCategories.Create";
        public const string Pages_ProductCategories_Edit = "Pages.ProductCategories.Edit";
        public const string Pages_ProductCategories_Delete = "Pages.ProductCategories.Delete";

        public const string Pages_ProdactCategories = "Pages.ProdactCategories";
        public const string Pages_ProdactCategories_Create = "Pages.ProdactCategories.Create";
        public const string Pages_ProdactCategories_Edit = "Pages.ProdactCategories.Edit";
        public const string Pages_ProdactCategories_Delete = "Pages.ProdactCategories.Delete";

        public const string Pages_UnitPrices = "Pages.UnitPrices";
        public const string Pages_UnitPrices_Create = "Pages.UnitPrices.Create";
        public const string Pages_UnitPrices_Edit = "Pages.UnitPrices.Edit";
        public const string Pages_UnitPrices_Delete = "Pages.UnitPrices.Delete";

        public const string Pages_Products = "Pages.Products";
        public const string Pages_Products_Create = "Pages.Products.Create";
        public const string Pages_Products_Edit = "Pages.Products.Edit";
        public const string Pages_Products_Delete = "Pages.Products.Delete";

        public const string Pages_Quotations = "Pages.Quotations";
        public const string Pages_Quotations_Create = "Pages.Quotations.Create";
        public const string Pages_Quotations_Edit = "Pages.Quotations.Edit";
        public const string Pages_Quotations_Delete = "Pages.Quotations.Delete";

        public const string Pages_Clients = "Pages.Clients";
        public const string Pages_Clients_Create = "Pages.Clients.Create";
        public const string Pages_Clients_Edit = "Pages.Clients.Edit";
        public const string Pages_Clients_Delete = "Pages.Clients.Delete";

        public const string Pages_ContactPersons = "Pages.ContactPersons";
        public const string Pages_ContactPersons_Create = "Pages.ContactPersons.Create";
        public const string Pages_ContactPersons_Edit = "Pages.ContactPersons.Edit";
        public const string Pages_ContactPersons_Delete = "Pages.ContactPersons.Delete";

        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";
        public const string Pages_Administration_Users_Unlock = "Pages.Administration.Users.Unlock";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

    }
}