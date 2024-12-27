using eCommerce.Data;
using eCommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public CartRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> ClearCartAsync(string? UserId)
        {
            var CartItems = await _dbcontext.Cart.Where(u=>u.UserId == UserId).ToListAsync();
            _dbcontext.Cart.RemoveRange(CartItems);
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync(string? UserId)
        {
            return await _dbcontext.Cart.Where(u => u.UserId == UserId).Include(p=>p.Product).ToListAsync();
        }

        async Task<bool> ICartRepository.UpdateCartAsync(string UserId, int ProductId, int Count)
        {
            if(string.IsNullOrWhiteSpace(UserId))
            {
                return false;
            }
            var CartInDb = await _dbcontext.Cart.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (CartInDb == null) {
                var Cart = new Cart
                {
                    UserId = UserId,
                    ProductId = ProductId,
                    Count = Count
                };
                await _dbcontext.Cart.AddAsync(Cart);
            }
            else
            {
                CartInDb.ProductId = ProductId;
                CartInDb.Count += Count;
                if(CartInDb.Count <= 0)
                {
                    _dbcontext.Cart.Remove(CartInDb);
                }
            }
            return await _dbcontext.SaveChangesAsync() > 0;
        }
    }
}
