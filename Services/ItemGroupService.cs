using AutoMapper;
using ItemManagment.Helpers;
using ItemManagment.Models;
using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services.Interface;
using Microsoft.IdentityModel.Tokens;

namespace ItemManagment.Services
{
    public class ItemGroupService : IItemGroupService
    {
        private readonly IItemRepository _ItemRepository;
        private readonly IMapper _Mapper;

        public ItemGroupService(IItemRepository itemRepository, IMapper mapper)
        {
            _ItemRepository = itemRepository;
            _Mapper = mapper;
        }

        public async Task<ItemGroupDto?> AddItemGroup(createItemGroupDto itemGroup)
        {
            ItemGroup? CreatingItemGroup = await _ItemRepository.CreateItemGroup(itemGroup.ConvertToModel());
            if (CreatingItemGroup is null) throw new InvalidOperationException("Өгөгдлийн санд хадгалахад алдаа гарлаа");
                return _Mapper.Map<ItemGroupDto>(CreatingItemGroup);
        }

        public async Task DeleteItemGroup(int itemGroupId)
        {
            var itemGroup = await _ItemRepository.GetItemGroupById(itemGroupId);
            if (itemGroup is not null)
            {
                await _ItemRepository.DeleteItemGroup(itemGroup);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(itemGroupId), $"ItemGroup with id {itemGroupId} not found");
            }
        }

        public async Task<IEnumerable<ItemGroupDto>> GetAllItemGroups(int ShopId)
        {
            var ItemGroups = await _ItemRepository.GetAllItemGroups(ShopId);
            if (ItemGroups.IsNullOrEmpty()) throw new ArgumentOutOfRangeException(nameof(ItemGroups), "ItemGroups not found");
            return _Mapper.Map<IEnumerable<ItemGroupDto>>(ItemGroups);
        }

        public async Task<List<ItemGroup>> GetAllItemGroupsWithPagination(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemGroupDto?> GetItemGroupById(int id)
        {
            if (id <= 0) throw new ArgumentNullException(id + " Дуудсан бараа бүртгэлийн Id буруу байна");

            var itemGroup = await _ItemRepository.GetItemGroupById(id);
            if (itemGroup is null) throw new Exception("Бараа бүртгэл олдсонгүй");

            var itemGroupDto = _Mapper.Map<ItemGroupDto>(itemGroup);
            return itemGroupDto;
        }

        public async Task<int> GetTotalItemGroupCounts(int shopId) => await _ItemRepository.GetTotalItemGroupCounts(shopId);

        public async Task<ItemGroupDto?> UpdateItemGroup(int itemGroupId, createItemGroupDto itemGroupDto)
        {
            if (itemGroupDto is null) throw new ArgumentNullException(nameof(itemGroupDto), "Шинэчлэх барааны мэдээлэл олдсонгүй");
            if (itemGroupId <= 0) throw new ArgumentNullException(nameof(itemGroupId), "Шинэчлэх бүлгийн Id буруу байна");
            var itemGroup = await _ItemRepository.GetItemGroupById(itemGroupId);
            if (itemGroup is null) throw new ArgumentException("Шинэчлэх бүлгийн дугаар олдсонгүй", nameof(itemGroupId));
            itemGroup = itemGroupDto.UpdateToEntity(itemGroup);
            var updatedItemGroup = await _ItemRepository.UpdateItemGroup(itemGroup);
            return _Mapper.Map<ItemGroupDto>(updatedItemGroup);
        }
        public async Task<Response<ItemGroupDto>> AddItemsToGroup(int itemGroupId, List<int> itemIds)
        {
            int totalAdded = 0;
            int existAdded = 0;

            Response<ItemGroupDto> response = new();
            try
            {
                var existItemGroup = await _ItemRepository.GetItemGroupById(itemGroupId);
                if (existItemGroup is null) throw new ArgumentNullException($"{itemGroupId} Id-тай бүлэг олдсонгүй");

                if (existItemGroup.Items.IsNullOrEmpty()) existItemGroup.Items = new List<Item>();

                List<Item> itemsToAdd = new();
                foreach (var itemId in itemIds)
                {
                    totalAdded++;
                    var item = await _ItemRepository.GetItemById(itemId);
                    if (item is null) continue;
                    itemsToAdd.Add(item);
                }
                if (itemsToAdd.IsNullOrEmpty()) throw new ArgumentOutOfRangeException($"{itemIds} Бараанууд олдсонгүй");

                var uniqueItemsToAdd = itemsToAdd.Where(i => !existItemGroup.Items.Any(gi => gi.Id == i.Id));
                existItemGroup.Items.AddRange(uniqueItemsToAdd);

                var UpdatedItemGroup = await _ItemRepository.UpdateItemGroup(existItemGroup);
                existAdded = itemsToAdd.Count;
                response.Data = _Mapper.Map<ItemGroupDto>(UpdatedItemGroup);
                response.Message = $"Амжилттай нэмэгдлээ. Нийт: {UpdatedItemGroup.Items.Count}. Шинэчлэгдсэн: {existAdded}/{totalAdded}";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }

        public async Task<Response<ItemGroupDto>> RemoveItemsFromGroup(int itemGroupId, List<int> itemIds)
        {
            Response<ItemGroupDto> response = new();
            try
            {
                var existItemGroup = await _ItemRepository.GetItemGroupById(itemGroupId);
                if (existItemGroup is null) throw new ArgumentNullException($"{itemGroupId} Id-тай бүлэг олдсонгүй");

                var itemsToRemove = existItemGroup.Items.Where(a => itemIds.Contains(a.Id)).ToList();
                if (itemsToRemove.Count != itemIds.Count)
                {
                    response.Succeeded = false;
                    response.Errors = new string[] { "Хасах бараанууд тухайн бүлэгт олдсонгүй" };
                    return response;
                }

                existItemGroup.Items.RemoveAll(item => itemsToRemove.Contains(item));

                var UpdatedItemGroup = await _ItemRepository.UpdateItemGroup(existItemGroup);

                response.Data = _Mapper.Map<ItemGroupDto>(UpdatedItemGroup);
                response.Message = $"Амжилттай хасагдлаа. Нийт: {UpdatedItemGroup.Items.Count}";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }

    }
}
