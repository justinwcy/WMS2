using Application.Service.Queries;
using MediatR;
using WebUI.Components.Models;

namespace WebUI.Utilities
{
    public class DatabaseHelper(IMediator mediator)
    {
        public async Task<List<StaffModel>> GetAllStaffs(Guid companyId)
        {
            var getAllStaffIdsByCompanyIdQuery = new GetAllStaffIdsByCompanyIdQuery(companyId);
            var staffIds = await mediator.Send(getAllStaffIdsByCompanyIdQuery);
            return await GetStaffs(staffIds);
        }

        public async Task<List<StaffModel>> GetStaffs(List<Guid> staffIds)
        {
            var getStaffsByIdsQuery = new GetStaffsByIdsQuery(staffIds);
            var getStaffResponseDTOs = await mediator.Send(getStaffsByIdsQuery);
            var staffModels = getStaffResponseDTOs.Select(getStaffResponseDTO =>
                new StaffModel()
                {
                    Id = getStaffResponseDTO.Id,
                    ZoneIds = getStaffResponseDTO.ZoneIds,
                    CompanyId = getStaffResponseDTO.CompanyId,
                    CustomClaims = getStaffResponseDTO.Claims,
                    Email = getStaffResponseDTO.Email,
                    FirstName = getStaffResponseDTO.FirstName,
                    LastName = getStaffResponseDTO.LastName,
                    Roles = getStaffResponseDTO.Roles,
                    StaffNotificationIds = getStaffResponseDTO.StaffNotificationIds,
                }).ToList();

            return staffModels;
        }

        public async Task<List<IncomingOrderModel>> GetAllIncomingOrders(Guid companyId)
        {
            var getAllIncomingOrderIdsByCompanyIdQuery = new GetAllIncomingOrderIdsByCompanyIdQuery(companyId);
            var incomingOrderIds = await mediator.Send(getAllIncomingOrderIdsByCompanyIdQuery);
            return await GetIncomingOrders(incomingOrderIds);
        }

        public async Task<List<IncomingOrderModel>> GetIncomingOrders(List<Guid> incomingOrderIds)
        {
            var getIncomingOrdersByIdsQuery = new GetIncomingOrdersByIdsQuery(incomingOrderIds);
            var getIncomingOrderResponseDTOs = await mediator.Send(getIncomingOrdersByIdsQuery);
            var incomingOrderModels = getIncomingOrderResponseDTOs.Select(getIncomingOrderResponseDTO =>
                new IncomingOrderModel()
                {
                    Id = getIncomingOrderResponseDTO.Id,
                    Status = getIncomingOrderResponseDTO.Status,
                    IncomingOrderProductIds = getIncomingOrderResponseDTO.IncomingOrderProductIds,
                    IncomingDate = getIncomingOrderResponseDTO.IncomingDate,
                    PONumber = getIncomingOrderResponseDTO.PONumber,
                    ReceivingDate = getIncomingOrderResponseDTO.ReceivingDate,
                    VendorId = getIncomingOrderResponseDTO.VendorId,
                }).ToList();

            return incomingOrderModels;
        }

        public async Task<List<IncomingOrderProductModel>> GetAllIncomingOrderProducts(Guid companyId)
        {
            var getAllIncomingOrderProductIdsByCompanyIdQuery = new GetAllIncomingOrderProductIdsByCompanyIdQuery(companyId);
            var incomingOrderProductIds = await mediator.Send(getAllIncomingOrderProductIdsByCompanyIdQuery);
            return await GetIncomingOrderProducts(incomingOrderProductIds);
        }

