using InventoryManagement.Infrastructure.Entities;

namespace InventoryManagement.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<Transaction> Transactions { get; }
        Task<int> CompleteAsync();
    }
}