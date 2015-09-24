using BusinessLayer.Business;
using OnlineShoppingWeb.Business;
using OnlineShoppingWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingWeb.Controllers
{
    public class HomeController : Controller
    {
        private CustomerService service;
        public HomeController()
        {
            service = new CustomerService();
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult AddNew()
        {

            return View("Form", new CustomerViewModel());
        }

        //[HttpPost]
        //public ActionResult SaveCustomer(Customer customer, string btnSubmit)
        //{


        //    switch (btnSubmit)
        //    {
        //        case "Save":
        //            if (ModelState.IsValid)
        //            {
        //                if (customer.CustomerID == 0)
        //                {
        //                    service.CreateNewCustomer(customer);
        //                    ViewBag.Message = "Customer " + customer.FirstName + "has been saved";
        //                }
        //                else
        //                {
        //                    service.UpdateCustomer(customer);
        //                    ViewBag.Message = "Customer " + customer.FirstName + "has been updated";
        //                }
        //            }
        //            else
        //            {
        //                return View("Form");
        //            }
        //            break;

        //        case "Cancel":
        //            return RedirectToAction("Index");

        //    }


        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult SaveCustomer(Customer customer, string btnSubmit)
        {


            switch (btnSubmit)
            {
                case "Save":
                    //UserService _userService = new UserService();
                    //if (_userService.IsUserNameOccupied(customer.UserName))
                    //{
                    //    ModelState.AddModelError("UserName", "This user name has already been used");
                    //}
                    if (ModelState.IsValid)
                    {

                        service.CreateNewCustomer(customer);
                        ViewBag.Message = "Customer " + customer.FirstName + "has been saved";

                    }
                    else
                    {
                        var customerViewModel = new CustomerViewModel();
                        customerViewModel.Address = customer.Address;
                        customerViewModel.Age = customer.Age.ToString();
                        customerViewModel.CustomerID = customer.CustomerID;
                        customerViewModel.FirstName = customer.FirstName;
                        customerViewModel.LastName = customer.LastName;
                        customerViewModel.Mobile = customer.Mobile;
                        customerViewModel.Password = customer.UserPass;
                        customerViewModel.UserName = customer.UserName;
                        customerViewModel.Email = customer.Email;
                        return View("Form", customerViewModel);
                    }
                    break;

                case "Cancel":
                    return RedirectToAction("Index");

            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            service.DeleteCustomer(customer);
            return RedirectToAction("MyAdmin");
        }

        [Authorize]
        public ActionResult MyAdmin()
        {
            var customerListModel = new CustomerViewModelList();
            customerListModel.CustomerList = new List<CustomerViewModel>();
            var customerList = service.GetCustomers();
            foreach (var customer in customerList)
            {
                var customerViewModel = new CustomerViewModel();
                customerViewModel.Address = customer.Address;
                customerViewModel.Age = customer.Age.ToString();
                customerViewModel.CustomerID = customer.CustomerID;
                customerViewModel.FirstName = customer.FirstName;
                customerViewModel.LastName = customer.LastName;
                customerViewModel.Mobile = customer.Mobile;
                customerViewModel.Password = customer.UserPass;
                customerViewModel.UserName = customer.UserName;
                customerViewModel.Email = customer.Email;
                customerListModel.CustomerList.Add(customerViewModel);
            }
            return View(customerListModel);
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            if (customer != null)
            {
                var customerViewModel = new CustomerViewModel();
                customerViewModel.Address = customer.Address;
                customerViewModel.Age = customer.Age.ToString();
                customerViewModel.CustomerID = customer.CustomerID;
                customerViewModel.FirstName = customer.FirstName;
                customerViewModel.LastName = customer.LastName;
                customerViewModel.Mobile = customer.Mobile;
                customerViewModel.Password = customer.UserPass;
                customerViewModel.UserName = customer.UserName;
                customerViewModel.Email = customer.Email;
                return View(customerViewModel);
            }
            else
            {
                ViewBag.Message = "Can not find customer";
                return View("MyAdmin");
            }
        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            TryUpdateModel(customer,new string[] {"FirstName","LastName","Age","Email","Address","Mobile"});
            if (ModelState.IsValid)
            {
                service.UpdateCustomer(customer);
                return RedirectToAction("MyAdmin");
            }
            else
            {
                return View();
            }
        }
    }
}
