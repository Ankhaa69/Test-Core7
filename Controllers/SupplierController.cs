using ItemManagment.Models.DataTransferModels;
using ItemManagment.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SupplierController : ControllerBase
    {
        readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var response = await _supplierService.GetSupplierById(id);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("shop/{shopId}")]
        public async Task<IActionResult> GetAllSupplierByShopId(int shopId)
        {
            var response = await _supplierService.GetAllSupplierByShopId(shopId);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] createSupplierDto createSupplierDto)
        {
            var response = await _supplierService.AddSupplier(createSupplierDto);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }
        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] createSupplierDto createSupplierDto)
        {
            var response = await _supplierService.UpdateSupplier(supplierId, createSupplierDto);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {
            var response = await _supplierService.DeleteSupplier(supplierId);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }
        [HttpPut("Items")]
        public async Task<IActionResult> AddItemsToSupplier([FromQuery] int supplierId,[FromBody] int[] itemIds)
        {
            var response = await _supplierService.AddItemsToSupplier(supplierId,itemIds);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete("Items")]
        public async Task<IActionResult> RemoveItemsFromSupplier([FromQuery] int supplierId, [FromBody] int[] itemIds)
        {
            var response = await _supplierService.RemoveItemsFromSupplier(supplierId, itemIds);
            if (response.Succeeded) return Ok(response);
            return BadRequest(response);
        }
    }
}
