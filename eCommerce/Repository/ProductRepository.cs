using eCommerce.Data;
using eCommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace eCommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = dbcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _dbcontext.Product.AddAsync(product);
            await _dbcontext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ProductInDb = await _dbcontext.Product.FirstOrDefaultAsync(c => c.Id == id);
            var ImageUrl=Path.Combine(_webHostEnvironment.WebRootPath,ProductInDb.ImageUrl.TrimStart('/'));
            if (File.Exists(ImageUrl))
            {
                File.Delete(ImageUrl);
            }
            if (ProductInDb != null)
            {
                _dbcontext.Product.Remove(ProductInDb);
                return (await _dbcontext.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var ProductInDb = await _dbcontext.Product.Include(c=>c.Category).FirstOrDefaultAsync(c=> c.Id == id);
            if (ProductInDb == null)
            {
                return new Product();
            }
            return ProductInDb;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbcontext.Product.Include(c => c.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var ProductInDB = await _dbcontext.Product.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (ProductInDB is not null)
            {
                ProductInDB.Name = product.Name;
                ProductInDB.Price = product.Price;
                ProductInDB.Description = product.Description;
                ProductInDB.CategoryId = product.CategoryId;
                //ProductInDB.SpecialTag = product.SpecialTag;
                ProductInDB.ImageUrl = product.ImageUrl;
                _dbcontext.Product.Update(ProductInDB);
                await _dbcontext.SaveChangesAsync();
                return ProductInDB;
            }
            return product;
        }
    }
}