        public async Task<List<IncomingOrderProductModel>> GetIncomingOrderProducts(List<Guid> incomingOrderProductIds)
        {
            var getIncomingOrderProductsByIdsQuery = new GetIncomingOrderProductsByIdsQuery(incomingOrderProductIds);
            var getIncomingOrderProductResponseDTOs = await mediator.Send(getIncomingOrderProductsByIdsQuery);
            var incomingOrderProductModels = 
                getIncomingOrderProductResponseDTOs.Select(getIncomingOrderProductResponseDTO => 
                    new IncomingOrderProductModel()
                    {
                        Id = getIncomingOrderProductResponseDTO.Id,
                        Status = getIncomingOrderProductResponseDTO.Status,
                        IncomingOrderId = getIncomingOrderProductResponseDTO.IncomingOrderId,
                        ProductId = getIncomingOrderProductResponseDTO.ProductId,
                        Quantity = getIncomingOrderProductResponseDTO.Quantity,
                    }).ToList();

            return incomingOrderProductModels;
        }

        public async Task<List<VendorModel>> GetAllVendors(Guid companyId)
        {
            var getAllVendorIdsByCompanyIdQuery = new GetAllVendorIdsByCompanyIdQuery(companyId);
            var vendorIds = await mediator.Send(getAllVendorIdsByCompanyIdQuery);
            return await GetVendors(vendorIds);
        }

        public async Task<List<VendorModel>> GetVendors(List<Guid> vendorIds)
        {
            var getVendorsByIdsQuery = new GetVendorsByIdsQuery(vendorIds);
            var getVendorResponseDTOs = await mediator.Send(getVendorsByIdsQuery);
            var vendorModels = getVendorResponseDTOs.Select(getVendorResponseDTO =>
                new VendorModel()
                {
                    Id = getVendorResponseDTO.Id,
                    Address = getVendorResponseDTO.Address,
                    Email = getVendorResponseDTO.Email,
                    FirstName = getVendorResponseDTO.FirstName,
                    LastName = getVendorResponseDTO.LastName,
                    IncomingOrderIds = getVendorResponseDTO.IncomingOrderIds,
                    PhoneNumber = getVendorResponseDTO.PhoneNumber,
                }).ToList();

            return vendorModels;
        }

        public async Task<List<InventoryModel>> GetAllInventories(Guid companyId)
        {
            var getAllInventoryIdsByCompanyIdQuery = new GetAllInventoryIdsByCompanyIdQuery(companyId);
            var inventoryIds = await mediator.Send(getAllInventoryIdsByCompanyIdQuery);
            return await GetInventories(inventoryIds);
        }

        public async Task<List<InventoryModel>> GetInventories(List<Guid> inventoryIds)
        {
            var getInventoriesByIdsQuery = new GetInventoriesByIdsQuery(inventoryIds);
            var getInventoryResponseDTOs = await mediator.Send(getInventoriesByIdsQuery);
            var inventoryModels = getInventoryResponseDTOs.Select(getInventoryResponseDTO =>
                new InventoryModel()
                {
                    Id = getInventoryResponseDTO.Id,
                    ProductId = getInventoryResponseDTO.ProductId,
                    Quantity = getInventoryResponseDTO.Quantity,
                    RackId = getInventoryResponseDTO.RackId
                }).ToList();

            return inventoryModels;
        }

        public async Task<List<RackModel>> GetAllRacks(Guid companyId)
        {
            var getAllRackIdsByCompanyIdQuery = new GetAllRackIdsByCompanyIdQuery(companyId);
            var rackIds = await mediator.Send(getAllRackIdsByCompanyIdQuery);
            return await GetRacks(rackIds);
        }

        public async Task<List<RackModel>> GetRacks(List<Guid> rackIds)
        {
            var getRacksByIdsQuery = new GetRacksByIdsQuery(rackIds);
            var getRackResponseDTOs = await mediator.Send(getRacksByIdsQuery);
            var rackModels = getRackResponseDTOs.Select(getRackResponseDTO =>
                new RackModel()
                {
                    Id = getRackResponseDTO.Id,
                    Name = getRackResponseDTO.Name,
                    ProductIds = getRackResponseDTO.ProductIds,
                    Height = getRackResponseDTO.Height,
                    Width = getRackResponseDTO.Width,
                    Depth = getRackResponseDTO.Depth,
                    MaxWeight = getRackResponseDTO.MaxWeight,
                    ZoneId = getRackResponseDTO.ZoneId,
                }).ToList();

            return rackModels;
        }

