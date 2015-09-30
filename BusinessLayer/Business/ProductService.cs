using BusinessLayer;
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
            ZhenLiuOnlineDBContext products = new ZhenLiuOnlineDBContext();
            var productList = products.Products.ToList();
            return productList;
        }

        public void SaveProduct(Product product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ZhenLiuOnlineDBContext"].ConnectionString;
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

        public void CreateProduct(Product product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ZhenLiuOnlineDBContext"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCreateProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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
            string connectionString = ConfigurationManager.ConnectionStrings["ZhenLiuOnlineDBContext"].ConnectionString;
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