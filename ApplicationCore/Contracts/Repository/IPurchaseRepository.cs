using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetPurchasesByUserAsync(int userId);
    }
}