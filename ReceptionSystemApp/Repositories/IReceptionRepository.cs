using ReceptionSystemApp.Models;

namespace ReceptionSystemApp.Repositories
{
    public interface IReceptionRepository
    {
        Task<IEnumerable<Reception>> GetAllReceptions();
        Task<Reception> GetReceptionById(int id);
        Task CreateReception(Reception reception);
        Task UpdateReception(Reception reception);
        Task DeleteReception(int id);
        bool ReceptionExists(int id);
        Task LoadVisitor(Reception reception);
    }
}
