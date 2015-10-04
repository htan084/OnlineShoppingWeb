using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using OnlineShoppingWeb.ViewModels;
namespace OnlineShoppingWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        
        private ZhenLiuOnlineDBContext db = new ZhenLiuOnlineDBContext();
        public ActionResult ViewCart()
        {
            List<ShoppingItemViewModel> cart = (List<ShoppingItemViewModel>)Session["Cart"];
            if (cart != null)
            {
                return View(cart);
            }
            else
            {
                return View(new List<ShoppingItemViewModel>());
            }
        }

        private int IsExisting(int id)
        {
            List<ShoppingItemViewModel> cart = (List<ShoppingItemViewModel>)Session["Cart"];

            for (int i = 0; i < cart.Count(); i++)
            {
                if (cart[i].Product.Id == id)
                {
                    return i;
                }               
            }
            return -1;
        }
        public ActionResult AddToCart(int id)
        {
            var productViewModel = ConvertToViewModelFromProduct(db.Products.Find(id));
            int quantity = Convert.ToInt32(Request["Quantity"]);
            if (Session["Cart"] == null)
            {
                List<ShoppingItemViewModel> cart = new List<ShoppingItemViewModel>();
                cart.Add(new ShoppingItemViewModel(productViewModel, quantity));
                Session["Cart"] = cart;
            }
            else
            {
                List<ShoppingItemViewModel> cart = (List<ShoppingItemViewModel>)Session["Cart"];
                int index = IsExisting(id);
                if (index == -1)
                {
                    cart.Add(new ShoppingItemViewModel(productViewModel, quantity));
                }
                else
                {
                    cart[index].quantity+= quantity;
                }
                Session["Cart"] = cart;
            }
            return RedirectToAction("ShowProductShoppingPage", "Product");
        }

        public ActionResult Delete(int id)
        {
            int index = IsExisting(id);
            List<ShoppingItemViewModel> cart = (List<ShoppingItemViewModel>)Session["Cart"];
            cart.RemoveAt(index);
            Session["Cart"] = cart;
            return RedirectToAction("Cart");
        }

        public ProductViewModel ConvertToViewModelFromProduct(Product product)
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Id = product.Id;
            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price.ToString();
            productViewModel.Url = product.Url;
            return productViewModel;
        }
    }
}
