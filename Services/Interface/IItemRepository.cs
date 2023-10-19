using ItemManagment.Helpers;
using ItemManagment.Models;

namespace ItemManagment.Services.Interface
{
    public interface IItemRepository
    {
        #region ItemMethods
        Task<List<Item>> GetAllItems(int shopId);
        Task<int> GetTotalItemCounts(int shopId);
        Task<List<Item>?> GetAllItemsbyShopId(int shopId);
        Task<Item?> GetItemById(int id);
        Task AddItem(Item item);
        Task<Item> UpdateItem(Item item);
        Task DeleteItem(Item item);
        Task<List<Item>> ListItem(ISpecification<Item> spec);
        #endregion
        #region ItemGroupMethods
        Task<ItemGroup?> CreateItemGroup(ItemGroup itemGroup);
        Task<ItemGroup?> GetItemGroupById(int? id);
        Task<ItemGroup?> GetItemGroupByName(string name, int shopId);
        Task DeleteItemGroup(ItemGroup itemGroup);
        Task<ItemGroup> UpdateItemGroup(ItemGroup itemGroup);
        Task<int> GetTotalItemGroupCounts(int shopId);
        Task<List<ItemGroup>> GetAllItemGroups(int shopId);
        #endregion
        #region MeasureMethods
        Task<Measure> GetFirstMeasure();
        Task<Measure?> GetMeasureById(int? id);
        Task<Measure?> GetMeasureByIdOrFirst(int? id);
        Task<List<Measure>?> GetAllMeasureByShopId(int Shopid);
        Task<bool> ValidateMeasureIdByName(string name, int shopId);
        Task<Measure?> AddMeasure(Measure measure);
        Task UpdateMeasure(Measure measure);
        Task DeleteMeasure(Measure measure);
        #endregion
        #region UnionBarcodeMethods
        Task<UnionBarcode?> GetUnionBarcodeById(int? id);
        Task<UnionBarcode?> GetUnionBarcodeByName(string name, int shopId);
        #endregion
        #region SupplierMethods
        Task<Supplier?> GetSupplierById(int? id);
        Task<Supplier?> GetSupplierWithItemById(int id);
        Task<List<Supplier>?> GetAllSuppliersByShopId(int shopId);
        Task<Supplier> AddSupplier(Supplier supplier);
        Task DeleteSupplier(Supplier supplier);
        Task<Supplier?> UpdateSupplier(Supplier supplier);
        #endregion
        Task<int> CommitChanges();
    }
}
