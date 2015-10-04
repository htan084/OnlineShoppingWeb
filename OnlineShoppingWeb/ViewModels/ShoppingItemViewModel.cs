using OnlineShoppingWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingWeb.ViewModels
{
    public class ShoppingItemViewModel
    {
        private ProductViewModel product = new ProductViewModel();



        public ProductViewModel Product
        {
            get { return product; }
            set { product = value; }
        }


        public int quantity{get;set;}

        public ShoppingItemViewModel()
        {

        }

        public ShoppingItemViewModel(ProductViewModel product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }
    }
}
