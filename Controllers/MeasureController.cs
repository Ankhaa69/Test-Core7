using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services;
using ItemManagment.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ItemManagment.Controllers
{
    /// <summary>
    /// Барааны хэмжих нэгжийн контроллер
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]")]
    public class MeasureController : ControllerBase
    {
        readonly IMeasureService _measureService;
        public MeasureController(IMeasureService measureService)
        {
                _measureService = measureService;
        }

        /// <summary>
        /// Хэмжих нэгжийг Id-аар хайж олох.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasureByIdAsync(int id)
        {
            var getItem = await _measureService.GetMeasureById(id);
            return Ok(getItem);
        }

        /// <summary>
        /// Хэмжих нэгжийг үүсгэх.
        /// </summary>
        /// <param name="measureDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMeasure([FromBody] createMeasureDto measureDto)
        {
            if (!ModelState.IsValid) return BadRequest("Үүсгэх хэмжих нэгж олдсонгүй.");
            Log.Information("AddMeasure method called: {0}", measureDto);

            var response = await _measureService.AddMeasure(measureDto);
            Log.Information("AddMeasure method called: {0} Success", measureDto);
            return Ok(response);
        }

        /// <summary>
        /// Тухайн Id-тай хэмжих нэгжийг засварлах.
        /// </summary>
        /// <param name="id">Засварлах хэмжих нэгжийн Id</param>
        /// <param name="measureDto">Засварласан мэдээлэлүүд</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateMeasure([FromQuery] int id,[FromBody] createMeasureDto measureDto)
        {
            if (!ModelState.IsValid) return BadRequest("Засварлах хэмжих нэгж олдсонгүй.");
            Log.Information("UpdateMeasure method called: {0}", measureDto);

            var response = await _measureService.UpdateMeasure(id, measureDto);
            Log.Information("UpdateMeasure method called: {0} Success", measureDto);
            return Ok(response);
        }

        /// <summary>
        /// Тухайн Id-тай хэмжих нэгжийг устгах.
        /// </summary>
        /// <param name="measureId"> хэмжих нэгжийн Id</param>
        /// <returns></returns>
        [HttpDelete("{measureId}")]
        public async Task<IActionResult> DeleteMeasure(int measureId)
        {
            await _measureService.DeleteMeasure(measureId);
            Log.Information("DeleteMeasure method called: {0} Success", measureId);
            return NoContent();
        }

        /// <summary>
        /// Тухайн дэлгүүр дээрх бүх хэмжих нэгжийг авах.
        /// </summary>
        /// <param name="shopId">дэлгүүрийн Id</param>
        /// <returns></returns>
        [HttpGet("GetAllMeasuresByShopId")]
        public async Task<IActionResult> GetAllMeasuresByShopId([FromQuery] int shopId)
        {
            var measureDtos = await _measureService.GetAllMeasureByShopId(shopId);
            return Ok(measureDtos);
        }

        /// <summary>
        /// Хэмжих нэгжинд бараанууд харгалзуулах
        /// </summary>
        /// <param name="groupId">Бараа нэмэх хэмжих нэгжийн Id</param>
        /// <param name="ItemIds">Нэмэх бараануудын ID [1,2,3] гм</param>
        /// <returns></returns>
        [HttpPost("Items")]
        public async Task<IActionResult> AddItemsToMeasure([FromQuery] int measureId, [FromBody] List<int> ItemIds)
        {
            var result = await _measureService.AddItemsToMeasure(measureId, ItemIds);
            return Ok(result);
        }

        /// <summary>
        /// Хэмжих нэгжээс бараанууд хасах
        /// </summary>
        /// <param name="groupId">Бараа нэмэх хэмжих нэгжийн Id</param>
        /// <param name="ItemIds">Нэмэх бараануудын ID [1,2,3] гм</param>
        /// <returns></returns>
        [HttpDelete("Items")]
        public async Task<IActionResult> RemoveItemsFromGroup([FromQuery] int measureId, [FromBody] List<int> ItemIds)
        {
            var result = await _measureService.RemoveItemsFromMeasure(measureId, ItemIds);
            return Ok(result);
        }

    }
}
