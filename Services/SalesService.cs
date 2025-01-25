using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;

namespace WebApplication2.Services
{
    public class SalesService
    {
        private readonly AppDbContext _context;

        public SalesService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all sales records, including related User and ProductInformation data.
        /// </summary>
        /// <returns>List of sales.</returns>
        public async Task<List<SalesRetrive>> GetAllSalesAsync()
        {
            return await _context.SalesRetrive
                .Include(s => s.User)
                .Include(s => s.Product)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves sales for a specific user by UserId.
        /// </summary>
        public async Task<List<SalesRetrive>> GetSalesByUserAsync(int userId)
        {
            return await _context.SalesRetrive
                .Where(s => s.UserId == userId)
                .Include(s => s.User)
                .Include(s => s.Product)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific sale by its SaleId.
        /// </summary>
        public async Task<SalesRetrive> GetSaleByIdAsync(int saleId)
        {
            try
            {
                var sale = await _context.SalesRetrive
                    .Include(s => s.User)
                    .Include(s => s.Product)
                    .FirstOrDefaultAsync(s => s.SalesId == saleId);

                if (sale == null)
                    throw new KeyNotFoundException($"Sale with ID {saleId} not found.");

                return sale;
            }
            catch (Exception ex)
            {
                // Log the exception (use a logger like Serilog/NLog)
                throw new Exception($"Error retrieving sale with ID {saleId}: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        public async Task<Sales> CreateSaleAsync(Sales newSale)
        {
            try
            {
                if (newSale == null)
                    throw new ArgumentException("Sale data is required.");

                if (newSale.Quantity <= 0)
                    throw new ArgumentException("Quantity must be greater than 0.");

                if (newSale.TotalPrice <= 0)
                    throw new ArgumentException("TotalPrice must be greater than 0.");

                var userExists = await _context.Users.AnyAsync(u => u.Id == newSale.UserId);
                if (!userExists)
                    throw new ArgumentException($"User with ID {newSale.UserId} does not exist.");

                var productExists = await _context.ProductInformation.AnyAsync(p => p.ProductId == newSale.ProductId);
                if (!productExists)
                    throw new ArgumentException($"Product with ID {newSale.ProductId} does not exist.");

                _context.Sales.Add(newSale);
                await _context.SaveChangesAsync();

                return newSale;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"Error creating sale: {ex.Message}");
            }
        }
    }
}






