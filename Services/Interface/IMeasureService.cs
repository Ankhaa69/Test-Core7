using ItemManagment.Helpers;
using ItemManagment.Models;
using ItemManagment.Models.DataTransferModels;

namespace ItemManagment.Services.Interface
{
    public interface IMeasureService
    {
        Task<Response<MeasureDto?>> GetMeasureById(int id);
        Task<Response<IEnumerable<MeasureDto>?>> GetAllMeasureByShopId(int shopId);
        Task<Response<MeasureDto>> AddMeasure(createMeasureDto createMeasureDto);
        Task<Response<bool>> UpdateMeasure(int measureId, createMeasureDto createMeasureDto);
        Task<Response<bool>> DeleteMeasure(int measureId);
        Task<Response<MeasureDto>> AddItemsToMeasure(int measureId, List<int> itemIds);
        Task<Response<MeasureDto>> RemoveItemsFromMeasure(int measureId, List<int> itemIds);
    }
}
