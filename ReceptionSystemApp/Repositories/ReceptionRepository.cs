using Microsoft.EntityFrameworkCore;
using ReceptionSystemApp.Data;
using ReceptionSystemApp.Models;

namespace ReceptionSystemApp.Repositories
{
    public class ReceptionRepository:IReceptionRepository
    {
        private readonly ReceptionDbContext _dbContext;

        public ReceptionRepository(ReceptionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Reception>> GetAllReceptions()
        {
            try
            {
                return await _dbContext.Receptions.ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error occurred while retrieving receptions.", ex);
            }
        }

        public async Task<Reception> GetReceptionById(int id)
        {
            try
            {
                return await _dbContext.Receptions.FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error occurred while retrieving reception with ID {id}.", ex);
            }
        }

        public async Task CreateReception(Reception reception)
        {
            if (reception == null)
            {
                throw new ArgumentNullException(nameof(reception));
            }

            try
            {
                _dbContext.Receptions.Add(reception);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Failed to create reception. Please check the provided data.", ex);
            }
        }

        public async Task UpdateReception(Reception reception)
        {
            if (reception == null)
            {
                throw new ArgumentNullException(nameof(reception));
            }

            try
            {
                _dbContext.Entry(reception).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error occurred while updating reception with ID {reception.Id}.", ex);
            }
        }

        public async Task DeleteReception(int id)
        {
            try
            {
                var reception = await _dbContext.Receptions.FirstOrDefaultAsync(a => a.Id == id);
                if (reception != null)
                {
                    _dbContext.Receptions.Remove(reception);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception($"Error occurred while deleting reception with ID {id}.", ex);
            }
        }

        public bool ReceptionExists(int id)
        {
            return _dbContext.Receptions.Any(a => a.Id == id);
        }

        public async Task LoadVisitor(Reception reception)
        {
            await _dbContext.Entry(reception).Reference(a => a.Visitor).LoadAsync();
        }

    }
}
