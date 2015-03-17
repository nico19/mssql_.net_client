using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection myConnection = new SqlConnection("user id=nico;" +
                                       "password=nicopass;server=NICO\\SQLEXPRESS;" +
                                       "database=temp;");
            //OPEN CONNECTION
            myConnection.Open();

            //CREATE TABLE STATEMENT
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = myConnection;
            comm.CommandText =    " CREATE TABLE Persons " +
                                    " ( " +
                                        " ID int IDENTITY(1,1) PRIMARY KEY, " + 
                                        " Name varchar(255) NOT NULL, " + 
                                        " City varchar(255) " + 
                                    " ) ";
                        
            comm.ExecuteNonQuery();

            //-------------------------------------------------------
            //INSERT STATEMENT
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = myConnection;
            comm.Parameters.Add("@Name", SqlDbType.NVarChar).Value = "NICO";
            comm.Parameters.Add("@City", SqlDbType.NVarChar).Value = "Ternopil";

            comm.CommandText = "INSERT INTO Persons (Name, City) VALUES (@Name, @City);";
            comm.ExecuteNonQuery();

            //------------------------------------------------------
            //UPDATE STATEMENT
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = myConnection;
            comm.Parameters.Add("@Name", SqlDbType.NVarChar).Value = "NICO";
            comm.Parameters.Add("@NewName", SqlDbType.NVarChar).Value = "NICONEW";
            comm.CommandText = "UPDATE Persons SET Name = @NewName WHERE Name =  @Name;";
            comm.ExecuteNonQuery();

            //------------------------------------------------------
            //SELECT STATEMENT
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = myConnection;
            comm.Parameters.Add("@City", SqlDbType.NVarChar).Value = "Ternopil";
            comm.CommandText = "SELECT Name FROM Persons WHERE City = @City;";
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }
            reader.Close();

            //-----------------------------------------------------
            //DELETE STATEMENT
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = myConnection;
            comm.Parameters.Add("@City", SqlDbType.NVarChar).Value = "Ternopil";
            comm.CommandText = "DELETE FROM Persons WHERE City = @City;";
            comm.ExecuteNonQuery();

            comm.CommandText = "DROP TABLE Persons;";
            comm.ExecuteNonQuery();
            
            Console.ReadLine();
            
            //CLOSE CONNECTION
            myConnection.Close();
        }
    }
}
