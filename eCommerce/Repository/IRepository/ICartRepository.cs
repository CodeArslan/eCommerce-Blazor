using eCommerce.Data;

namespace eCommerce.Repository.IRepository
{
    public interface ICartRepository
    {
        public Task<bool> UpdateCartAsync(string UserId, int ProductId, int Count);
        public Task<IEnumerable<Cart>> GetAllAsync(string? UserId);
        public Task<bool> ClearCartAsync(string? UserId);
        public Task<int> GetTotalCartCountAsync(string? UserId);
    }
}
