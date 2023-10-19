using ItemManagment.Helpers;
using ItemManagment.Models;
using ItemManagment.Models.DataTransferModels;

namespace ItemManagment.Services.Interface
{
    public interface IItemGroupService
    {
        Task<IEnumerable<ItemGroupDto>> GetAllItemGroups(int ShopId);
        Task<int> GetTotalItemGroupCounts(int shopId);
        Task<List<ItemGroup>> GetAllItemGroupsWithPagination(int pageNumber, int pageSize);
        Task<ItemGroupDto?> GetItemGroupById(int id);
        Task<ItemGroupDto?> AddItemGroup(createItemGroupDto itemGroup);
        Task<ItemGroupDto?> UpdateItemGroup(int itemGroupId, createItemGroupDto itemGroupDto);
        Task DeleteItemGroup(int itemGroupId);
        Task<Response<ItemGroupDto>> AddItemsToGroup(int itemGroupId, List<int> itemIds);
        Task<Response<ItemGroupDto>> RemoveItemsFromGroup(int itemGroupId, List<int> itemIds);
    }
}