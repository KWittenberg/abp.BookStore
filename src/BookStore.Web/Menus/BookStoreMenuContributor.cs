using System.Threading.Tasks;
using BookStore.Localization;
using BookStore.MultiTenancy;
using BookStore.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace BookStore.Web.Menus;

public class BookStoreMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BookStoreResource>();

        context.Menu.Items.Insert(0, new ApplicationMenuItem(BookStoreMenus.Home, l["Menu:Home"], "~/", icon: "fas fa-home", order: 0));

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(new ApplicationMenuItem("BooksStore", l["Menu:BookStore"], icon: "fa fa-book")
            .AddItem(new ApplicationMenuItem("BooksStore.Books", l["Menu:Books"], url: "/Books")
            .RequirePermissions(BookStorePermissions.Books.Default))
            .AddItem(new ApplicationMenuItem("BooksStore.Authors", l["Menu:Authors"], url: "/Authors")
            .RequirePermissions(BookStorePermissions.Books.Default)));
    }

    #region OLD MENU
    //private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    //{
    //    var administration = context.Menu.GetAdministration();
    //    var l = context.GetLocalizer<BookStoreResource>();

    //    context.Menu.Items.Insert(0, new ApplicationMenuItem(BookStoreMenus.Home, l["Menu:Home"], "~/", icon: "fas fa-home", order: 0));

    //    // Add BookStore and Books
    //    context.Menu.AddItem(new ApplicationMenuItem("BooksStore", l["Menu:BookStore"], icon: "fa fa-book")
    //        .AddItem(new ApplicationMenuItem("BooksStore.Books", l["Menu:Books"], url: "/Books"))
    //        .AddItem(new ApplicationMenuItem("BooksStore.Authors", l["Menu:Authors"], url: "/Authors")));


    //    //context.Menu.AddItem(new ApplicationMenuItem("BooksStore", l["Menu:BookStore"], icon: "fa fa-book")
    //    //    .AddItem(new ApplicationMenuItem("BooksStore.Books", l["Menu:Books"], url: "/Books")
    //    //    .RequirePermissions(BookStorePermissions.Books.Default)));

    //    if (MultiTenancyConsts.IsEnabled)
    //    {
    //        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
    //    }
    //    else
    //    {
    //        administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
    //    }

    //    administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
    //    administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
    //}
    #endregion
}
