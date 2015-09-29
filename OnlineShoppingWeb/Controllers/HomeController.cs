using BusinessLayer;
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

        [HttpPost]
        public ActionResult SaveCustomer(CustomerViewModel customerViewModel, string btnSubmit)
        {


            switch (btnSubmit)
            {
                case "Save":
                    //UserService _userService = new UserService();
                    //if (_userService.IsUserNameOccupied(customer.UserName))
                    //{
                    //    ModelState.AddModelError("UserName", "This user name has already been used");
                    //}

                    if ((string.IsNullOrEmpty(customerViewModel.UserName)) || customerViewModel.UserName.Length < 5 || customerViewModel.UserName.Length>9)
                    {
                        ModelState.AddModelError("UserName", "UserName needs to between 5 to 9 characters");
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

                    }
                    else
                    {
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
                var customerViewModel = ConvertToViewModelFromCustomer(customer);
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
        public ActionResult Edit_Post(int customerID)
        {
            var customer = service.GetCustomerById(customerID);
            var customerViewModel = ConvertToViewModelFromCustomer(customer);
            TryUpdateModel(customerViewModel, new string[] { "FirstName", "LastName","Email", "Address", "Mobile" ,"DateOfBirth"});
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

        public CustomerViewModel ConvertToViewModelFromCustomer(Customer customer)
        {
            var customerViewModel = new CustomerViewModel();
            customerViewModel.Address = customer.Address;
            customerViewModel.CustomerID = customer.CustomerId;
            customerViewModel.FirstName = customer.FirstName;
            customerViewModel.LastName = customer.LastName;
            customerViewModel.Mobile = customer.Mobile;
            customerViewModel.UserPass = customer.UserPass;
            customerViewModel.UserName = customer.UserName;
            customerViewModel.Email = customer.Email;
            customerViewModel.DateOfBirth = customer.DateOfBirth;
            return customerViewModel;
        }

        public Customer ConvertToCustomerFromViewModel(CustomerViewModel customerViewModel)
        {
            Customer customer = new Customer { 
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
