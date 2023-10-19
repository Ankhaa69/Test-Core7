using ItemManagment.Models;
using ItemManagment.Models.DbContexts;
using ItemManagment.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ItemManagment.Services
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemDbContext _dbContext;

        public ItemRepository(ItemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CommitChanges() => await _dbContext.SaveChangesAsync();
        #region ItemMethods
        public async Task<List<Item>> GetAllItems(int shopId) => await _dbContext.Items.AsNoTracking().Where(a => a.ShopId == shopId).ToListAsync();
        public async Task<List<Item>?> GetAllItemsbyShopId(int shopId)
            => await _dbContext.Items
                               .Include(item => item.Measure)
                               .Include(item => item.ItemGroup)
                               .Include(item => item.UnionBarcode)
                               .Where(item => item.ShopId == shopId).ToListAsync();
        public async Task<int> GetTotalItemCounts(int shopId) => await _dbContext.Items.Where(i => i.ShopId == shopId).CountAsync();

        public async Task<Item?> GetItemById(int id) => await _dbContext.Items
                               .Include(item => item.Measure)
                               .Include(item => item.ItemGroup)
                               .Include(item => item.UnionBarcode)
                               .FirstOrDefaultAsync(item => item.Id == id);

        public async Task AddItem(Item item)
        {
            item.CreatedAt = DateTime.Now;
            item.IsDeleted = false;
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Item> UpdateItem(Item item)
        {
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task DeleteItem(Item item)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Item>> ListItem(ISpecification<Item> spec) => await _dbContext.Items.AsNoTracking().Where(spec.Criteria).ToListAsync();
        #endregion
        #region MeasureMethods
        public async Task<Measure?> AddMeasure(Measure measure)
        {
            measure.IsDeleted = false;
            var newMeasure = await _dbContext.Measures.AddAsync(measure);
            var a = await _dbContext.SaveChangesAsync();
            if (a > 0)
            {
                var createdMeasure = await _dbContext.Measures
                                   .FirstOrDefaultAsync(a => a.Id == newMeasure.Entity.Id);
                return createdMeasure;
            }
            else
            {
                return null;
            }
        }
        public async Task DeleteMeasure(Measure measure)
        {
            measure.IsDeleted = true;
            measure.DeletedAt = DateTime.Now;
            _dbContext.Measures.Update(measure);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateMeasure(Measure measure)
        {
            _dbContext.Measures.Update(measure);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Measure> GetFirstMeasure() => await _dbContext.Measures.FirstAsync();
        public async Task<Measure?> GetMeasureById(int? id) => await _dbContext.Measures.Include(m => m.Items).FirstOrDefaultAsync(a => a.Id == id);
        public async Task<Measure?> GetMeasureByIdOrFirst(int? id)
        {
            var measure = await _dbContext.Measures.FirstOrDefaultAsync(a => a.Id == id);
            if (measure is null) return measure = await _dbContext.Measures.FirstOrDefaultAsync();
            return measure;
        }
        public async Task<List<Measure>?> GetAllMeasureByShopId(int Shopid) => await _dbContext.Measures.Where(a => a.ShopId == Shopid).ToListAsync();
        public async Task<bool> ValidateMeasureIdByName(string name, int shopId) => await _dbContext.Measures.AnyAsync(a => a.Name == name && a.ShopId == shopId);
        #endregion
        #region ItemGroupMethods
        public async Task<ItemGroup?> CreateItemGroup(ItemGroup itemGroup)
        {
            itemGroup.CreatedAt = DateTime.Now;
            itemGroup.IsDeleted = false;
            var createdItemGroup = await _dbContext.ItemGroups.AddAsync(itemGroup);
            int a = await _dbContext.SaveChangesAsync();
            if (a > 0)
            {
                var createdItem = await _dbContext.ItemGroups
                                   .FirstOrDefaultAsync(a => a.Id == createdItemGroup.Entity.Id);
                return createdItem;
            }
            else
            {
                return null;
            }
        }
        public async Task DeleteItemGroup(ItemGroup itemGroup)
        {
            itemGroup.IsDeleted = true;
            itemGroup.DeletedAt = DateTime.Now;
            _dbContext.ItemGroups.Update(itemGroup);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<ItemGroup?> GetItemGroupById(int? id) => await _dbContext.ItemGroups.Include(g => g.Items).FirstOrDefaultAsync(a => a.Id == id);
        public async Task<ItemGroup?> GetItemGroupByName(string name, int shopId)
        {
            return await _dbContext.ItemGroups
                .Where(a => a.ShopId == shopId)
                .FirstOrDefaultAsync(a => a.Name == name);
        }
        public async Task<List<ItemGroup>> GetAllItemGroups(int shopId) => await _dbContext.ItemGroups.AsNoTracking().Where(a => a.ShopId == shopId).ToListAsync();
        public async Task<ItemGroup> UpdateItemGroup(ItemGroup itemGroup)
        {
            _dbContext.ItemGroups.Update(itemGroup);
            await _dbContext.SaveChangesAsync();
            return itemGroup;
        }
        public async Task<int> GetTotalItemGroupCounts(int shopId)
        {
            int totalCounts = await _dbContext.ItemGroups.Where(a => a.ShopId == shopId).CountAsync();
            return totalCounts;
        }
        #endregion
        #region UnionBarcodeMethods
        public async Task<UnionBarcode?> GetUnionBarcodeById(int? id) => await _dbContext.UnionBarcodes.FirstOrDefaultAsync(a => a.Id == id);
        public async Task<UnionBarcode?> GetUnionBarcodeByName(string name, int shopId)
        {
            return await _dbContext.UnionBarcodes
                .Where(a => a.ShopId == shopId)
                .FirstOrDefaultAsync(a => a.Name == name);
        }
        #endregion
        #region SupplierMethods
        public async Task<Supplier?> GetSupplierById(int? id) => await _dbContext.Suppliers.FirstOrDefaultAsync(a => a.Id == id);
        public async Task<Supplier?> GetSupplierWithItemById(int id) => await _dbContext.Suppliers.Include(a => a.ItemSuppliers).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<Supplier>?> GetAllSuppliersByShopId(int shopId)=> await _dbContext.Suppliers.Where(a => a.ShopId == shopId).ToListAsync();
        public async Task<Supplier> AddSupplier(Supplier supplier)
        {
            supplier.CreatedAt = DateTime.Now;
            supplier.IsDeleted = false;
            var newSupplier = await _dbContext.Suppliers.AddAsync(supplier);
            var a = await _dbContext.SaveChangesAsync();
            if (a > 0)
            {
                var createdSupplier = await _dbContext.Suppliers
                                   .FirstOrDefaultAsync(a => a.Id == newSupplier.Entity.Id);
                return createdSupplier;
            }
            else
            {
                return null;
            }
        }
        public async Task DeleteSupplier(Supplier supplier)
        {
            supplier.IsDeleted = true;
            supplier.DeletedAt = DateTime.Now;
            _dbContext.Suppliers.Update(supplier);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Supplier?> UpdateSupplier(Supplier supplier)
        {
            _dbContext.Suppliers.Update(supplier);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Suppliers.FirstOrDefaultAsync(s => s.Id == supplier.Id);
        }
        #endregion
    }
}
