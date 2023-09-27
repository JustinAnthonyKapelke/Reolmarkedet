using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Configuration;


namespace Reolmarkedet.Model
{
    public class ProductRepository
    {
        private List<Product> _products = new  List<Product>();

        public ProductRepository()         
        {
            _products = new List<Product>();
            InitializeProducts(); 
        }

        public void addProduct(Product product) 
        {
            _products.Add(product);
        }

        public void removeProduct(Product product) 
        {
            _products.Remove(product);
        }

        //public Product createProduct(string productName, double productPrice)
        //{
        //    Product product = new Product(productName, productPrice);

        //    addProduct(product);

        //    return product;
        //}


        private void InitializeProducts()
        {
            string connectionString = "din forbindelsesstreng her"; // Erstat med din database forbindelsesstreng

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseServerInstance"].ConnectionString))
            {
                connection.Open();

                string query = "SELECT ProductID, ProductName, ProductPrice, ShelfOwnerID FROM RMProduct";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int productID = (int)reader["ProductID"];
                        string productName = (string)reader["ProductName"];
                        double productPrice = (double)reader["ProductPrice"];
                        int shelfOwnerID = (int)reader["ShelfOwnerID"];

                        addProduct(new Product(productID, productName, productPrice, shelfOwnerID));
                    }
                }
            }
        }


        public Product createProduct(int productID, string productName, double productPrice, int shelfOwnerID)
        {
            Product product = new Product(productID, productName, productPrice, shelfOwnerID);

            addProduct(product);

            return product;
        }


        public List<Product> GetProducts()
        {
            return _products;
        }

        public Product GetProductByID(int productID)
        {
            Product result = null;

            foreach (Product product in _products)
            {
                if (product.ProductID == productID)
                {
                    result = product;
                    break;
                }
            }

            return result;
        }


    }
}
