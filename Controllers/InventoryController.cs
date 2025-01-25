using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using WebApplication2.Data;
using WebApplication2.Entities;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            var inventory = await _inventoryService.GetInventoryAsync();
            return Ok(inventory);
        }

        // GET: api/Inventory/{productId}
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetInventoryByProductId(int productId)
        {
            var inventory = await _inventoryService.GetInventoryByProductIdAsync(productId);
            if (inventory == null)
            {
                return NotFound($"No inventory found for product ID: {productId}");
            }
            return Ok(inventory);
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<IActionResult> AddInventory([FromBody] Inventory newInventory)
        {
            if (newInventory == null)
            {
                return BadRequest("Invalid inventory data.");
            }

            var inventory = await _inventoryService.AddInventoryAsync(newInventory);
            return CreatedAtAction(nameof(GetInventoryByProductId), new { productId = inventory.ProductId }, inventory);
        }

        // PUT: api/Inventory/{productId}
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateStock(int productId, [FromBody] UpdateStockRequest request)
        {
            if (request.Quantity == 0)
            {
                return BadRequest("Quantity adjustment must be non-zero.");
            }

            try
            {
                var updatedInventory = await _inventoryService.UpdateStockAsync(productId, request.Quantity);
                return Ok(new
                {
                    message = "Stock updated successfully",
                    updatedInventory
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE: api/Inventory/{productId}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteInventory(int productId)
        {
            try
            {
                var result = await _inventoryService.DeleteInventoryAsync(productId);
                return Ok(new { message = "Product inventory deleted successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

    // Request model for updating stock
    public class UpdateStockRequest
    {
        public int Quantity { get; set; }
    }
}


