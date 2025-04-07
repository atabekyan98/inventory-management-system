using InventoryManagement.Infrastructure.Entities;

namespace InventoryManagement.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
            Products = new Repository<Product>(_context);
            Categories = new Repository<Category>(_context);
            Suppliers = new Repository<Supplier>(_context);
            Transactions = new Repository<Transaction>(_context);
        }

        public IRepository<Product> Products { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<Supplier> Suppliers { get; }
        public IRepository<Transaction> Transactions { get; }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}