using System.Configuration;
using System.Data.SqlClient;
using System;


namespace Debtor
{
    public class Database
    {
        private string DatabaseName { get; set; } = "People";

        private SqlConnection GetSqlConnection()
        {
            string ConString = ConfigurationManager.ConnectionStrings["PeopleConn"].ConnectionString;
            SqlConnection con = new SqlConnection(ConString);

            return con;
        }

        public Database()
        {
            CreateTable();
        }

        public void CreateTable()
        {
            SqlConnection con = GetSqlConnection();

            string query =
            @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='People' and xtype='U')
                CREATE TABLE dbo.People
            (
            ID int IDENTITY(1,1) NOT NULL,
            Name nvarchar(50) NOT NULL,
            Surname nvarchar(50) NOT NULL,
            Address nvarchar(50) NOT NULL,
            Amount DECIMAL NOT NULL
            CONSTRAINT pk_id PRIMARY KEY (ID)
            );";

            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void AddBorower(string name, string surname,string address,decimal amount)
        {
            SqlConnection con = GetSqlConnection();
            string query = "INSERT INTO " + DatabaseName + " (Name, Surname,Address,Amount) Values('" + name + "','" + surname + "','" + address + "','" + amount + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n\tUdało się dodać dłużnika\n");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteBorrower(int id)
        {
            SqlConnection con = GetSqlConnection();

            string query = "DELETE FROM " + DatabaseName + "    WHERE ID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n\tDłużnik został usunięty\n");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateBorrowerData(int id, string type, string value)
        {
            SqlConnection con = GetSqlConnection();

            string query = "UPDATE " + DatabaseName + " SET " + type + " = '" + value + "' WHERE ID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n\tDane został zaaktualizowane\n");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public void UpdateBorrowedMoney(int id, decimal amount)
        {
            SqlConnection con = GetSqlConnection();

            string query = "UPDATE " + DatabaseName + " SET Amount = '" + amount + "' WHERE ID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n\tDług został zaaktualizowany\n");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public void ListAllBorrowers()
        {
            SqlConnection con = GetSqlConnection();

            string querystring = "Select * from " + DatabaseName;
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("\n  ID\t|\tImie\t|\tNazwisko\t|\tAdres\t\t|\tKwota");

            while (reader.Read())
            {
                Console.WriteLine("  " + reader[0].ToString() + "\t|\t" + reader[1].ToString() + "\t|\t" + reader[2].ToString() + "\t\t|\t" + reader[3].ToString() + "\t|\t" + reader[4].ToString() + " zł");
            }
        }
    }
}
