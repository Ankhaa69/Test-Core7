using ItemManagment.Helpers;
using ItemManagment.Models.DataTransferModels;

namespace ItemManagment.Services.Interface
{
    public interface ISupplierService
    {
        Task<Response<SupplierDto?>> GetSupplierById(int id);
        Task<Response<IEnumerable<SupplierDto>?>> GetAllSupplierByShopId(int shopId);
        Task<Response<SupplierDto>> AddSupplier(createSupplierDto createSupplierDto);
        Task<Response<SupplierDto>> UpdateSupplier(int supplierId, createSupplierDto createSupplierDto);
        Task<Response<bool>> DeleteSupplier(int supplierId);
        Task<Response<SupplierDto>> AddItemsToSupplier(int supplierId, int[] itemIds);
        Task<Response<SupplierDto>> RemoveItemsFromSupplier(int supplierId, int[] itemIds);
    }
}
