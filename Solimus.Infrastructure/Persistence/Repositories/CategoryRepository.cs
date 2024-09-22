using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;
using Solimus.Infrastructure.Persistence.Context;

namespace Solimus.Infrastructure.Persistence.Repositories;

public class CategoryRepository(SolimusContext context) : GenericRepository<Category>(context), ICategoryRepository
{
}
