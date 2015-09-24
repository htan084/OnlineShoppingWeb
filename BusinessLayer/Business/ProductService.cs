using OnlineShoppingWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.Business
{
    public class ProductService
    {
        public List<Product> GetProducts()
        {
            //var productList = new List<Product>();
            //using (SqlConnection conn = new SqlConnection())
            //{
            //    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            //    conn.Open();
            //    SqlCommand sql = new SqlCommand("select * from products", conn);
            //    using (SqlDataReader reader = sql.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            var product = new Product
            //            {
            //                Id = Convert.ToInt32(reader["Id"]),
            //                Name = reader["Name"].ToString(),
            //                Price = Convert.ToDecimal(reader["Price"]),
            //                Url = reader["Url"].ToString()
            //            };
            //            productList.Add(product);
            //        }
            //    }
            //}
            CustomerContext products = new CustomerContext();
            var productList = products.Products.ToList();
            return productList;
        }

        public void SaveProduct(Product product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = product.Id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = product.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramPrice = new SqlParameter();
                paramPrice.ParameterName = "@Price";
                paramPrice.Value = product.Price;
                cmd.Parameters.Add(paramPrice);

                SqlParameter paramUrl = new SqlParameter();
                paramUrl.ParameterName = "@Url";
                paramUrl.Value = product.Url;
                cmd.Parameters.Add(paramUrl);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(Product product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = product.Id;
                cmd.Parameters.Add(paramId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }


}