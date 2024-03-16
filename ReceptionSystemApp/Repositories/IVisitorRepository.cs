using Microsoft.EntityFrameworkCore;
using ReceptionSystemApp.Data;
using ReceptionSystemApp.Models;

namespace ReceptionSystemApp.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly ReceptionDbContext _dbContext;
        private readonly ILogger<VisitorRepository> _logger;

        public VisitorRepository(ReceptionDbContext dbContext, ILogger<VisitorRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitors()
        {
            try
            {
                return await _dbContext.Visitors.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving visitors");
                throw;
            }
        }

        public async Task<Visitor> GetVisitorById(int id)
        {
            try
            {
                return await _dbContext.Visitors.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving visitor with ID {id}");
                throw;
            }
        }
        public async Task CreateVisitor(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (await _dbContext.Visitors.AnyAsync(v => v.Email == visitor.Email))
            {
                throw new Exception("Visitor with the same email already exists.");
            }

            try
            {
                await _dbContext.Visitors.AddAsync(visitor);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error occurred while creating visitor: {ex.Message}");
                throw new Exception("Failed to create visitor. Please check the provided data.");
            }
        }


        public async Task DeleteVisitor(int id)
        {
            try
            {
                var visitor = await _dbContext.Visitors.FirstOrDefaultAsync(x => x.Id == id);
                if (visitor != null)
                {
                    _dbContext.Remove(visitor);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting visitor with ID: {id}");
                throw;
            }
        }

        public async Task UpdateVisitor(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            try
            {
                _dbContext.Entry(visitor).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Error occurred while updating visitor: {ex.Message}");
                throw new Exception("Failed to update visitor due to concurrency conflict.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating visitor: {ex.Message}");
                throw;
            }
        }
    }
}
