using eCommerce.Data;
using eCommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public OrderRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
           orderHeader.OrderDate = DateTime.Now;
           await _dbcontext.OrderHeader.AddAsync(orderHeader);
           await _dbcontext.SaveChangesAsync();
           return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                return await _dbcontext.OrderHeader.Where(o => o.UserId == userId).ToListAsync();
            }
            else
            {
                return await _dbcontext.OrderHeader.ToListAsync();
            }
        }

        public async Task<OrderHeader> GetAsync(int id) => await _dbcontext.OrderHeader.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<OrderHeader> GetOrderBySessionIdAsync(string sessionId)
        {
            return await _dbcontext.OrderHeader.FirstOrDefaultAsync(o => o.SessionId == sessionId);
        }

        public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status, string paymentIntentId)
        {
            var OrderHeaderInDb=_dbcontext.OrderHeader.FirstOrDefault(o => o.Id == orderId);
            if(OrderHeaderInDb != null)
            {
                if(!string.IsNullOrEmpty(paymentIntentId))
                {
                    OrderHeaderInDb.PaymentIntentId = paymentIntentId;
                }
                OrderHeaderInDb.Status = status;
                await _dbcontext.SaveChangesAsync();
            }
            return OrderHeaderInDb;
        }
    }
}
