using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Reolmarkedet.Model
{
    public class ShelfOwnerRepository
    {
        private List<ShelfOwner> _shelfOwners;

        public ShelfOwnerRepository()
        {
            _shelfOwners = new List<ShelfOwner>();
            InitializeShelfOwners();
        }

        public void addShelfOwner(ShelfOwner shelfOwner)
        {
            _shelfOwners.Add(shelfOwner);
        }

        public void removeShelfOwner(ShelfOwner shelfOwner)
        {
            _shelfOwners.Remove(shelfOwner);
        }

        public void createShelfOwner(int shelfOwnerID, string shelfOwnerName, double ShelfOwnerBalance) 
        {
            ShelfOwner shelfOwner = new ShelfOwner(shelfOwnerID, shelfOwnerName, ShelfOwnerBalance);
            addShelfOwner(shelfOwner);
        }


        public List<ShelfOwner> GetShelfOwners()
        {
            return _shelfOwners;
        }

        public ShelfOwner GetShelfOwnerByID(int shelfOwnerID)
        {
            ShelfOwner result = null;

            foreach (ShelfOwner shelfOwner in _shelfOwners)
            {
                if (shelfOwner.ShelfOwnerID == shelfOwnerID)
                {
                    result = shelfOwner;
                    break;
                }
            }

            return result;
        }

        private void InitializeShelfOwners()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseServerInstance"].ConnectionString))
            {
                connection.Open();

                string query = "SELECT ShelfOwnerID, ShelfOwnerName, ShelfOwnerBalance FROM RMShelfOwner";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int shelfOwnerID = (int)reader["ShelfOwnerID"];
                        string shelfOwnerName = (string)reader["ShelfOwnerName"];
                        double shelfOwnerBalance = (double)reader["ShelfOwnerBalance"];
                        addShelfOwner(new ShelfOwner(shelfOwnerID, shelfOwnerName, shelfOwnerBalance));
                    }
                }
            }
        }

        public void InsertIntoShelfOwnerBalance(int shelfOwnerID, double shelfOwnerBalance)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseServerInstance"].ConnectionString))
            {
                connection.Open();

                string query = "UPDATE RMShelfOwner SET ShelfOwnerBalance = ShelfOwnerBalance + @NewBalance WHERE ShelfOwnerID = @ShelfOwnerID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewBalance", shelfOwnerBalance);
                    command.Parameters.AddWithValue("@ShelfOwnerID", shelfOwnerID);
                    command.ExecuteNonQuery();
                }
            }
        }



        // New method to get actual shelfowner balance
        public double GetShelfOwnerBalanceFromDB(int shelfOwnerID)
        {
            double balance = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseServerInstance"].ConnectionString))
            {
                connection.Open();

                string query = "SELECT ShelfOwnerBalance FROM RMShelfOwner WHERE ShelfOwnerID = @ShelfOwnerID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ShelfOwnerID", shelfOwnerID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {                
                        balance = (double)reader["ShelfOwnerBalance"];
                        }
                      
                    }
                }
            }

            return balance;
        }


    }
}
