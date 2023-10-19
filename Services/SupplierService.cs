using AutoMapper;
using ItemManagment.Helpers;
using ItemManagment.Models;
using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services.Interface;
using Microsoft.IdentityModel.Tokens;

namespace ItemManagment.Services
{
    public class SupplierService : ISupplierService
    {
        readonly IMapper _mapper;
        readonly IItemRepository _itemRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRepository"></param>
        /// <param name="mapper"></param>
        public SupplierService(IItemRepository itemRepository, IMapper mapper)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<SupplierDto?>> GetSupplierById(int id)
        {
            Response<SupplierDto?> response = new();
            try
            {
                var existSupplier = await _itemRepository.GetSupplierById(id);
                if (existSupplier is null) throw new Exception($"{id} ID-тай Нийлүүлэгч олдсонгүй");
                response.Data = _mapper.Map<SupplierDto>(existSupplier);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<Response<IEnumerable<SupplierDto>?>> GetAllSupplierByShopId(int shopId)
        {
            Response<IEnumerable<SupplierDto>?> response = new();
            try
            {
               var suppliers = await _itemRepository.GetAllSuppliersByShopId(shopId);
                if (suppliers.IsNullOrEmpty()) throw new Exception($"{shopId} ShopId-тай дэлгүүрт Нийлүүлэгч олдсонгүй");
                response.Data = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createSupplierDto"></param>
        /// <returns></returns>
        public async Task<Response<SupplierDto>> AddSupplier(createSupplierDto createSupplierDto)
        {
            Response<SupplierDto> response = new();
            try
            {
                Supplier newSupplier = createSupplierDto.CreateEntity();
                var createdSupplier = await _itemRepository.AddSupplier(newSupplier);
                response.Data = _mapper.Map<SupplierDto>(createdSupplier);
                response.Message = $"{createdSupplier.Name} Нийлүүлэгч амжилттай үүсгэлээ";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> DeleteSupplier(int supplierId)
        {
            Response<bool> response = new();
            try
            {
                var deletingSupplier = await _itemRepository.GetSupplierById(supplierId);
                if (deletingSupplier is null) throw new Exception("Нийлүүлэгч олдсонгүй");

                await _itemRepository.DeleteSupplier(deletingSupplier);
                response.Data = true;
                response.Message = $"{deletingSupplier.Name} Нийлүүлэгч амжилттай устгалаа";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="createSupplierDto"></param>
        /// <returns></returns>
        public async Task<Response<SupplierDto>> UpdateSupplier(int supplierId, createSupplierDto createSupplierDto)
        {
            Response<SupplierDto> response = new();
            try
            {
                var updatingSupplier = await _itemRepository.GetSupplierById(supplierId);
                if (updatingSupplier is null) throw new Exception("Нийлүүлэгч олдсонгүй");

                updatingSupplier = createSupplierDto.UpdateToEntity(updatingSupplier);
                var updatedSupplier = await _itemRepository.UpdateSupplier(updatingSupplier);
                if (updatedSupplier is null) throw new Exception("Нийлүүлэгчийн мэдээлэл шинэчлэлт амжилтгүй боллоо");
                response.Data = _mapper.Map<SupplierDto>(updatedSupplier);
                response.Message = $"{updatedSupplier.Name} Нийлүүлэгч амжилттай шинэчилэгдлээ";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="itemIds"></param>
        /// <returns></returns>
        public async Task<Response<SupplierDto>> AddItemsToSupplier(int supplierId, int[] itemIds)
        {
            int allItemCount = itemIds.Count();
            int updatedItemCount = 0;
            Response<SupplierDto> response = new();
            try
            {
                var updatingSupplier = await _itemRepository.GetSupplierWithItemById(supplierId);
                if (updatingSupplier is null) throw new Exception("Нийлүүлэгч олдсонгүй");

                List<Item> updatingItems = new();
                foreach (var id in itemIds)
                {
                    var item = await _itemRepository.GetItemById(id);
                    if (item is null || updatingSupplier.ItemSuppliers.Any(a => a.ItemId == id)) continue;
                    updatedItemCount++;
                    updatingSupplier.ItemSuppliers.Add(new ItemSupplier { ItemId = id, SupplierId = supplierId });
                }

                var updatedSupplier = await _itemRepository.UpdateSupplier(updatingSupplier);
                if (updatedSupplier is null) throw new Exception("Нийлүүлэгчийн мэдээлэл шинэчлэлт амжилтгүй боллоо");
                response.Data = _mapper.Map<SupplierDto>(updatedSupplier);
                response.Message = $"{updatedSupplier.Name} Нийлүүлэгч амжилттай шинэчилэгдлээ. {updatedItemCount}/{allItemCount} ";
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors = new string[] { ex.Message, ex.InnerException?.Message };
            }
            return response;
        }
        public async Task<Response<SupplierDto>> RemoveItemsFromSupplier(int supplierId, int[] itemIds)
        {
            int allItemCount = itemIds.Count();
            int updatedItemCount = 0;
            Response<SupplierDto> response = new();
            try
            {
                var updatingSupplier = await _itemRepository.GetSupplierWithItemById(supplierId);
                if (updatingSupplier is null) throw new Exception("Нийлүүлэгч олдсонгүй");
                var itemsToRemove = updatingSupplier.ItemSuppliers
                                              .Where(s => itemIds.Contains(s.ItemId))
                                              .ToList();
                if (itemsToRemove.IsNullOrEmpty()) throw new Exception("Нийлүүлэгч олдсонгүй");
                foreach (var itemToRemove in itemsToRemove)
                {
                    updatingSupplier.ItemSuppliers.Remove(itemToRemove);
                    updatedItemCount++;
                }
                var updatedSupplier = await _itemRepository.UpdateSupplier(updatingSupplier);
                if (updatedSupplier is null) throw new Exception("Нийлүүлэгчийн мэдээлэл шинэчлэлт амжилтгүй боллоо");

                response.Data = _mapper.Map<SupplierDto>(updatedSupplier);
                response.Message = $"{updatedSupplier.Name} Нийлүүлэгч амжилттай шинэчилэгдлээ. {updatedItemCount}/{allItemCount} ";
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
