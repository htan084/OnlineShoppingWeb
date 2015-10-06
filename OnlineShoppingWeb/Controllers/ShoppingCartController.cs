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

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            int num = 0;
            bool isValidQuantity = Int32.TryParse(Request["Quantity"],out num);
            if ((!isValidQuantity) || num>100 || num<0)
            {
                ModelState.AddModelError("Quantity", "please enter a valid number");
            }
            if (ModelState.IsValid)
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
                        cart[index].quantity += quantity;
                    }
                    Session["Cart"] = cart;
                }
                return RedirectToAction("ShowProductShoppingPage", "Product");
            }
            else
            {
                return Content("<script> alert('please enter a valid quantity number');window.location='/Product/ShowProductShoppingPage'</script>");
            }
        }

        [HttpPost]
        public ActionResult EditQuantity(int id)
        {
            int quantity = 0;
            bool isValidQuantity = Int32.TryParse(Request["Quantity"], out quantity);
            if ((!isValidQuantity) || quantity > 100 || quantity < 0)
            {
                ModelState.AddModelError("Quantity", "please enter a valid number");
            }
            if (ModelState.IsValid)
            {
                int index = IsExisting(id);
                List<ShoppingItemViewModel> cart = (List<ShoppingItemViewModel>)Session["Cart"];
                cart[index].quantity = quantity;
                Session["Cart"] = cart;
                return RedirectToAction("ViewCart");
            }
            else
            {
                return Content("<script> alert('please enter a valid quantity number');window.location='/ShoppingCart/ViewCart'</script>");
            }
        }

        public ActionResult Delete(int id)
        {
            int index = IsExisting(id);
            List<ShoppingItemViewModel> cart = (List<ShoppingItemViewModel>)Session["Cart"];
            cart.RemoveAt(index);
            Session["Cart"] = cart;
            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            string userName = User.Identity.Name;
            OrderLine orderLine = new OrderLine { 
                
            
            };
            Random rad = new Random();
            Order order = new Order {
                Customer = db.Customers.Single(x => x.UserName == userName),
                OrderNo = rad.Next(1,10000),
                CustomerId = db.Customers.Single(x => x.UserName == userName).CustomerId,
                OrderTime = DateTime.Now.Date,
                Total = CaculateTotalPrice()
            };
            db.Orders.Add(order);

            foreach (var item in (List<ShoppingItemViewModel>)Session["Cart"])
            {
                OrderLine orderItem = new OrderLine
                {
                    Order = order,
                    OrderId = order.OrderId,
                    ProductId = item.Product.Id,
                    Product = db.Products.Single(x=>x.Id == item.Product.Id),
                    Quantity = item.quantity
                };
                db.OrderLines.Add(orderItem);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return  Content("<script> alert('Congratulations!your order has been sent to the seller');window.location='/Product/ShowProductShoppingPage'</script>");
        }

        public double CaculateTotalPrice()
        {
            double totalPrice = 0.0;
            List<ShoppingItemViewModel> cart = (List<ShoppingItemViewModel>)Session["Cart"];
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    totalPrice += (Convert.ToDouble(item.Product.Price) * item.quantity);
                }
            }
            return totalPrice;
        }

        [ChildActionOnly]
        public ActionResult ShowTotalPrice()
        {
            double total = CaculateTotalPrice();
            return View(total);
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
