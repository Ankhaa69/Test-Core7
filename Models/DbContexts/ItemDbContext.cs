using Microsoft.EntityFrameworkCore;

namespace ItemManagment.Models.DbContexts
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<UnionBarcode> UnionBarcodes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ItemGroup>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Measure>().HasQueryFilter(p => !p.IsDeleted );
            modelBuilder.Entity<UnionBarcode>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Supplier>().HasQueryFilter(p => !p.IsDeleted);

            DataSeeder.SeedMeasures(modelBuilder);
            DataSeeder.SeedItemGroups(modelBuilder);
            DataSeeder.SeedItems(modelBuilder);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.ItemGroup)
                .WithMany(ig => ig.Items)
                .HasForeignKey(i => i.ItemGroupId);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Measure)
                .WithMany(m => m.Items)
                .HasForeignKey(i => i.MeasureId);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.UnionBarcode)
                .WithMany(m => m.Items)
                .HasForeignKey(i => i.UnionBarcodeId);

            modelBuilder.Entity<Item>()
               .HasOne(i => i.UnionBarcode)
               .WithMany(m => m.Items)
               .HasForeignKey(i => i.UnionBarcodeId);

            modelBuilder.Entity<ItemSupplier>()
        .HasKey(ss => new { ss.ItemId, ss.SupplierId });

            modelBuilder.Entity<ItemSupplier>()
                .HasOne(ss => ss.Item)
                .WithMany(i => i.ItemSuppliers)
                .HasForeignKey(ss => ss.ItemId);

            modelBuilder.Entity<ItemSupplier>()
                .HasOne(ss => ss.Supplier)
                .WithMany(s => s.ItemSuppliers)
                .HasForeignKey(ss => ss.SupplierId);
        }
    }
}
