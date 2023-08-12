namespace ConsoleApplication1.Classes
{
    
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderManager
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly List<Order> _orders = new List<Order>();

        public void CreateProduct(Product product)
        {
            _products.Add(product);
        }

        public Product GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Weight = updatedProduct.Weight;
                existingProduct.Height = updatedProduct.Height;
                existingProduct.Width = updatedProduct.Width;
                existingProduct.Length = updatedProduct.Length;
            }
        }

        public void DeleteProduct(int productId)
        {
            _products.RemoveAll(p => p.Id == productId);
        }

        public void CreateOrder(Order order)
        {
            int newOrderId = _orders.Count + 1;

            order.Id = newOrderId;

            order.CreateDate = DateTime.Now;

            _orders.Add(order);        }

        public Order GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == updatedOrder.Id);
            if (existingOrder != null)
            {
                existingOrder.CreateDate = updatedOrder.CreateDate;
                existingOrder.UpdatedDate = DateTime.Now;
                existingOrder.ProductId = updatedOrder.ProductId;
                existingOrder.Status = updatedOrder.Status;
            }
        }

        public void DeleteOrder(int orderId)
        {
            _orders.RemoveAll(o => o.Id == orderId);
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public List<Order> GetOrdersByFilter(int productId, string status)
        {
            var filteredOrders = _orders;

            if (productId != 0)
            {
                filteredOrders = filteredOrders.Where(o => o.ProductId == productId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                filteredOrders = filteredOrders.Where(o => o.Status == status).ToList();
            }

            return filteredOrders;
        }

        public void BulkDeleteOrders(List<int> orderIds)
        {
            _orders.RemoveAll(o => orderIds.Contains(o.Id));
        }    }
}