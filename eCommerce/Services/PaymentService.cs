using eCommerce.Data;
using eCommerce.Repository.IRepository;
using eCommerce.Utility;
using Microsoft.AspNetCore.Components;
using Stripe.Checkout;

namespace eCommerce.Services
{
    public class PaymentService
    {
        private readonly NavigationManager _navigationManager;
        private IOrderRepository _orderRepository;
        public PaymentService(NavigationManager navigationManager, IOrderRepository orderRepository)
        {
            _navigationManager = navigationManager;
            _orderRepository = orderRepository;
        }
        public Session CreateStripeCheckoutSession(OrderHeader orderHeader)
        {
            var lineItems = orderHeader.OrderDetails.Select(o => new Stripe.Checkout.SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = o.Product.Name,
                    },
                    UnitAmountDecimal = (decimal?)(o.Price * 100),
                },
                Quantity = o.Count,
            }).ToList();
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = $"{_navigationManager.BaseUri}Order/Confirmation/{{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{_navigationManager.BaseUri}Cart",
                LineItems = lineItems,
                Mode = "payment",
                
            };
            var service = new Stripe.Checkout.SessionService();
            var session=service.Create(options);
            return session;
        }
        public async Task<OrderHeader> CheckPaymentStatusandUpdate(string sessionId)
        {
            OrderHeader orderHeader = await _orderRepository.GetOrderBySessionIdAsync(sessionId);
            var service = new SessionService();
            var session = service.Get(sessionId);
            if(session.PaymentStatus.ToLower()=="paid")
            if (orderHeader != null)
            {
                await _orderRepository.UpdateStatusAsync(orderHeader.Id, StaticDetails.StatusApproved, session.PaymentIntentId);
            }
            return orderHeader;
        }
    }
}