        public async Task<List<WarehouseModel>> GetAllWarehouses(Guid companyId)
        {
            var getAllWarehouseIdsByCompanyIdQuery = new GetAllWarehouseIdsByCompanyIdQuery(companyId);
            var warehouseIds = await mediator.Send(getAllWarehouseIdsByCompanyIdQuery);
            return await GetWarehouses(warehouseIds);
        }

        public async Task<List<WarehouseModel>> GetWarehouses(List<Guid> warehouseIds)
        {
            var getWarehousesByIdsQuery = new GetWarehousesByIdsQuery(warehouseIds);
            var getWarehouseResponseDTOs = await mediator.Send(getWarehousesByIdsQuery);
            var warehouseModels = getWarehouseResponseDTOs.Select(getWarehouseResponseDTO =>
                new WarehouseModel()
                {
                    Id = getWarehouseResponseDTO.Id,
                    Name = getWarehouseResponseDTO.Name,
                    Address = getWarehouseResponseDTO.Address,
                    ZoneIds = getWarehouseResponseDTO.ZoneIds,
                    CompanyId = getWarehouseResponseDTO.CompanyId,
                }).ToList();

            return warehouseModels;
        }

        public async Task<List<ZoneModel>> GetAllZones(Guid companyId)
        {
            var getAllZoneIdsByCompanyIdQuery = new GetAllZoneIdsByCompanyIdQuery(companyId);
            var zoneIds = await mediator.Send(getAllZoneIdsByCompanyIdQuery);
            return await GetZones(zoneIds);
        }

        public async Task<List<ZoneModel>> GetZones(List<Guid> zoneIds)
        {
            var getZonesByIdsQuery = new GetZonesByIdsQuery(zoneIds);
            var getZoneResponseDTOs = await mediator.Send(getZonesByIdsQuery);
            var zoneModels = getZoneResponseDTOs.Select(getZoneResponseDTO =>
                new ZoneModel()
                {
                    Id = getZoneResponseDTO.Id,
                    Name = getZoneResponseDTO.Name,
                    StaffIds = getZoneResponseDTO.StaffIds,
                    RackIds = getZoneResponseDTO.RackIds,
                    WarehouseId = getZoneResponseDTO.WarehouseId,
                }).ToList();

            return zoneModels;
        }

        public async Task<List<BinModel>> GetAllBins(Guid companyId)
        {
            var getAllBinIdsByCompanyIdQuery = new GetAllBinIdsByCompanyIdQuery(companyId);
            var binIds = await mediator.Send(getAllBinIdsByCompanyIdQuery);
            return await GetBins(binIds);
        }

        public async Task<List<BinModel>> GetBins(List<Guid> binIds)
        {
            var getBinsByIdsQuery = new GetBinsByIdsQuery(binIds);
            var getBinResponseDTOs = await mediator.Send(getBinsByIdsQuery);
            var binModels = getBinResponseDTOs.Select(getBinResponseDTO =>
                new BinModel()
                {
                    Id = getBinResponseDTO.Id,
                    Name = getBinResponseDTO.Name,
                    CustomerOrderIds = getBinResponseDTO.CustomerOrderIds,
                }).ToList();

            return binModels;
        }

        public async Task<List<CourierModel>> GetAllCouriers(Guid companyId)
        {
            var getAllCourierIdsByCompanyIdQuery = new GetAllCourierIdsByCompanyIdQuery(companyId);
            var courierIds = await mediator.Send(getAllCourierIdsByCompanyIdQuery);
            return await GetCouriers(courierIds);
        }

