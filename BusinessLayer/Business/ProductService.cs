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

        
           //@totalRow int output,
           //@index int = 1,
           //@rowNumber int = 5
        //public void GetProductsFromStoreProcedure(int rowNumber, int index)
        //{

        //    ZhenLiuOnlineDBContext products = new ZhenLiuOnlineDBContext();
        //    System.Data.Entity.Core.Objects.ObjectParameter obj = new System.Data.Entity.Core.Objects.ObjectParameter("totalRow", SqlDbType.Int);

        //    var s = products.sp_ShowProductShoppingPage(obj, index, rowNumber).ToList();
        //}

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

                SqlParameter paramOnSpecial = new SqlParameter();
                paramOnSpecial.ParameterName = "@OnSpecial";
                paramOnSpecial.Value = product.OnSpecial;
                cmd.Parameters.Add(paramOnSpecial);

                SqlParameter paramOutOfStock = new SqlParameter();
                paramOutOfStock.ParameterName = "@OutOfStock";
                paramOutOfStock.Value = product.OutOfStock;
                cmd.Parameters.Add(paramOutOfStock);

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

                SqlParameter paramOnSpecial = new SqlParameter();
                paramOnSpecial.ParameterName = "@OnSpecial";
                paramOnSpecial.Value = product.OnSpecial;
                cmd.Parameters.Add(paramOnSpecial);

                SqlParameter paramOutOfStock = new SqlParameter();
                paramOutOfStock.ParameterName = "@OutOfStock";
                paramOutOfStock.Value = product.OutOfStock;
                cmd.Parameters.Add(paramOutOfStock);

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