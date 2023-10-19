using ItemManagment.Helpers;
using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ItemManagment.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _ItemService;

        public ItemController(IItemService ItemService)
        {
            _ItemService = ItemService;
        }
        /// <summary>
        /// Барааг Id-аар хайж байршуулна.
        /// </summary>
        /// <param name="id">Тухайн барааны Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemByIdAsync(int id)
        {
            var getItem = await _ItemService.GetItemById(id);
            return Ok(getItem);
        }
        /// <summary>
        /// Бараа бүртгэл нэмэх
        /// </summary>
        /// <param name="itemDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] createItemDto itemDto)
        {
            if (!ModelState.IsValid) return BadRequest("Үүсгэх бараа бүртгэл олдсонгүй.");
            Log.Information("AddItem method called: {0}",itemDto);

            var response = await _ItemService.AddItem(itemDto);
            Log.Information("AddItem method called: {0} Success", itemDto);
            return Ok(response);
        }
        /// <summary>
        /// Бараа бүртгэл засварлах
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id,[FromBody] createItemDto itemDto)
        {
            Log.Information("UpdateItem method called: {0} id Updating by {1}",id,itemDto);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var UpdatedItem = await _ItemService.UpdateItem(id,itemDto);
            Log.Information("UpdateItem method called: {0} id Success",id);
            return Ok(UpdatedItem);
        }
        /// <summary>
        /// Бараа бүртгэл устгах
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _ItemService.DeleteItem(id);
            Log.Information("DeleteItem method called: {0} Success", id);
            return NoContent();
        }
        /// <summary>
        /// Тухайн дэлгүүрийн Id-гаар Бараа бүртгэл бүгдийг авах
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [HttpGet("byShopId/{shopId}")]
        public IActionResult GetAllItemsbyShopId(int shopId)
        {
            var itemDtos = _ItemService.GetAllItemsbyShopId(shopId);
            return Ok(itemDtos);
        }
        /// <summary>
        /// Тухайн дэлгүүрийн Id-гаар Бараа бүртгэлийн тоо хэмжээг авах
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [HttpGet("GetAllItemsCount")]
        public async Task<IActionResult> GetTotalItemsCount([FromQuery] int shopId)
        {
            return Ok(await _ItemService.GetTotalItemCounts(shopId));
        }
        /// <summary>
        /// Тухайн Id-тай дэлгүүрийн бүх барааг хуудаслаж авах
        /// </summary>
        /// <param name="paramInfo">Хуудсын дугаар, Хуудас бүрт агуулагдах барааны хэмжээ,Дэлгүүрийн Id</param>
        /// <returns></returns>
        [HttpPost("getPagination")]
        public async Task<IActionResult> GetPaginatedItems([FromBody] PaginationParamDto paramInfo)
        {
            var pagedResult = await _ItemService.GetAllItemsWithPagination(paramInfo);
            return Ok(pagedResult);
        }
        /// <summary>
        /// Бүх талбар дундаас Бараа бүртгэл хайх
        /// </summary>
        /// <param name="specificationDto">searchFieldParams дээр "Хайх талбарын нэр":Утга гэх Лист утга авна, Боломжит хайх талбар Name,BarCode,InternalCode,ShopId</param>
        /// <returns></returns>
        [HttpPost("getSpecification")]
        public async Task<IActionResult> GetSpecificationItems([FromBody] ItemSpecificationDto specificationDto)
        {
            var pagedResult = await _ItemService.GetSpecificationItems(specificationDto.SearchFieldParams, specificationDto.paramInfo);
            return Ok(pagedResult);
        }
    }
}
