using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using OnlineShoppingWeb.Models;
using System.Configuration;
using System.Data;


namespace OnlineShoppingWeb.Business
{
    public class CustomerService
    {
        public void CreateNewCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
                conn.Open();
                SqlCommand sql = new SqlCommand("insert into customers (UserName,UserPass,Age,Mobile,Email,LastName,FirstName,Address) values(@UserName,@UserPass,@Age,@Mobile,@Email,@LastName,@FirstName,@Address)", conn);
                sql.Parameters.Add(new SqlParameter("UserName", customer.UserName));
                sql.Parameters.Add(new SqlParameter("UserPass", customer.UserPass));
                sql.Parameters.Add(new SqlParameter("Age", customer.Age));
                sql.Parameters.Add(new SqlParameter("Mobile", customer.Mobile));
                sql.Parameters.Add(new SqlParameter("Email", customer.Email));
                sql.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                sql.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                sql.Parameters.Add(new SqlParameter("Address", customer.Address));
                sql.ExecuteNonQuery();
            }
        }

        public List<Customer> GetCustomers()
        {
            //List<Customer> customerList = new List<Customer>();
            //using (SqlConnection conn = new SqlConnection())
            //{
            //    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Home"].ConnectionString;
            //    conn.Open();
            //    SqlCommand sql = new SqlCommand("select * from customers", conn);
            //    using (SqlDataReader reader = sql.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            var customer = new Customer
            //            {
            //                CustomerID = (int)reader["CustomerID"],
            //                LastName = reader["LastName"].ToString(),
            //                FirstName = reader["FirstName"].ToString(),
            //                UserName = reader["UserName"].ToString(),
            //                Password = reader["UserPass"].ToString(),
            //                Age = (int)reader["Age"],
            //                MobileNumber = reader["Mobile"].ToString(),
            //                Email = reader["Email"].ToString(),
            //                Address = reader["Address"].ToString()
            //            };
            //            customerList.Add(customer);
            //        }
            //    }

            //}
            
            CustomerContext customers = new CustomerContext();
            var customerList = customers.Customers.ToList();
            return customerList;
        }

        public Customer GetCustomerById(int customerID)
        {
            //Customer customer = null;
            //using (SqlConnection conn = new SqlConnection())
            //{
            //    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Home"].ConnectionString;
            //    conn.Open();
            //    SqlCommand sql = new SqlCommand("select * from customers where customerID = @ID",conn);
            //    sql.Parameters.Add(new SqlParameter("ID",customerID));
            //    using (SqlDataReader reader = sql.ExecuteReader())
            //    {
            //        if (reader.HasRows)
            //        {
            //            reader.Read();
            //            customer = new Customer
            //            {
            //                CustomerID = customerID,
            //                LastName = reader["LastName"].ToString(),
            //                FirstName = reader["FirstName"].ToString(),
            //                UserName = reader["UserName"].ToString(),
            //                UserPass = reader["UserPass"].ToString(),
            //                Age = (int)reader["Age"],
            //                Mobile = reader["Mobile"].ToString(),
            //                Email = reader["Email"].ToString(),
            //                Address = reader["Address"].ToString()
            //            };
            //        }
            //    }
            //}
            CustomerContext customers = new CustomerContext();
            Customer customer = customers.Customers.Single(cus => cus.CustomerID == customerID);
            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
                conn.Open();
                SqlCommand sql = new SqlCommand("Update customers set UserPass=@UserPass,Age=@Age,Mobile=@Mobile,Email=@Email,LastName=@LastName,FirstName=@FirstName,Address=@Address where customerID = @CustomerID", conn);
                sql.Parameters.Add(new SqlParameter("UserName", customer.UserName));
                sql.Parameters.Add(new SqlParameter("CustomerID", customer.CustomerID));
                sql.Parameters.Add(new SqlParameter("UserPass", customer.UserPass));
                sql.Parameters.Add(new SqlParameter("Age", customer.Age));
                sql.Parameters.Add(new SqlParameter("Mobile", customer.Mobile));
                sql.Parameters.Add(new SqlParameter("Email", customer.Email));
                sql.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                sql.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                sql.Parameters.Add(new SqlParameter("Address", customer.Address));
                sql.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@CustomerID";
                paramId.Value = customer.CustomerID;
                cmd.Parameters.Add(paramId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}