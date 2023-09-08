using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Npgsql;
using Dapper;

namespace ConsoleApplication1.Classes
{
    public class Manager
    {
        private readonly string _connectionString;

        public Manager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString;
        }

        public void CreateProduct(Product product)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"INSERT INTO public.""Product""
                              (""Name"", ""Description"", ""Weight"", ""Height"", ""Width"", ""Length"")
                              VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";
                dbConnection.Execute(sql, product);
                dbConnection.Close();
            }
        }

        public Product GetProductById(int productId)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"SELECT * FROM public.""Product"" WHERE ""Id"" = @ProductId";
                return dbConnection.QueryFirstOrDefault<Product>(sql, new { ProductId = productId });
            }
        }

        public void UpdateProduct(Product updatedProduct)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"UPDATE public.""Product""
                              SET ""Name"" = @Name, 
                                  ""Description"" = @Description, 
                                  ""Weight"" = @Weight, 
                                  ""Width"" = @Width, 
                                  ""Height"" = @Height, 
                                  ""Length"" = @Length
                              WHERE ""Id"" = @Id";
                dbConnection.Execute(sql, updatedProduct);
                dbConnection.Close();

            }
        }

        public void DeleteProduct(int productId)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"DELETE FROM public.""Product"" WHERE ""Id"" = @ProductId";
                dbConnection.Execute(sql, new { ProductId = productId });
                dbConnection.Close();
            }
        }

        public void CreateOrder(Order order)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"INSERT INTO public.""Order""
                              (""CreateDate"", ""UpdatedDate"", ""ProductId"", ""Status"")
                              VALUES (@CreateDate, @UpdatedDate, @ProductId, @Status)";
                dbConnection.Execute(sql, order);
                dbConnection.Close();
            }
        }

        public Order GetOrderById(int orderId)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"SELECT * FROM public.""Order"" WHERE ""Id"" = @OrderId";
                return dbConnection.QueryFirstOrDefault<Order>(sql, new { OrderId = orderId });
            }
        }

        public void UpdateOrder(Order updatedOrder)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"UPDATE public.""Order""
                              SET ""CreateDate"" = @CreateDate,
                                  ""UpdatedDate"" = @UpdatedDate,
                                  ""ProductId"" = @ProductId,
                                  ""Status"" = @Status
                              WHERE ""Id"" = @Id";
                dbConnection.Execute(sql, updatedOrder);
                dbConnection.Close();
            }
        }

        public void DeleteOrder(int orderId, OrderStatusEnum? filterStatus = null)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"DELETE FROM public.""Order"" WHERE ""Id"" = @OrderId";
                if (filterStatus.HasValue)
                {
                    sql += @" AND ""Status"" = @FilterStatus";
                }
                dbConnection.Execute(sql, new { OrderId = orderId, FilterStatus = (int?)filterStatus });
                dbConnection.Close();
            }
        }

        public List<Product> GetAllProducts()
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"SELECT * FROM public.""Product""";
                return dbConnection.Query<Product>(sql).ToList();
            }
        }

        public List<Order> GetOrdersByFilter(int? createDate = null, int? updatedDate = null, OrderStatusEnum? filterStatus = null, int? filterProductId = null)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                var parameters = new
                {
                    create_date = createDate,
                    updated_date = updatedDate,
                    filter_status = (int?)filterStatus,
                    filter_product_id = filterProductId
                };
                string sql = @"SELECT * FROM public.getfilteredorders(@create_date, @updated_date, @filter_status, @filter_product_id)";
                return dbConnection.Query<Order>(sql, parameters).ToList();
            }
        }

        public void BulkDeleteOrders(List<int> orderIds, int? createDate = null, int? updatedDate = null, OrderStatusEnum? filterStatus = null, int? filterProductId = null)
        {
            if (orderIds == null || orderIds.Count == 0)
            {
                return;
            }

            using (IDbConnection dbConnection = new NpgsqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sql = @"DELETE FROM public.""Order"" WHERE ""Id"" = ANY(@OrderIds)";
                if (createDate.HasValue)
                {
                    sql += " AND \"CreateDate\" = @CreateDate";
                }
                if (updatedDate.HasValue)
                {
                    sql += " AND \"UpdatedDate\" = @UpdatedDate";
                }
                if (filterStatus.HasValue)
                {
                    sql += " AND \"Status\" = @FilterStatus";
                }
                if (filterProductId.HasValue)
                {
                    sql += " AND \"ProductId\" = @FilterProductId";
                }
                dbConnection.Execute(sql, new { OrderIds = orderIds, CreateDate = createDate, UpdatedDate = updatedDate, FilterStatus = (int?)filterStatus, FilterProductId = filterProductId });
                dbConnection.Close();
            }
        }
    }
}
