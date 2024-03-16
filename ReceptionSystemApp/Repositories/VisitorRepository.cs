using ReceptionSystemApp.Models;

namespace ReceptionSystemApp.Repositories
{
    public interface IVisitorRepository
    {
        Task<IEnumerable<Visitor>> GetAllVisitors();
        Task<Visitor> GetVisitorById(int id);
        Task CreateVisitor(Visitor visitor);
        Task DeleteVisitor(int id);
        Task UpdateVisitor(Visitor visitor);
    }
}
