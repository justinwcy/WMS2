using Application.DTO.Response;
using MudBlazor;
using WebUI.Components.Pages;
using WebUI.Components.Pages.Company.Company;
using WebUI.Components.Pages.Company.Staff;
using WebUI.Components.Pages.IncomingOrder.IncomingOrder;
using WebUI.Components.Pages.IncomingOrder.IncomingOrderProduct;
using WebUI.Components.Pages.IncomingOrder.Vendor;
using WebUI.Components.Pages.Inventory.Inventory;
using WebUI.Components.Pages.Inventory.Rack;
using WebUI.Components.Pages.Inventory.Warehouse;
using WebUI.Components.Pages.Inventory.Zone;
using WebUI.Components.Pages.Order.Bin;
using WebUI.Components.Pages.Order.Courier;
using WebUI.Components.Pages.Order.Customer;
using WebUI.Components.Pages.Order.CustomerOrder;
using WebUI.Components.Pages.Order.CustomerOrderDetail;
using WebUI.Components.Pages.Product.Product;
using WebUI.Components.Pages.Product.ProductGroup;
using WebUI.Components.Pages.RefundOrder.RefundOrder;
using WebUI.Components.Pages.RefundOrder.RefundOrderProduct;
using WebUI.Components.Pages.Shop.Shop;
using WebUI.Themes;

namespace WebUI.Utilities
{
    public static class ViewDialogUtilities
    {
        public static async Task<DialogResult?> OpenCompanyDialogAsync(
    Guid companyId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
    IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenCompanyDialogAsync([companyId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenCompanyDialogAsync(
            List<Guid> companyIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<CompanyDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.CompanyIds, companyIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<CompanyDialog>("Company", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenStaffDialogAsync(
            Guid staffId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenStaffDialogAsync([staffId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenStaffDialogAsync(
            List<Guid> staffIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<StaffDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.StaffIds, staffIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<StaffDialog>("Staff", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenIncomingOrderDialogAsync(
            Guid incomingOrderId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenIncomingOrderDialogAsync([incomingOrderId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenIncomingOrderDialogAsync(
            List<Guid> incomingOrderIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<IncomingOrderDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.IncomingOrderIds, incomingOrderIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<IncomingOrderDialog>("IncomingOrder", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenIncomingOrderProductDialogAsync(
            Guid incomingOrderProductId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenIncomingOrderProductDialogAsync([incomingOrderProductId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenIncomingOrderProductDialogAsync(
            List<Guid> incomingOrderProductIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<IncomingOrderProductDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.IncomingOrderProductIds, incomingOrderProductIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<IncomingOrderProductDialog>("IncomingOrderProduct", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenVendorDialogAsync(
            Guid vendorId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenVendorDialogAsync([vendorId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenVendorDialogAsync(
            List<Guid> vendorIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<VendorDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.VendorIds, vendorIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<VendorDialog>("Vendor", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenInventoryDialogAsync(
            Guid inventoryId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenInventoryDialogAsync([inventoryId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenInventoryDialogAsync(
            List<Guid> inventoryIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<InventoryDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.InventoryIds, inventoryIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<InventoryDialog>("Inventory", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenRackDialogAsync(
            Guid rackId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenRackDialogAsync([rackId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenRackDialogAsync(
            List<Guid> rackIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<RackDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.RackIds, rackIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<RackDialog>("Rack", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenWarehouseDialogAsync(
            Guid warehouseId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenWarehouseDialogAsync([warehouseId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenWarehouseDialogAsync(
            List<Guid> warehouseIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<WarehouseDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.WarehouseIds, warehouseIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<WarehouseDialog>("Warehouse", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenZoneDialogAsync(
            Guid zoneId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenZoneDialogAsync([zoneId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenZoneDialogAsync(
            List<Guid> zoneIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<ZoneDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.ZoneIds, zoneIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<ZoneDialog>("Zone", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenBinDialogAsync(
            Guid binId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenBinDialogAsync([binId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenBinDialogAsync(
            List<Guid> binIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<BinDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.BinIds, binIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<BinDialog>("Bin", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenCourierDialogAsync(
            Guid courierId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenCourierDialogAsync([courierId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenCourierDialogAsync(
            List<Guid> courierIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<CourierDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.CourierIds, courierIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<CourierDialog>("Courier", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenCustomerDialogAsync(
            Guid customerId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenCustomerDialogAsync([customerId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenCustomerDialogAsync(
            List<Guid> customerIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<CustomerDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.CustomerIds, customerIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<CustomerDialog>("Customer", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenCustomerOrderDialogAsync(
            Guid customerOrderId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenCustomerOrderDialogAsync([customerOrderId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenCustomerOrderDialogAsync(
            List<Guid> customerOrderIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<CustomerOrderDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.CustomerOrderIds, customerOrderIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<CustomerOrderDialog>("CustomerOrder", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenCustomerOrderDetailDialogAsync(
            Guid customerOrderDetailId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenCustomerOrderDetailDialogAsync([customerOrderDetailId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenCustomerOrderDetailDialogAsync(
            List<Guid> customerOrderDetailIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<CustomerOrderDetailDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.CustomerOrderDetailIds, customerOrderDetailIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<CustomerOrderDetailDialog>("CustomerOrderDetail", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenProductDialogAsync(
            Guid productId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenProductDialogAsync([productId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenProductDialogAsync(
            List<Guid> productIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<ProductDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.ProductIds, productIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<ProductDialog>("Product", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenProductGroupDialogAsync(
            Guid productGroupId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenProductGroupDialogAsync([productGroupId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenProductGroupDialogAsync(
            List<Guid> productGroupIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<ProductGroupDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.ProductGroupIds, productGroupIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<ProductGroupDialog>("ProductGroup", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenRefundOrderDialogAsync(
            Guid refundOrderId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenRefundOrderDialogAsync([refundOrderId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenRefundOrderDialogAsync(
            List<Guid> refundOrderIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<RefundOrderDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.RefundOrderIds, refundOrderIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<RefundOrderDialog>("RefundOrder", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenRefundOrderProductDialogAsync(
            Guid refundOrderProductId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenRefundOrderProductDialogAsync([refundOrderProductId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenRefundOrderProductDialogAsync(
            List<Guid> refundOrderProductIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<RefundOrderProductDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.RefundOrderProductIds, refundOrderProductIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<RefundOrderProductDialog>("RefundOrderProduct", parameters, options);
            return await dialog.Result;
        }

        public static async Task<DialogResult?> OpenShopDialogAsync(
            Guid shopId, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            return await OpenShopDialogAsync([shopId], currentStaff, dialogService, mainPage, pageComponents);
        }

        public static async Task<DialogResult?> OpenShopDialogAsync(
            List<Guid> shopIds, GetStaffResponseDTO currentStaff, IDialogService dialogService,
            IMainPage mainPage, List<IPageComponent> pageComponents)
        {
            var parameters = new DialogParameters<ShopDialog>()
            {
                { x => x.CurrentStaff, currentStaff },
                { x => x.ShopIds, shopIds },
                { x => x.MainPage, mainPage },
                { x => x.PageComponents, pageComponents },
            };

            var options = Theme.DefaultViewDialogOptions();
            var dialog = await dialogService.ShowAsync<ShopDialog>("Shop", parameters, options);
            return await dialog.Result;
        }

    }
}