        public async Task<List<CourierModel>> GetCouriers(List<Guid> courierIds)
        {
            var getCouriersByIdsQuery = new GetCouriersByIdsQuery(courierIds);
            var getCourierResponseDTOs = await mediator.Send(getCouriersByIdsQuery);
            var courierModels = getCourierResponseDTOs.Select(getCourierResponseDTO =>
                new CourierModel()
                {
                    Id = getCourierResponseDTO.Id,
                    Name = getCourierResponseDTO.Name,
                    CustomerOrderIds = getCourierResponseDTO.CustomerOrderIds,
                    Price = getCourierResponseDTO.Price,
                    Remark = getCourierResponseDTO.Remark,
                }).ToList();

            return courierModels;
        }

        public async Task<List<CustomerModel>> GetAllCustomers(Guid companyId)
        {
            var getAllCustomerIdsByCompanyIdQuery = new GetAllCustomerIdsByCompanyIdQuery(companyId);
            var customerIds = await mediator.Send(getAllCustomerIdsByCompanyIdQuery);
            return await GetCustomers(customerIds);
        }

        public async Task<List<CustomerModel>> GetCustomers(List<Guid> customerIds)
        {
            var getCustomersByIdsQuery = new GetCustomersByIdsQuery(customerIds);
            var getCustomerResponseDTOs = await mediator.Send(getCustomersByIdsQuery);
            var customerModels = getCustomerResponseDTOs.Select(getCustomerResponseDTO =>
                new CustomerModel()
                {
                    Id = getCustomerResponseDTO.Id,
                    Address = getCustomerResponseDTO.Address,
                    CustomerOrderIds = getCustomerResponseDTO.CustomerOrderIds,
                    Email = getCustomerResponseDTO.Email,
                    FirstName = getCustomerResponseDTO.FirstName,
                    LastName = getCustomerResponseDTO.LastName,
                    
                }).ToList();

            return customerModels;
        }

        public async Task<List<CustomerOrderModel>> GetAllCustomerOrders(Guid companyId)
        {
            var getAllCustomerOrderIdsByCompanyIdQuery = new GetAllCustomerOrderIdsByCompanyIdQuery(companyId);
            var customerOrderIds = await mediator.Send(getAllCustomerOrderIdsByCompanyIdQuery);
            return await GetCustomerOrders(customerOrderIds);
        }

        public async Task<List<CustomerOrderModel>> GetCustomerOrders(List<Guid> customerOrderIds)
        {
            var getCustomerOrdersByIdsQuery = new GetCustomerOrdersByIdsQuery(customerOrderIds);
            var getCustomerOrderResponseDTOs = await mediator.Send(getCustomerOrdersByIdsQuery);
            var customerOrderModels = getCustomerOrderResponseDTOs.Select(getCustomerOrderResponseDTO =>
                new CustomerOrderModel()
                {
                    Id = getCustomerOrderResponseDTO.Id,
                    BinId = getCustomerOrderResponseDTO.BinId,
                    CourierId = getCustomerOrderResponseDTO.CourierId,
                    CustomerOrderDetailIds = getCustomerOrderResponseDTO.CustomerOrderDetailIds,
                    CustomerId = getCustomerOrderResponseDTO.CustomerId,
                    ExpectedArrivalDate = getCustomerOrderResponseDTO.ExpectedArrivalDate,
                    OrderAddress = getCustomerOrderResponseDTO.OrderAddress,
                    OrderCreationDate = getCustomerOrderResponseDTO.OrderCreationDate,
                }).ToList();

            return customerOrderModels;
        }

        public async Task<List<CustomerOrderDetailModel>> GetAllCustomerOrderDetails(Guid companyId)
        {
            var getAllCustomerOrderDetailIdsByCompanyIdQuery = new GetAllCustomerOrderDetailIdsByCompanyIdQuery(companyId);
            var customerOrderDetailIds = await mediator.Send(getAllCustomerOrderDetailIdsByCompanyIdQuery);
            return await GetCustomerOrderDetails(customerOrderDetailIds);
        }

