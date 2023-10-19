using Microsoft.EntityFrameworkCore;

namespace ItemManagment.Models.DbContexts
{
    public static class DataSeeder
    {
        public static void SeedMeasures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measure>().HasData(
                new Measure {Id = 1, Name = "Kilogram", Description = "Нэгж Масс хэмжих", Type = 0 },
                new Measure { Id = 2, Name = "Meter",Description = "Нэгж уртаар хэмжих",Type = 0 },
                new Measure { Id = 3, Name = "Piece", Description = "Нэгж хэсгээр хэмжих", Type = 0 }
            );
        }
        public static void SeedItemGroups(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemGroup>().HasData(
                new ItemGroup { Id = 1,Name = "Үндсэн бүлэг",Description = "Лангуун дээрх бараанууд",Code="001",Type = 0,ShopId = 0001}
            );
        }
        public static void SeedItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, InternalCode = 001, BarCode = "1234567890", Name = "Талх Атар,180гр", Price = 2400, Cost = 2000, WholePrice = 2200, WholeQuantity = 10, PackageQuantity = 30, MeasureId = 1, ShopId = 0001,ItemGroupId = 1, IsDeleted = false, TaxType = Helpers.Type.TaxType.Vat },
                new Item { Id = 2, InternalCode = 002, BarCode = "1233445566", Name = "Коко кола, Бидний, 300мг", Price = 2800, Cost = 2500, WholePrice = 2600, WholeQuantity = 30, PackageQuantity = 50, MeasureId = 1, ShopId = 0001, ItemGroupId = 1, IsDeleted = false, TaxType = Helpers.Type.TaxType.Vat },
                new Item { Id = 3, InternalCode = 003, BarCode = "1234455667", Name = "Сэнгүр лаазтай 0.5л", Price = 3000, Cost = 2400, WholePrice = 2800, WholeQuantity = 120, PackageQuantity = 24, MeasureId = 1, ShopId = 0001, ItemGroupId = 1, IsDeleted = false, TaxType = Helpers.Type.TaxType.Vat }
            );
        }
    }

}
