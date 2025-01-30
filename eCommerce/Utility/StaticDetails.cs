using eCommerce.Data;

namespace eCommerce.Utility
{
    public static class StaticDetails
    {
        public static string Role_Admin="Admin";
        public static string Role_Customer="Customer";
        
        public static string StatusPending = "Pending";
        public static string StatusApproved = "Approved";
        public static string StatusReadyForPickup = "ReadyForPickup";
        public static string StatusCompleted = "Completed";
        public static string StatusCancelled = "Cancelled";


        public static List<OrderDetails> ConvertShoppinCartListToOrderDetailList(List<eCommerce.Data.Cart> shoppingCartList)
        {
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            foreach (var cart in shoppingCartList)
            {
                OrderDetails orderDetails = new OrderDetails
                {
                    ProductId = cart.ProductId,
                    Product = cart.Product,
                    ProductName=cart.Product.Name,
                    Price = Convert.ToDouble(cart.Product.Price),
                    Count = cart.Count
                };
                orderDetailsList.Add(orderDetails);
            }
            return orderDetailsList;
        }
    }
}
