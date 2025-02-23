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
        public DbSet<ProductRack> ProductRacks { get; set; }
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
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductGroupProduct> ProductGroupProducts { get; set; }
        public DbSet<RefundOrder> RefundOrders { get; set; }
        public DbSet<RefundOrderProduct> RefundOrderProducts { get; set; }
        public DbSet<ProductShop> ProductShops { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ManyToManyRelationships
            modelBuilder
                .Entity<Shop>()
                .HasMany(shop => shop.Products)
                .WithMany(product => product.Shops)
                .UsingEntity<ProductShop>(
                    r =>
                        r.HasOne<Product>(productShop=>productShop.Product)
                            .WithMany()
                            .HasForeignKey(productShop => productShop.ProductId)
                            .OnDelete(DeleteBehavior.Cascade),
                    l =>
                        l.HasOne<Shop>(productShop => productShop.Shop)
                            .WithMany()
                            .HasForeignKey(productShop => productShop.ShopId)
                            .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder
                .Entity<Product>()
                .HasMany(product => product.Racks)
                .WithMany(rack => rack.Products)
                .UsingEntity<ProductRack>(
                    r =>
                        r.HasOne<Rack>(productRack=>productRack.Rack)
                            .WithMany()
                            .HasForeignKey(productRack => productRack.RackId)
                            .OnDelete(DeleteBehavior.Cascade),
                    l =>
                        l.HasOne<Product>(productRack => productRack.Product)
                            .WithMany()
                            .HasForeignKey(productRack => productRack.ProductId)
                            .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder
                .Entity<Zone>()
                .HasMany(zone => zone.Staffs)
                .WithMany(staff => staff.Zones)
                .UsingEntity<ZoneStaff>(
                    r =>
                        r.HasOne<Staff>(zoneStaff=>zoneStaff.Staff)
                            .WithMany()
                            .HasForeignKey(zoneStaff => zoneStaff.StaffId)
                            .OnDelete(DeleteBehavior.Cascade),
                    l =>
                        l.HasOne<Zone>(zoneStaff => zoneStaff.Zone)
                            .WithMany()
                            .HasForeignKey(zoneStaff => zoneStaff.ZoneId)
                            .OnDelete(DeleteBehavior.NoAction)
                );

            modelBuilder
                .Entity<ProductGroup>()
                .HasMany(productGroup => productGroup.Products)
                .WithMany(product => product.ProductGroups)
                .UsingEntity<ProductGroupProduct>(
                    r =>
                        r.HasOne<Product>(productGroupProduct => productGroupProduct.Product)
                            .WithMany()
                            .HasForeignKey(productGroupProduct => productGroupProduct.ProductId)
                            .OnDelete(DeleteBehavior.Cascade),
                    l =>
                        l.HasOne<ProductGroup>(productGroupProduct => productGroupProduct.ProductGroup)
                            .WithMany()
                            .HasForeignKey(productGroupProduct => productGroupProduct.ProductGroupId)
                            .OnDelete(DeleteBehavior.Cascade)
                );

            #endregion

            #region OneToManyRelationships
            modelBuilder
                .Entity<RefundOrder>()
                .HasMany(refundOrder => refundOrder.RefundOrderProducts)
                .WithOne(refundOrderProduct => refundOrderProduct.RefundOrder)
                .HasForeignKey(refundOrderProduct => refundOrderProduct.RefundOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<IncomingOrder>()
                .HasMany(incomingOrder => incomingOrder.IncomingOrderProducts)
                .WithOne(incomingOrderProduct => incomingOrderProduct.IncomingOrder)
                .HasForeignKey(incomingOrderProduct => incomingOrderProduct.IncomingOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Vendor>()
                .HasMany(vendor => vendor.IncomingOrders)
                .WithOne(incomingOrder => incomingOrder.Vendor)
                .HasForeignKey(incomingOrder => incomingOrder.VendorId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder
                .Entity<CustomerOrderDetail>()
                .HasOne(customerOrderDetail => customerOrderDetail.Product)
                .WithMany(product => product.CustomerOrderDetails)
                .HasForeignKey(product => product.CustomerOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<CustomerOrder>()
                .HasMany(customerOrder => customerOrder.CustomerOrderDetails)
                .WithOne(customerOrderDetail => customerOrderDetail.CustomerOrder)
                .HasForeignKey(customerOrderDetail => customerOrderDetail.CustomerOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Customer>()
                .HasMany(customer => customer.CustomerOrders)
                .WithOne(customerOrder => customerOrder.Customer)
                .HasForeignKey(customerOrder => customerOrder.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Courier>()
                .HasMany(courier => courier.CustomerOrders)
                .WithOne(customerOrder => customerOrder.Courier)
                .HasForeignKey(customerOrder => customerOrder.CourierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Bin>()
                .HasMany(bin => bin.CustomerOrders)
                .WithOne(customerOrder => customerOrder.Bin)
                .HasForeignKey(customerOrder => customerOrder.BinId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Zone>()
                .HasMany(zone => zone.Racks)
                .WithOne(rack => rack.Zone)
                .HasForeignKey(rack => rack.ZoneId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Company>()
                .HasMany(company => company.Staffs)
                .WithOne(staff => staff.Company)
                .HasForeignKey(staff => staff.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Staff>()
                .HasMany(staff => staff.StaffNotifications)
                .WithOne(staffNotification => staffNotification.Staff)
                .HasForeignKey(staffNotification => staffNotification.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Company>()
                .HasMany(company => company.Warehouses)
                .WithOne(warehouse => warehouse.Company)
                .HasForeignKey(warehouse => warehouse.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Warehouse>()
                .HasMany(warehouse => warehouse.Zones)
                .WithOne(zone => zone.Warehouse)
                .HasForeignKey(zone => zone.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ProductGroup>()
                .HasMany(productGroup => productGroup.Photos)
                .WithOne(photo => photo.ProductGroup)
                .HasForeignKey(photo => photo.ProductGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region OneToOneRelationships
            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.CurrentInventory)
                .WithOne(inventory => inventory.Product)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
    
}
