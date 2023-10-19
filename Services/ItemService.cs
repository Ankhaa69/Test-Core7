using AutoMapper;
using ItemManagment.Helpers;
using ItemManagment.Helpers.Specifications;
using ItemManagment.Models;
using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Dynamic.Core;
using static ItemManagment.Helpers.Specifications.ItemSpecification;

namespace ItemManagment.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Шинээр үүсэх барааны мэдээлэлийг шалгаж бааз руу хадгална
        /// </summary>
        /// <param name="createItemDto">Шинээр үүсгэх барааны мэдээлэл</param>
        /// <returns></returns>
        public async Task<Response<bool>> AddItem(createItemDto createItemDto)
        {
            Response<bool> response = new();
            try
            {
                var createdItem = createItemDto.ConvertToEntity();
                createdItem.Measure = await _itemRepository.GetMeasureByIdOrFirst(createItemDto.MeasureId);
                if (createItemDto.ItemGroupId > 0)
                {
                    createdItem.ItemGroup = await _itemRepository.GetItemGroupById(createItemDto.ItemGroupId);
                }
                else 
                {
                    createdItem.ItemGroupId = null;
                    createdItem.ItemGroup = null;
                }
                if (createItemDto.UnionBarcodeId > 0)
                {
                    createdItem.UnionBarcode = await _itemRepository.GetUnionBarcodeById(createItemDto.UnionBarcodeId);
                }
                else
                {
                    createdItem.UnionBarcodeId = null;
                    createdItem.UnionBarcode = null;
                }
                
                await _itemRepository.AddItem(createdItem);
                response.Data = true;
                response.Message =$"{createdItem.Name} Бараа бүртгэл амжилттай үүслээ";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }
        /// <summary>
        /// Заагдсан дэлгүүрийн ID-гаар бүх барааг нь авна
        /// </summary>
        /// <param name="shopId">Бараануудын авах дэлгүүрийн ID</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Response<List<ItemDto>?>> GetAllItemsbyShopId(int shopId)
        {
            Response<List<ItemDto>?> response = new();
            try
            {
                var items = await _itemRepository.GetAllItemsbyShopId(shopId);
                if (items.IsNullOrEmpty()) throw new Exception("Бараа бүртгэл олдсонгүй");
                var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(items).ToList();
                response.Data = itemDtos;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }
        /// <summary>
        /// Тухайн дэлгүүр дээрх бүх барааг хуудаслаж явуулна
        /// </summary>
        /// <param name="paramInfo">Хуудсын дугаар, Хуудсын хэмжээ, Дэлгүүрийн ID</param>
        /// <returns></returns>
        public async Task<Helpers.PagedResult<List<ItemDto>>> GetAllItemsWithPagination(PaginationParamDto paramInfo)
        {
            Helpers.PagedResult<List<ItemDto>> pagedResult = new();
            try
            {
                var items = await _itemRepository.GetAllItems(paramInfo.shopId);
                if (items.IsNullOrEmpty()) throw new Exception("Бараа бүртгэл олдсонгүй"); //Front.toi tohirno
                var pagedItems = items.Skip((paramInfo.pageNumber - 1) * paramInfo.pageSize).Take(paramInfo.pageSize).ToList();
                var pagedItemDtos = _mapper.Map<IEnumerable<ItemDto>>(pagedItems).ToList();

                int totalItems = await _itemRepository.GetTotalItemCounts(paramInfo.shopId);
                var totalPages = (int)Math.Ceiling(totalItems / (double)paramInfo.pageSize);
                var paginationInfo = new PaginationInfo
                {
                    CurrentPage = paramInfo.pageNumber,
                    PageSize = paramInfo.pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages
                };
                pagedResult = new Helpers.PagedResult<List<ItemDto>>(pagedItemDtos, paginationInfo);
            }
            catch (Exception ex)
            {
                pagedResult.Succeeded = false;
                pagedResult.Errors = new string[] { ex.Message };
            }
            return pagedResult;
        }
        /// <summary>
        /// Нийт барааны бүртгэлийн тоог олно
        /// </summary>
        /// <param name="shopId"> бараанд харгалзах дэлгүүрийн ID</param>
        /// <returns></returns>
        public async Task<Response<int>> GetTotalItemCounts(int shopId) {
            Response<int> response = new();
            try
            {
                response.Data = await _itemRepository.GetTotalItemCounts(shopId);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }
        /// <summary>
        /// ID-гаар бараа бүртгэлийг олох
        /// </summary>
        /// <param name="id">хайх барааны ID</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Response<ItemDto?>> GetItemById(int id)
        {
            Response<ItemDto?> response = new();
            try
            {
                if (id <= 0) throw new ArgumentNullException(id + " Дуудсан бараа бүртгэлийн Id буруу байна");

                var item = await _itemRepository.GetItemById(id);
                if (item is null) throw new Exception("Бараа бүртгэл олдсонгүй");

                var itemDto = _mapper.Map<ItemDto>(item);
                response.Data = itemDto;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            
            return response;
        }
        /// <summary>
        /// Барааны мэдээллийг шинэчлэх
        /// </summary>
        /// <param name="itemId">Шинэчлэх барааны ID</param>
        /// <param name="itemDto">Шинэчлэгдэж буй мэдээлэл</param>
        /// <returns></returns>
        public async Task<Response<ItemDto?>> UpdateItem(int itemId, createItemDto itemDto)
        {
            Response<ItemDto?> response = new();
            try
            {
                if (itemDto is null)
                    throw new ArgumentNullException(nameof(itemDto), "Шинэчлэх барааны мэдээлэл олдсонгүй");

                var item = await _itemRepository.GetItemById(itemId);

                if (item is null)
                    throw new ArgumentException("Шинэчлэх барааны дугаар олдсонгүй", nameof(itemId));

                var updatingItem = itemDto.UpdateOnEntity(item);

                updatingItem.Measure = await _itemRepository.GetMeasureByIdOrFirst(itemDto.MeasureId);
                if (itemDto.ItemGroupId > 0)
                {
                    var itemGroup = await _itemRepository.GetItemGroupById(itemDto.ItemGroupId);
                    updatingItem.ItemGroup = itemGroup;
                }
                if (itemDto.UnionBarcodeId > 0)
                {
                    var UnionBarcode = await _itemRepository.GetUnionBarcodeById(itemDto.UnionBarcodeId);
                    updatingItem.UnionBarcode = UnionBarcode;
                }
                var updatedItem = await _itemRepository.UpdateItem(item);
                var updatedItemDto = _mapper.Map<ItemDto>(updatedItem);
                response.Data = updatedItemDto;
                response.Message = $"{itemDto.Name} бараа бүртгэлийн мэдээлэл амжилттай шинэчлэгдлээ";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }
        public async Task<Response<bool>> DeleteItem(int itemId) {
            Response<bool> response = new();
            try
            {
                var item = await _itemRepository.GetItemById(itemId);
                if (item is not null)
                {
                    await _itemRepository.DeleteItem(item);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(itemId), $"Item with id {itemId} not found");
                }
                response.Data = true;
                response.Message = $"{itemId} бараа бүртгэлийн мэдээлэл амжилттай устгалаа";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }
        public async Task<Helpers.PagedResult<List<ItemDto>>> GetSpecificationItems(Dictionary<SearchField, string> searchFieldParams, PaginationParamDto paramInfo)
        {
            var spec = new ItemSpecification(searchFieldParams);
            var items = await _itemRepository.ListItem(spec);
            var pagedResult = new Helpers.PagedResult<List<ItemDto>>();
            try
            {
                if (items.IsNullOrEmpty()) throw new ArgumentNullException(string.Format("Search result is null Params: {0}, {1}", searchFieldParams));
                var pagedItems = items.Skip((paramInfo.pageNumber - 1) * paramInfo.pageSize).Take(paramInfo.pageSize).ToList();
                var pagedItemDtos = _mapper.Map<IEnumerable<ItemDto>>(pagedItems).ToList();

                int totalItems = await _itemRepository.GetTotalItemCounts(paramInfo.shopId);
                var totalPages = (int)Math.Ceiling(totalItems / (double)paramInfo.pageSize);
                var paginationInfo = new PaginationInfo
                {
                    CurrentPage = paramInfo.pageNumber,
                    PageSize = paramInfo.pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages
                };
                pagedResult = new Helpers.PagedResult<List<ItemDto>>(pagedItemDtos, paginationInfo);
            }
            catch (Exception ex)
            {
                pagedResult.Succeeded = false;
                pagedResult.Errors = new string[] { ex.Message };
            }
            return pagedResult;
        }       
    }
}
