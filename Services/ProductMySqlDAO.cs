using MySqlConnector;
using ProductStoreApp.Models;
using System;
using System.Collections.Generic;

namespace ProductStoreApp.Services
{
    public class ProductMySqlDAO : IProductsDataService
    {
        //local environment variables
        private static string database_server = Environment.GetEnvironmentVariable("DATABASE_SERVER_NAME");
        private static string database_userId = Environment.GetEnvironmentVariable("DATABASE_USER_ID");
        private static string database_password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        private static string database_schema = Environment.GetEnvironmentVariable("DATABASE_SCHEMA");
        private static string database_port = Environment.GetEnvironmentVariable("DATABASE_PORT");

        //MySQL database connection string.
        private static string connectionString = "server=" + database_server + ";UserId=" + database_userId + ";password=" + database_password + ";database=" + database_schema + ";port=" + database_port;

        public List<ProductModel> AllProducts()
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string sqlStatement = "SELECT * FROM products";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel((int)reader[0], (string)reader[1], (decimal)reader[2], (string)reader[3]));
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundProducts;
        }

        public bool Delete(ProductModel product)
        {
            bool isDeleted = false;

            string sqlStatement = "DELETE FROM products WHERE Id = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@id", product.Id);

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                    isDeleted = true;

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return isDeleted;
        }

        public ProductModel GetProductById(int id)
        {
            ProductModel foundProduct = null;

            string sqlStatement = "SELECT * FROM products WHERE Id = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProduct = new ProductModel((int)reader[0], (string)reader[1], (decimal)reader[2], (string)reader[3]);
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundProduct;
        }

        public int Insert(ProductModel product)
        {
            int result = -1;

            string sqlStatement = "INSERT INTO products (Name, Price, Description) VALUES (@name, @price, @description); SELECT LAST_ID_INSERTED();";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@description", product.Description);

                try
                {
                    connection.Open();
                    var affectedRow = command.ExecuteScalar();

                    if (affectedRow != null)
                    {
                        //special case for BIGINT in MySQL
                        if(affectedRow.GetType() == typeof(ulong))
                            result = Convert.ToInt32(affectedRow);
                        else
                            result = (int)affectedRow;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }

        public List<ProductModel> SearchProducts(string searchTerm)
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string sqlStatement = "SELECT * FROM products WHERE Name LIKE @Name";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel((int)reader[0], (string)reader[1], (decimal)reader[2], (string)reader[3]));
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundProducts;
        }

        public int Update(ProductModel product)
        {
            int result = -1;

            string sqlStatement = "UPDATE products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);

                try
                {
                    connection.Open();

                    result = Convert.ToInt32(command.ExecuteScalar());

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}
