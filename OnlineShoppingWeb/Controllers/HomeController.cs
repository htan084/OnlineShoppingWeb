﻿using BusinessLayer;
using BusinessLayer.Business;
using OnlineShoppingWeb.Business;
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
    public class HomeController : Controller
    {
        private CustomerService service;
        private ZhenLiuOnlineDBContext db = new ZhenLiuOnlineDBContext();
        public HomeController()
        {
            this.service = new CustomerService();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            var productService = new ProductService();
            var products = productService.GetProducts();
            var productControl = new ProductController();
            var listModel = productControl.ConvertToViewModelList(products).ProductList;

            return View(listModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AddNew()
        {
            return View("Form", new CustomerViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveCustomer(CustomerViewModel customerViewModel, string btnSubmit)
        {

            switch (btnSubmit)
            {
                case "Save":
                    UserService _userService = new UserService();
                    string userName = customerViewModel.UserName;

                    if ((string.IsNullOrEmpty(customerViewModel.UserName)) || customerViewModel.UserName.Length < 5 || customerViewModel.UserName.Length > 9)
                    {
                        ModelState.AddModelError("UserName", "UserName needs to between 5 to 9 characters");
                    }
                    else if (_userService.IsUserNameOccupied(userName))
                    {
                        ModelState.AddModelError("UserName", "This user name has already been used");
                    }
                    if ((string.IsNullOrEmpty(customerViewModel.UserPass)) || customerViewModel.UserPass.Length < 5 || customerViewModel.UserPass.Length > 9)
                    {
                        ModelState.AddModelError("UserPass", "Password needs to between 5 to 9 characters");
                    }


                    if (ModelState.IsValid)
                    {
                        var customer = ConvertToCustomerFromViewModel(customerViewModel);
                        service.CreateNewCustomer(customer);
                        ViewBag.Message = "Customer " + customerViewModel.FirstName + "has been saved";
                        return View();
                    }
                    else
                    {
                        return View("Form", customerViewModel);
                    }

                case "Cancel":
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AdminFilter]
        public ActionResult Delete(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            service.DeleteCustomer(customer);
            return RedirectToAction("MyAdmin");
        }

        [HttpGet]
        [AdminFilter]
        public ActionResult AdminHomePage()
        {
            return View();
        }

        [AdminFilter]
        public ActionResult MyAdmin()
        {
            var customerListModel = new CustomerViewModelList();
            customerListModel.CustomerList = new List<CustomerViewModel>();
            var customerList = service.GetCustomers();
            foreach (var customer in customerList)
            {
                var customerViewModel = ConvertToViewModelFromCustomer(customer);
                customerListModel.CustomerList.Add(customerViewModel);
            }
            return View(customerListModel);
        }

        [HttpGet]
        [ActionName("Edit")]
        [AdminFilter]
        public ActionResult Edit_Get(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            if (customer != null)
            {
                var customerViewModel = ConvertToViewModelFromCustomer(customer);
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
        [AdminFilter]
        public ActionResult Edit_Post(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            var customerViewModel = ConvertToViewModelFromCustomer(customer);
            TryUpdateModel(customerViewModel, new string[] { "FirstName", "LastName", "Email", "ConfirmEmailAddress", "Address", "Mobile", "DateOfBirth" });
            if (ModelState.IsValid)
            {
                var modiefiedCustomer = ConvertToCustomerFromViewModel(customerViewModel);
                service.UpdateCustomer(modiefiedCustomer);
                return RedirectToAction("MyAdmin");
            }
            else
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult getDetailsByCustomer(string userName)
        {
            var customer = db.Customers.Single(x => x.UserName == userName);
            var customerViewModel = ConvertToViewModelFromCustomer(customer);
            return View(customerViewModel);
        }

        public string saveDetailsByCustomer(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            var customerViewModel = ConvertToViewModelFromCustomer(customer);
            TryUpdateModel(customerViewModel, new string[] { "FirstName", "LastName", "UserPass", "ConfirmUserPass", "Email", "ConfirmEmailAddress", "Address", "Mobile", "DateOfBirth" });
            if ((string.IsNullOrEmpty(customerViewModel.UserPass)) || customerViewModel.UserPass.Length < 5 || customerViewModel.UserPass.Length > 9)
            {
                ModelState.AddModelError("UserPass", "Password needs to between 5 to 9 characters");
            }

            if (ModelState.IsValid)
            {
                var modiefiedCustomer = ConvertToCustomerFromViewModel(customerViewModel);
                service.UpdateCustomer(modiefiedCustomer);
                return "valid";
            }
            else
            {

                return "invalid";
            }
        } 

        public CustomerViewModel ConvertToViewModelFromCustomer(Customer customer)
        {
            var customerViewModel = new CustomerViewModel();
            customerViewModel.Address = customer.Address;
            customerViewModel.CustomerID = customer.CustomerId;
            customerViewModel.FirstName = customer.FirstName;
            customerViewModel.LastName = customer.LastName;
            customerViewModel.Mobile = customer.Mobile;
            customerViewModel.UserPass = customer.UserPass;
            customerViewModel.ConfirmUserPass = customer.UserPass;
            customerViewModel.UserName = customer.UserName;
            customerViewModel.Email = customer.Email;
            customerViewModel.ConfirmEmailAddress = customer.Email;
            customerViewModel.DateOfBirth = customer.DateOfBirth;
            return customerViewModel;
        }

        public Customer ConvertToCustomerFromViewModel(CustomerViewModel customerViewModel)
        {
            Customer customer = new Customer
            {
                Address = customerViewModel.Address,
                CustomerId = customerViewModel.CustomerID,
                Email = customerViewModel.Email,
                FirstName = customerViewModel.FirstName,
                LastName = customerViewModel.LastName,
                Mobile = customerViewModel.Mobile,
                UserName = customerViewModel.UserName,
                UserPass = customerViewModel.UserPass,
                DateOfBirth = customerViewModel.DateOfBirth
            };
            return customer;
        }
    }
}
