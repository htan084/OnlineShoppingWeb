using BusinessLayer;
using OnlineShoppingWeb.Filter;
using OnlineShoppingWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingWeb.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private ZhenLiuOnlineDBContext db = new ZhenLiuOnlineDBContext();

        public ActionResult ShowMyOrders()
        {
            return View();
        }
        //[HttpGet]
        //public JsonResult GetAllOrders(OrderViewModel order)
        //{
        //    var db = new ZhenLiuOnlineDBContext();

        //    var listOfOrders = db.Orders.Select(o=>new OrderViewModel { 
        //      CustomerName = o.Customer.FirstName+" "+o.Customer.LastName,
        //      IsChecked = o.IsChecked,
        //      IsReceived = o.IsReceived,
        //      IsShipped = o.IsShipped,
        //      OrderId = o.OrderId,
        //      OrderNo = o.OrderNo,
        //      OrderTime = o.OrderTime,
        //      Total = o.Total
        //    }).ToList();
        //    return Json(listOfOrders, JsonRequestBehavior.AllowGet); ;
        //}
        [AdminFilter]
        [HttpGet]
        public PartialViewResult GetAllOrders()
        {
            
            List<Order> orders = db.Orders.ToList();
            var orderViewModelList = ConvertToViewOrderList(orders);
            return PartialView("_Orders", orderViewModelList);
        }
        [AdminFilter]
        public JsonResult GetCountOfNewOrders()
        {
            int countOfOrders = db.Orders.Where(x=>x.IsChecked==false).Count();
            String count = "";
            if (countOfOrders != 0)
            {
                count = countOfOrders.ToString();
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        [AdminFilter]
        public JsonResult GetCountOfCheckedOrders()
        {
            int countOfOrders = db.Orders.Where(x => (x.IsChecked == true && x.IsShipped == false)).Count();
            String count = "";
            if (countOfOrders != 0)
            {
                count = countOfOrders.ToString();
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        [AdminFilter]
        public JsonResult GetCountOfOrdersInShipping()
        {
            int countOfOrders = db.Orders.Where(x => (x.IsShipped == true && x.IsReceived == false)).Count();
            String count = "";
            if (countOfOrders != 0)
            {
                count = countOfOrders.ToString();
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        [AdminFilter]
        [HttpGet]
        public PartialViewResult GetUnCheckedOrders()
        {

            List<Order> orders = db.Orders.Where(x=>x.IsChecked==false).ToList();
            var orderViewModelList = ConvertToViewOrderList(orders);
            return PartialView("_Orders", orderViewModelList);
        }
        [AdminFilter]
        [HttpGet]
        public PartialViewResult GetCheckedOrders()
        {

            List<Order> orders = db.Orders.Where(x => (x.IsChecked == true && x.IsShipped == false)).ToList();
            var orderViewModelList = ConvertToViewOrderList(orders);
            return PartialView("_Orders", orderViewModelList);
        }
        [AdminFilter]
        [HttpGet]
        public PartialViewResult GetOrdersInShipping()
        {

            List<Order> orders = db.Orders.Where(x => (x.IsShipped == true && x.IsReceived==false)).ToList();
            var orderViewModelList = ConvertToViewOrderList(orders);
            return PartialView("_Orders", orderViewModelList);
        }

        [AdminFilter]
        [HttpGet]
        public PartialViewResult GetReceivedOrders()
        {

            List<Order> orders = db.Orders.Where(x => x.IsReceived == true).ToList();
            var orderViewModelList = ConvertToViewOrderList(orders);
            return PartialView("_Orders", orderViewModelList);
        }

        [AdminFilter]
        [HttpPost]
        public JsonResult SaveOrder(OrderViewModel order)
        {

            return Json(true);
        }

        [AdminFilter]
        [HttpPost]
        public string SaveChanges(IEnumerable<OrderViewModel> models)
        {
            var orders = db.Orders.ToList();
            foreach (var item in models)
            {

                if (item.IsChecked)
                {
                    orders.Single(x => x.OrderId == item.OrderId).IsChecked = true;
                }
                else
                {
                    orders.Single(x => x.OrderId == item.OrderId).IsChecked = false;
                }

                if (item.IsShipped)
                {
                    orders.Single(x => x.OrderId == item.OrderId).IsShipped = true;
                }
                else
                {
                    orders.Single(x => x.OrderId == item.OrderId).IsShipped = false;
                }

                if (item.IsReceived)
                {
                    orders.Single(x => x.OrderId == item.OrderId).IsReceived = true;
                }
                else
                {
                    orders.Single(x => x.OrderId == item.OrderId).IsReceived = false;
                }
            }
            db.SaveChanges();
            return "change successfully saved";
            
        }

        public ActionResult ViewOrderHistoryByCustomer(string userName)
        {
            var customerId = db.Customers.Single(x => x.UserName == userName).CustomerId;
            var orders = db.Orders.Where(x => x.CustomerId == customerId).ToList();
            var modelList = ConvertToViewOrderList(orders);
            return View(modelList);
        }

        [AdminFilter]
        public List<OrderViewModel> ConvertToViewOrderList(List<Order> orders)
        {

            var listOfOrderViewModels = orders.Select(o => new OrderViewModel
            {
                CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                IsChecked = (bool)o.IsChecked,
                IsReceived = (bool)o.IsReceived,
                IsShipped = (bool)o.IsShipped,
                OrderId = o.OrderId,
                OrderNo = o.OrderNo,
                OrderTime = o.OrderTime,
                Total = "$"+ o.Total,
                OrderLines = o.OrderLines
            }).ToList();
            return listOfOrderViewModels;
        }
    }
}
