using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyCompanyName.AbpZeroTemplate.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var quotationItems = pages.CreateChildPermission(AppPermissions.Pages_QuotationItems, L("QuotationItems"));
            quotationItems.CreateChildPermission(AppPermissions.Pages_QuotationItems_Create, L("CreateNewQuotationItem"));
            quotationItems.CreateChildPermission(AppPermissions.Pages_QuotationItems_Edit, L("EditQuotationItem"));
            quotationItems.CreateChildPermission(AppPermissions.Pages_QuotationItems_Delete, L("DeleteQuotationItem"));



            var quotationUnitPrices = pages.CreateChildPermission(AppPermissions.Pages_QuotationUnitPrices, L("QuotationUnitPrices"));
            quotationUnitPrices.CreateChildPermission(AppPermissions.Pages_QuotationUnitPrices_Create, L("CreateNewQuotationUnitPrice"));
            quotationUnitPrices.CreateChildPermission(AppPermissions.Pages_QuotationUnitPrices_Edit, L("EditQuotationUnitPrice"));
            quotationUnitPrices.CreateChildPermission(AppPermissions.Pages_QuotationUnitPrices_Delete, L("DeleteQuotationUnitPrice"));



            var clientUnitPrices = pages.CreateChildPermission(AppPermissions.Pages_ClientUnitPrices, L("ClientUnitPrices"));
            clientUnitPrices.CreateChildPermission(AppPermissions.Pages_ClientUnitPrices_Create, L("CreateNewClientUnitPrice"));
            clientUnitPrices.CreateChildPermission(AppPermissions.Pages_ClientUnitPrices_Edit, L("EditClientUnitPrice"));
            clientUnitPrices.CreateChildPermission(AppPermissions.Pages_ClientUnitPrices_Delete, L("DeleteClientUnitPrice"));



            var productSubCategories = pages.CreateChildPermission(AppPermissions.Pages_ProductSubCategories, L("ProductSubCategories"));
            productSubCategories.CreateChildPermission(AppPermissions.Pages_ProductSubCategories_Create, L("CreateNewProductSubCategory"));
            productSubCategories.CreateChildPermission(AppPermissions.Pages_ProductSubCategories_Edit, L("EditProductSubCategory"));
            productSubCategories.CreateChildPermission(AppPermissions.Pages_ProductSubCategories_Delete, L("DeleteProductSubCategory"));



            var productCategories = pages.CreateChildPermission(AppPermissions.Pages_ProductCategories, L("ProductCategories"));
            productCategories.CreateChildPermission(AppPermissions.Pages_ProductCategories_Create, L("CreateNewProductCategory"));
            productCategories.CreateChildPermission(AppPermissions.Pages_ProductCategories_Edit, L("EditProductCategory"));
            productCategories.CreateChildPermission(AppPermissions.Pages_ProductCategories_Delete, L("DeleteProductCategory"));



            var prodactCategories = pages.CreateChildPermission(AppPermissions.Pages_ProdactCategories, L("ProdactCategories"));
            prodactCategories.CreateChildPermission(AppPermissions.Pages_ProdactCategories_Create, L("CreateNewProdactCategory"));
            prodactCategories.CreateChildPermission(AppPermissions.Pages_ProdactCategories_Edit, L("EditProdactCategory"));
            prodactCategories.CreateChildPermission(AppPermissions.Pages_ProdactCategories_Delete, L("DeleteProdactCategory"));



            var unitPrices = pages.CreateChildPermission(AppPermissions.Pages_UnitPrices, L("UnitPrices"));
            unitPrices.CreateChildPermission(AppPermissions.Pages_UnitPrices_Create, L("CreateNewUnitPrice"));
            unitPrices.CreateChildPermission(AppPermissions.Pages_UnitPrices_Edit, L("EditUnitPrice"));
            unitPrices.CreateChildPermission(AppPermissions.Pages_UnitPrices_Delete, L("DeleteUnitPrice"));



            var products = pages.CreateChildPermission(AppPermissions.Pages_Products, L("Products"));
            products.CreateChildPermission(AppPermissions.Pages_Products_Create, L("CreateNewProduct"));
            products.CreateChildPermission(AppPermissions.Pages_Products_Edit, L("EditProduct"));
            products.CreateChildPermission(AppPermissions.Pages_Products_Delete, L("DeleteProduct"));



            var quotations = pages.CreateChildPermission(AppPermissions.Pages_Quotations, L("Quotations"));
            quotations.CreateChildPermission(AppPermissions.Pages_Quotations_Create, L("CreateNewQuotation"));
            quotations.CreateChildPermission(AppPermissions.Pages_Quotations_Edit, L("EditQuotation"));
            quotations.CreateChildPermission(AppPermissions.Pages_Quotations_Delete, L("DeleteQuotation"));



            var clients = pages.CreateChildPermission(AppPermissions.Pages_Clients, L("Clients"));
            clients.CreateChildPermission(AppPermissions.Pages_Clients_Create, L("CreateNewClient"));
            clients.CreateChildPermission(AppPermissions.Pages_Clients_Edit, L("EditClient"));
            clients.CreateChildPermission(AppPermissions.Pages_Clients_Delete, L("DeleteClient"));



            var contactPersons = pages.CreateChildPermission(AppPermissions.Pages_ContactPersons, L("ContactPersons"));
            contactPersons.CreateChildPermission(AppPermissions.Pages_ContactPersons_Create, L("CreateNewContactPerson"));
            contactPersons.CreateChildPermission(AppPermissions.Pages_ContactPersons_Edit, L("EditContactPerson"));
            contactPersons.CreateChildPermission(AppPermissions.Pages_ContactPersons_Delete, L("DeleteContactPerson"));


            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host); 

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
