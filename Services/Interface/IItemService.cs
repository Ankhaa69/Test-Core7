using ItemManagment.Models;
using ItemManagment.Models.DataTransferModels;
using ItemManagment.Helpers;
using static ItemManagment.Helpers.Specifications.ItemSpecification;

namespace ItemManagment.Services.Interface
{
    public interface IItemService
    {
        Task<Response<bool>> AddItem(createItemDto createItemDto);
        Task<Response<List<ItemDto>?>> GetAllItemsbyShopId(int shopId);
        Task<PagedResult<List<ItemDto>>> GetAllItemsWithPagination(PaginationParamDto paramInfo);
        Task<Response<int>> GetTotalItemCounts(int shopId);
        Task<Response<ItemDto?>> GetItemById(int id);
        Task<Response<ItemDto?>> UpdateItem(int itemId, createItemDto itemDto);
        Task<Response<bool>> DeleteItem(int itemId);
       
        Task<PagedResult<List<ItemDto>>> GetSpecificationItems(Dictionary<SearchField, string> searchFieldParams, PaginationParamDto paramInfo);

    }
}