        public async Task<List<CustomerOrderDetailModel>> GetCustomerOrderDetails(List<Guid> customerOrderDetailIds)
        {
            var getCustomerOrderDetailsByIdsQuery = new GetCustomerOrderDetailsByIdsQuery(customerOrderDetailIds);
            var getCustomerOrderDetailResponseDTOs = await mediator.Send(getCustomerOrderDetailsByIdsQuery);
            var customerOrderDetailModels = getCustomerOrderDetailResponseDTOs.Select(getCustomerOrderDetailResponseDTO =>
                new CustomerOrderDetailModel()
                {
                    Id = getCustomerOrderDetailResponseDTO.Id,
                    CustomerOrderId = getCustomerOrderDetailResponseDTO.CustomerOrderId,
                    ProductId = getCustomerOrderDetailResponseDTO.ProductId,
                    Quantity = getCustomerOrderDetailResponseDTO.Quantity,
                    Status = getCustomerOrderDetailResponseDTO.Status,
                }).ToList();

            return customerOrderDetailModels;
        }

        public async Task<List<ProductModel>> GetAllProducts(Guid companyId)
        {
            var getAllProductIdsByCompanyIdQuery = new GetAllProductIdsByCompanyIdQuery(companyId);
            var productIds = await mediator.Send(getAllProductIdsByCompanyIdQuery);
            return await GetProducts(productIds);
        }

        public async Task<List<ProductModel>> GetProducts(List<Guid> productIds)
        {
            var getProductsByIdsQuery = new GetProductsByIdsQuery(productIds);
            var getProductResponseDTOs = await mediator.Send(getProductsByIdsQuery);
            var productModels = getProductResponseDTOs.Select(getProductResponseDTO =>
                new ProductModel()
                {
                    Id = getProductResponseDTO.Id,
                    Name = getProductResponseDTO.Name,
                    CustomerOrderDetailIds = getProductResponseDTO.CustomerOrderDetailIds,
                    Description = getProductResponseDTO.Description,
                    Height = getProductResponseDTO.Height,
                    IncomingOrderIds = getProductResponseDTO.IncomingOrderIds,
                    Length = getProductResponseDTO.Length,
                    PhotoIds = new List<Guid>(),
                    Price = getProductResponseDTO.Price,
                    ProductGroupIds = getProductResponseDTO.ProductGroupIds,
                    RackIds = getProductResponseDTO.RackIds,
                    RefundOrderIds = getProductResponseDTO.RefundOrderIds,
                    ShopIds = getProductResponseDTO.ShopIds,
                    Sku = getProductResponseDTO.Sku,
                    Tag = getProductResponseDTO.Tag,
                    Weight = getProductResponseDTO.Weight,
                    Width = getProductResponseDTO.Width,
                }).ToList();

            return productModels;
        }

        public async Task<List<ProductGroupModel>> GetAllProductGroups(Guid companyId)
        {
            var getAllProductGroupIdsByCompanyIdQuery = new GetAllProductGroupIdsByCompanyIdQuery(companyId);
            var productGroupIds = await mediator.Send(getAllProductGroupIdsByCompanyIdQuery);
            return await GetProductGroups(productGroupIds);
        }

        public async Task<List<ProductGroupModel>> GetProductGroups(List<Guid> productGroupIds)
        {
            var getProductGroupsByIdsQuery = new GetProductGroupsByIdsQuery(productGroupIds);
            var getProductGroupResponseDTOs = await mediator.Send(getProductGroupsByIdsQuery);
            var productGroupModels = getProductGroupResponseDTOs.Select(getProductGroupResponseDTO =>
                new ProductGroupModel()
                {
                    Id = getProductGroupResponseDTO.Id,
                    Name = getProductGroupResponseDTO.Name,
                    ProductIds = getProductGroupResponseDTO.ProductIds,
                    Description = getProductGroupResponseDTO.Description,
                    PhotoIds = getProductGroupResponseDTO.PhotoIds,
                }).ToList();

            return productGroupModels;
        }

