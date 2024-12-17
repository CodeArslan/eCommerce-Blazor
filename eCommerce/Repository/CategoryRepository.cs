using eCommerce.Data;
using eCommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public CategoryRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _dbcontext.Category.AddAsync(category);
            await _dbcontext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var CategoryInDb = await _dbcontext.Category.FirstOrDefaultAsync(c => c.Id == id);
            if (CategoryInDb != null)
            {
                _dbcontext.Category.Remove(CategoryInDb);
                return (await _dbcontext.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var CategoryInDb = await _dbcontext.Category.FirstOrDefaultAsync (c=> c.Id == id);
            if (CategoryInDb == null)
            {
                return new Category();
            }
            return CategoryInDb;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbcontext.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var CategoryInDb = await _dbcontext.Category.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (CategoryInDb is not null)
            {
                CategoryInDb.Name = category.Name;
                _dbcontext.Category.Update(CategoryInDb);
                await _dbcontext.SaveChangesAsync();
                return CategoryInDb;
            }
            return category;
        }
    }
}
