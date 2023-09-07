using System.Configuration;
using Npgsql;

namespace ConsoleApplication1.Classes
{
    
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderManager
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly List<Order> _orders = new List<Order>();
        private string _connectionString;

        public OrderManager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString;
        }
        
        public void CreateProduct(Product product)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO public.\"Product\"" +
                                  $"(\"Name\", \"Description\", \"Weight\", \"Height\", \"Width\", \"Length\") " +
                                  $"VALUES ('{product.Name}','{product.Description}', {product.Weight}, {product.Weight}, {product.Height}, {product.Length});";
                
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Product GetProductById(int productId)
        {
            var result = new Product();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM public.\"Product\" WHERE \"Id\"='{productId}'";
                var reader = cmd.ExecuteReader();
                
                if (!reader.Read())
                {
                    return null;
                }
                
                result.Id = (int)reader["Id"];
                result.Name = (string)reader["Name"];
                result.Description = (string)reader["Description"];
                result.Weight = (double)reader["Weight"];
                result.Width = (double)reader["Width"];
                result.Height = (double)reader["Height"];
                result.Length = (double)reader["Length"];
                
                connection.Close();
            }
            return result;
        }

        public void UpdateProduct(Product updatedProduct)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"UPDATE public.\"Product\" " +
                                  $"SET \"Name\"='{updatedProduct.Name}', " +
                                  $"\"Description\"='{updatedProduct.Description}', " +
                                  $"\"Weight\"={updatedProduct.Weight}, " +
                                  $"\"Width\"={updatedProduct.Width}, " +
                                  $"\"Height\"={updatedProduct.Height}, " +
                                  $"\"Length\"={updatedProduct.Length} " +
                                  $"WHERE \"Id\"={updatedProduct.Id}";
                
                    
                cmd.ExecuteNonQuery();
                
                connection.Close();
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM public.\"Product\" WHERE \"Id\"={productId}";

                cmd.ExecuteNonQuery();
                
                connection.Close();
            }        
        }

        public void CreateOrder(Order order)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO public.\"Order\"" +
                                  $"(\"CreateDate\", \"UpdatedDate\", \"ProductId\", \"Status\") " +
                                  $"VALUES ('{order.CreateDate}','{order.UpdatedDate}', {order.ProductId}, {order.Status});";
                
                cmd.ExecuteNonQuery();
                connection.Close();
            }    
        }

        public Order GetOrderById(int orderId)
        {
            var result = new Order();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM public.\"Order\" WHERE \"Id\"='{orderId}'";
                var reader = cmd.ExecuteReader();
                
                if (!reader.Read())
                {
                    return null;
                }
                
                result.Id = (int)reader["Id"];
                result.CreateDate = (DateTime)reader["CreateDate"];
                result.UpdatedDate = (DateTime)reader["UpdatedDate"];
                result.ProductId = (int)reader["ProductId"];
                result.Status = (int)reader["Status"];
                
                connection.Close();
            }
            return result;        }

        public void UpdateOrder(Order updatedOrder)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"UPDATE public.\"Order\" " +
                                  $"SET \"CreateDate\"='{updatedOrder.CreateDate}', " +
                                  $"\"UpdatedDate\"='{updatedOrder.UpdatedDate}', " +
                                  $"\"ProductId\"={updatedOrder.ProductId}, " +
                                  $"\"Status\"={updatedOrder.Status}";
                
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM public.\"Order\" WHERE \"Id\"={orderId}";

                cmd.ExecuteNonQuery();
                connection.Close();
            }         
        }

        public List<Product> GetAllProducts()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM public.\"Product\"";
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Description = (string)reader["Description"],
                            Weight = (double)reader["Weight"],
                            Width = (double)reader["Width"],
                            Height = (double)reader["Height"],
                            Length = (double)reader["Length"]
                        };

                        _products.Add(product);
                    }
                }
                
                connection.Close();
                return _products;
            }
        }

        public List<Order> GetOrdersByFilter(int filterYear, int filterMonth, string filterStatus, int filterProductId)
        {
            var filteredOrders = _orders;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("GetFilteredOrders", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("filter_year", filterYear);
                    command.Parameters.AddWithValue("filter_month", filterMonth);
                    command.Parameters.AddWithValue("filter_status", filterStatus);
                    command.Parameters.AddWithValue("filter_product_id", filterProductId);

                    command.Parameters.Add(new NpgsqlParameter("result_set", NpgsqlTypes.NpgsqlDbType.Refcursor)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    });
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {
                                Id = reader.GetInt32(0),
                                CreateDate = reader.GetDateTime(1),
                                UpdatedDate = reader.GetDateTime(2),
                                ProductId = reader.GetInt32(3),
                                Status = reader.GetInt16(4)
                            };
                            filteredOrders.Add(order);
                        }
                    }

                    return filteredOrders;
                }
            }
        }

        public void BulkDeleteOrders(List<int> orderIds)
        {
            if (orderIds == null || orderIds.Count == 0)
            {
                return;
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM public.\"Order\" WHERE \"Id\" = ANY(@orderIds)";

                cmd.Parameters.AddWithValue("@orderIds", orderIds.ToArray());

                cmd.ExecuteNonQuery();

                connection.Close();
            }        
        }    
    }
}