        public async Task<List<RefundOrderModel>> GetAllRefundOrders(Guid companyId)
        {
            var getAllRefundOrderIdsByCompanyIdQuery = new GetAllRefundOrderIdsByCompanyIdQuery(companyId);
            var refundOrderIds = await mediator.Send(getAllRefundOrderIdsByCompanyIdQuery);
            return await GetRefundOrders(refundOrderIds);
        }

        public async Task<List<RefundOrderModel>> GetRefundOrders(List<Guid> refundOrderIds)
        {
            var getRefundOrdersByIdsQuery = new GetRefundOrdersByIdsQuery(refundOrderIds);
            var getRefundOrderResponseDTOs = await mediator.Send(getRefundOrdersByIdsQuery);
            var refundOrderModels = getRefundOrderResponseDTOs.Select(getRefundOrderResponseDTO =>
                new RefundOrderModel()
                {
                    Id = getRefundOrderResponseDTO.Id,
                    RefundDate = getRefundOrderResponseDTO.RefundDate,
                    RefundOrderProductIds = getRefundOrderResponseDTO.RefundOrderProductIds,
                    RefundReason = getRefundOrderResponseDTO.RefundReason,
                    Status = getRefundOrderResponseDTO.Status,
                }).ToList();

            return refundOrderModels;
        }

        public async Task<List<RefundOrderProductModel>> GetAllRefundOrderProducts(Guid companyId)
        {
            var getAllRefundOrderProductIdsByCompanyIdQuery = new GetAllRefundOrderProductIdsByCompanyIdQuery(companyId);
            var refundOrderProductIds = await mediator.Send(getAllRefundOrderProductIdsByCompanyIdQuery);
            return await GetRefundOrderProducts(refundOrderProductIds);
        }

        public async Task<List<RefundOrderProductModel>> GetRefundOrderProducts(List<Guid> refundOrderProductIds)
        {
            var getRefundOrderProductsByIdsQuery = new GetRefundOrderProductsByIdsQuery(refundOrderProductIds);
            var getRefundOrderProductResponseDTOs = await mediator.Send(getRefundOrderProductsByIdsQuery);
            var refundOrderProductModels = getRefundOrderProductResponseDTOs.Select(getRefundOrderProductResponseDTO =>
                new RefundOrderProductModel()
                {
                    Id = getRefundOrderProductResponseDTO.Id,
                    ProductId = getRefundOrderProductResponseDTO.ProductId,
                    Quantity = getRefundOrderProductResponseDTO.Quantity,
                    RefundOrderId = getRefundOrderProductResponseDTO.RefundOrderId,
                    Status = getRefundOrderProductResponseDTO.Status,
                }).ToList();

            return refundOrderProductModels;
        }

        public async Task<List<ShopModel>> GetAllShops(Guid companyId)
        {
            var getAllShopIdsByCompanyIdQuery = new GetAllShopIdsByCompanyIdQuery(companyId);
            var shopIds = await mediator.Send(getAllShopIdsByCompanyIdQuery);
            return await GetShops(shopIds);
        }

        public async Task<List<ShopModel>> GetShops(List<Guid> shopIds)
        {
            var getShopsByIdsQuery = new GetShopsByIdsQuery(shopIds);
            var getShopResponseDTOs = await mediator.Send(getShopsByIdsQuery);
            var shopModels = getShopResponseDTOs.Select(getShopResponseDTO =>
                new ShopModel()
                {
                    Id = getShopResponseDTO.Id,
                    Name = getShopResponseDTO.Name,
                    Address = getShopResponseDTO.Address,
                    ProductIds = getShopResponseDTO.ProductIds,
                    Platform = getShopResponseDTO.Platform,
                    Website = getShopResponseDTO.Website,
                }).ToList();

            return shopModels;
        }
    }
}
