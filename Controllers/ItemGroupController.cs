using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ItemManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemGroupController : ControllerBase
    {
        private readonly IItemGroupService _ItemGroupService;
        public ItemGroupController(IItemGroupService itemGroupService)
        {
            _ItemGroupService = itemGroupService;
        }
        /// <summary>
        /// Бүлгийг id-аар хайж олох
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemGroupById(int id)
        {
            var getItemGroupDto = await _ItemGroupService.GetItemGroupById(id);
            if (getItemGroupDto is null) return NotFound();
            return Ok(getItemGroupDto);
        }
        /// <summary>
        /// Бүлгийг засварлах
        /// </summary>
        /// <param name="id">Засварлах бүлгийн Id</param>
        /// <param name="itemGroupDto">Засварлаж буй мэдээлэл</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemGroup(int id, [FromBody] createItemGroupDto itemGroupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var UpdatedItem = await _ItemGroupService.UpdateItemGroup(id, itemGroupDto);
            return Ok(UpdatedItem);
        }
        /// <summary>
        /// Бүлгийг нэмэх
        /// </summary>
        /// <param name="shopId">Дэлгүүрийн Id</param>
        /// <param name="itemGroupDto">Шинээр үүсгэж буй бүлгийн мэдээлэл</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddItemGroup([FromQuery] int shopId, [FromBody] createItemGroupDto itemGroupDto)
        {
            if (!ModelState.IsValid) return BadRequest("Үүсгэх бүлгийн бүртгэлээ шалгана уу.");

            var addedItemGroup = await _ItemGroupService.AddItemGroup(itemGroupDto);
            return addedItemGroup is null ? NotFound() : Ok(addedItemGroup);
        }
        /// <summary>
        /// Бүлгийг устгах
        /// </summary>
        /// <param name="id">Устгах бүлгийн Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemGroup(int id)
        {
            await _ItemGroupService.DeleteItemGroup(id);
            return NoContent();
        }
        /// <summary>
        /// Тухайн дэлгүүрийн бүх бүлгийг авах
        /// </summary>
        /// <param name="shopId">Дэлгүүрийн Id</param>
        /// <returns></returns>
        [HttpGet("GetAllItemGroup")]
        public async Task<IActionResult> GetAllItemGroupsByShopId([FromQuery] int shopId)
        {
            var itemGroupDtos = await _ItemGroupService.GetAllItemGroups(shopId);
            if (itemGroupDtos.IsNullOrEmpty()) return NotFound();
            return Ok(itemGroupDtos);
        }
        /// <summary>
        /// Тухайн дэлгүүрийн бүх бүлгийн тоог авах
        /// </summary>
        /// <param name="shopId">Дэлгүүрийн тоо</param>
        /// <returns></returns>
        [HttpGet("GetAllItemGroupCount")]
        public async Task<IActionResult> GetTotalItemGroupCounts([FromQuery] int shopId)
        {
          return Ok(await _ItemGroupService.GetTotalItemGroupCounts(shopId));
        }
        /// <summary>
        /// Бүлэгт бараанууд харгалзуулах
        /// </summary>
        /// <param name="groupId">Бараа нэмэх бүлгийн Id</param>
        /// <param name="ItemIds">Нэмэх бараануудын ID [1,2,3] гм</param>
        /// <returns></returns>
        [HttpPost("Items")]
        public async Task<IActionResult> AddItemsToGroup([FromQuery] int groupId, [FromBody] List<int> ItemIds)
        {
            var result = await _ItemGroupService.AddItemsToGroup(groupId, ItemIds);
            return Ok(result);
        }
        /// <summary>
        /// Бүлгээс бараанууд хасах
        /// </summary>
        /// <param name="groupId">Бараа нэмэх бүлгийн Id</param>
        /// <param name="ItemIds">Нэмэх бараануудын ID [1,2,3] гм</param>
        /// <returns></returns>
        [HttpDelete("Items")]
        public async Task<IActionResult> RemoveItemsFromGroup([FromQuery] int groupId, [FromBody] List<int> ItemIds)
        {
            var result = await _ItemGroupService.RemoveItemsFromGroup(groupId, ItemIds);
            return Ok(result);
        }
    }
}
