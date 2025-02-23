using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class ProductRepository(DataContext context) : BaseRepository<ProductEntity>(context)
{
  private readonly DataContext _context = context;
}