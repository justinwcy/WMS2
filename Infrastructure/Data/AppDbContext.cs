using Domain.Entities;
using Infrastructure.Extensions.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class WmsDbContext :IdentityDbContext<WmsStaff>
    {
        public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffNotification> StaffNotifications { get; set; }
        public DbSet<IncomingOrder> IncomingOrders { get; set; }
        public DbSet<IncomingOrderProduct> IncomingOrderProducts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ProductLocation> ProductLocations { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<ZoneStaff> ZoneStaffs { get; set; }
        public DbSet<Bin> Bins { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSku> ProductSkus { get; set; }
        public DbSet<RefundOrder> RefundOrders { get; set; }
        public DbSet<RefundOrderProduct> RefundOrderProducts { get; set; }
        public DbSet<ProductShop> ProductShops { get; set; }
        public DbSet<Shop> Shops { get; set; }
    }
}
