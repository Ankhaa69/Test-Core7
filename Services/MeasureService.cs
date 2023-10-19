using AutoMapper;
using ItemManagment.Helpers;
using ItemManagment.Models;
using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Dynamic.Core;

namespace ItemManagment.Services
{
    public class MeasureService : IMeasureService
    {
        readonly IItemRepository _itemRepository;
        readonly IMapper _mapper;
        public MeasureService(IItemRepository itemRepository, IMapper mapper) {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<Response<MeasureDto>> AddMeasure(createMeasureDto createMeasureDto)
        {
            Response<MeasureDto> response = new();
            try
            {
                var existMeasure = await _itemRepository.ValidateMeasureIdByName(createMeasureDto.Name, createMeasureDto.ShopId);
                if (existMeasure) {
                    response.Succeeded = false;
                    response.Message = "Хэмжих нэгж бүртгэлтэй байна. Үргэлжлүүлэх боломжгүй";
                }

                var measure = createMeasureDto.CreateEntity();
                var createdMeasure = await _itemRepository.AddMeasure(measure);
                response.Data = _mapper.Map<MeasureDto>(createdMeasure);
                response.Message = "Хэмжих нэгж амжилттай үүсгэлээ";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message,ex.InnerException.Message };
            }
            return response;
        }


        public async Task<Response<bool>> DeleteMeasure(int measureId)
        {
            Response<bool> response = new();
            try
            {
                var deletingMeasure = await _itemRepository.GetMeasureById(measureId);
                if (deletingMeasure is null) throw new Exception("Хэмжих нэгж олдсонгүй");

                await _itemRepository.DeleteMeasure(deletingMeasure);
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }
        public async Task<Response<bool>> UpdateMeasure(int measureId, createMeasureDto createMeasureDto)
        {
            Response<bool> response = new();
            try
            {
                var existMeasure = await _itemRepository.GetMeasureById(measureId);
                if (existMeasure is null) throw new Exception($"{measureId} ID-тай Хэмжих нэгж олдсонгүй");
                var updatingMeasure = createMeasureDto.UpdateToEntity(existMeasure);
                await _itemRepository.UpdateMeasure(updatingMeasure);
                response.Data = true;
                response.Message = "Хэмжих нэгж амжилттай шинэчиллээ";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }
        public async Task<Response<IEnumerable<MeasureDto>?>> GetAllMeasureByShopId(int shopId)
        {
            Response<IEnumerable<MeasureDto>?> response = new();
            try
            {
                var measures = await _itemRepository.GetAllMeasureByShopId(shopId);
                response.Data = _mapper.Map<IEnumerable<MeasureDto>>(measures);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }

        public async Task<Response<MeasureDto?>> GetMeasureById(int id)
        {
            Response<MeasureDto> response = new();
            try
            {
                var measure = await _itemRepository.GetMeasureById(id);
                if (measure is null) response.Message = $"{id} ID-тай хэмжих нэгж олдсонгүй";
                response.Data = _mapper.Map<MeasureDto>(measure);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message };
            }
            return response;
        }

        public async Task<Response<MeasureDto>> AddItemsToMeasure(int measureId, List<int> itemIds)
        {
            int totalAdded = 0;
            int existAdded = 0;

            Response<MeasureDto> response = new();
            try
            {
                var existMeasure = await _itemRepository.GetMeasureById(measureId);
                if (existMeasure is null) throw new ArgumentNullException($"{measureId} Id-тай бүлэг олдсонгүй");

                List<Item> itemsToAdd = new();
                foreach (var itemId in itemIds)
                {
                    totalAdded++;
                    var item = await _itemRepository.GetItemById(itemId);
                    if (item is null) continue;
                    itemsToAdd.Add(item);
                }
                if (itemsToAdd.IsNullOrEmpty()) throw new ArgumentOutOfRangeException($"{itemIds} Бараанууд олдсонгүй");
                if (existMeasure.Items.IsNullOrEmpty()) existMeasure.Items = new List<Item>();
                var uniqueItemsToAdd = itemsToAdd.Where(i => !existMeasure.Items.Any(gi => gi.Id == i.Id));
                foreach (var item in uniqueItemsToAdd)
                {
                    existMeasure.Items.Add(item);
                }

                await _itemRepository.UpdateMeasure(existMeasure);
                var updatedMeasure = await _itemRepository.GetMeasureById(measureId);
                existAdded = itemsToAdd.Count;
                response.Data = _mapper.Map<MeasureDto>(updatedMeasure);
                response.Message = $"Амжилттай нэмэгдлээ. Нийт: {updatedMeasure.Items.Count}. Шинэчлэгдсэн: {existAdded}/{totalAdded}";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }
        public async Task<Response<MeasureDto>> RemoveItemsFromMeasure(int measureId, List<int> itemIds)
        {
            Response<MeasureDto> response = new();
            try
            {
                var existMeasure = await _itemRepository.GetMeasureById(measureId);
                if (existMeasure is null) throw new ArgumentNullException($"{measureId} Id-тай бүлэг олдсонгүй");
                var itemsToRemove = existMeasure.Items.Where(a => itemIds.Contains(a.Id)).ToList();
                if (itemsToRemove.Count != itemIds.Count)
                {
                    response.Succeeded = false;
                    response.Errors = new string[] { "Хасах бараанууд тухайн бүлэгт олдсонгүй" };
                    return response;
                }
                foreach (var item in itemsToRemove)
                {
                    existMeasure.Items.Remove(item);
                }

                await _itemRepository.UpdateMeasure(existMeasure);
                var updatedMeasure = await _itemRepository.GetMeasureById(measureId);
                response.Data = _mapper.Map<MeasureDto>(updatedMeasure);
                response.Message = $"Амжилттай хасагдлаа. Нийт: {updatedMeasure.Items.Count}";
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

