using System;
using System.Collections.Generic;
using System.Configuration;
using ConsoleApplication1.Classes;

namespace ConsoleApplication1
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var orderManager = new Manager();

            var newProduct = new Product
            {
                Id = 1,
                Name = "Sample",
                Description = "A sample product",
                Weight = 2.5,
                Height = 10,
                Width = 8,
                Length = 15
            };

            orderManager.CreateProduct(newProduct);

            var retrievedProduct = orderManager.GetProductById(1);
            if (retrievedProduct != null)
            {
                Console.WriteLine("Retrieved Product: " + retrievedProduct.Name);
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            if (retrievedProduct != null)
            {
                retrievedProduct.Name = "Updated";
                retrievedProduct.Description = "Updated product description";
                orderManager.UpdateProduct(retrievedProduct);
            }

            orderManager.DeleteProduct(1);

            Console.WriteLine("Product operations completed.");

            var newOrder = new Order
            {
                ProductId = 1,
                Status = 0
            };
            orderManager.CreateOrder(newOrder);

            var retrievedOrder = orderManager.GetOrderById(1);
            if (retrievedOrder != null)
            {
                Console.WriteLine("Retrieved Order Status: " + retrievedOrder.Status);
            }
            else
            {
                Console.WriteLine("Order not found.");
            }

            if (retrievedOrder != null)
            {
                retrievedOrder.Status = 0;
                orderManager.UpdateOrder(retrievedOrder);
            }

            orderManager.DeleteOrder(1);

            Console.WriteLine("Order operations completed.");
            
            List<Product> allProducts = orderManager.GetAllProducts();
            Console.WriteLine("All Products:");
            foreach (var product in allProducts)
            {
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}");
            }

            List<Order> filteredOrders = orderManager.GetOrdersByFilter(filterYear: 2023, filterMonth: 8, filterStatus: "InProgress", filterProductId: 1);
            Console.WriteLine("Filtered Orders:");
            foreach (var order in filteredOrders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Status: {order.Status}");
            }

            var ordersToDelete = new List<int> { 1, 2, 3 };
            orderManager.BulkDeleteOrders(ordersToDelete);
            Console.WriteLine("Orders deleted successfully.");

            Console.ReadLine();
        }
    }
}