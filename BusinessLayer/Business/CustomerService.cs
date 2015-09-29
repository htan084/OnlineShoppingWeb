using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BusinessLayer;


namespace OnlineShoppingWeb.Business
{
    public class CustomerService
    {
        public void CreateNewCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ZhenLiuOnlineDBContext"].ConnectionString;
                conn.Open();
                SqlCommand sql = new SqlCommand("insert into customers (UserName,UserPass,Mobile,Email,LastName,FirstName,Address,DateOfBirth) values(@UserName,@UserPass,@Mobile,@Email,@LastName,@FirstName,@Address,@DateOfBirth)", conn);
                sql.Parameters.Add(new SqlParameter("UserName", customer.UserName));
                sql.Parameters.Add(new SqlParameter("UserPass", customer.UserPass));
                sql.Parameters.Add(new SqlParameter("Mobile", customer.Mobile));
                sql.Parameters.Add(new SqlParameter("Email", customer.Email));
                sql.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                sql.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                sql.Parameters.Add(new SqlParameter("Address", customer.Address));
                sql.Parameters.Add(new SqlParameter("DateOfBirth", customer.DateOfBirth));

                sql.ExecuteNonQuery();
            }
        }

        public List<Customer> GetCustomers()
        {

            ZhenLiuOnlineDBContext customers = new ZhenLiuOnlineDBContext();
            var customerList = customers.Customers.ToList();
            return customerList;
        }

        public Customer GetCustomerById(int customerID)
        {
            ZhenLiuOnlineDBContext customers = new ZhenLiuOnlineDBContext();
            Customer customer = customers.Customers.Single(cus => cus.CustomerId == customerID);
            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ZhenLiuOnlineDBContext"].ConnectionString;
                conn.Open();
                SqlCommand sql = new SqlCommand("Update customers set UserPass=@UserPass,Mobile=@Mobile,Email=@Email,LastName=@LastName,FirstName=@FirstName,Address=@Address,DateOfBirth = @DateOfBirth where customerID = @CustomerID", conn);
                sql.Parameters.Add(new SqlParameter("UserName", customer.UserName));
                sql.Parameters.Add(new SqlParameter("CustomerID", customer.CustomerId));
                sql.Parameters.Add(new SqlParameter("UserPass", customer.UserPass));
                sql.Parameters.Add(new SqlParameter("Mobile", customer.Mobile));
                sql.Parameters.Add(new SqlParameter("Email", customer.Email));
                sql.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                sql.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                sql.Parameters.Add(new SqlParameter("Address", customer.Address));
                sql.Parameters.Add(new SqlParameter("DateOfBirth", customer.DateOfBirth));

                sql.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ZhenLiuOnlineDBContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@CustomerID";
                paramId.Value = customer.CustomerId;
                cmd.Parameters.Add(paramId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}