using BusinessLayer.Business;
using OnlineShoppingWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShoppingWeb.Controllers
{
    public class AuthenticationController : Controller
    {

        private UserService _userService;

        public AuthenticationController()
        {
            _userService = new UserService();
        }

        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(UserViewModel userDetails)
        {
            if (ModelState.IsValid)
            {
                var isUserValid = _userService.IsValidUser(userDetails.UserName, userDetails.Password);
                if (isUserValid)
                {
                    FormsAuthentication.SetAuthCookie(userDetails.UserName, false);
                    var returnUrl = Request.QueryString["ReturnURL"];
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }else{
                        return RedirectToAction("Index", "Home");
                    }
                }else{  ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
           
            }else{
                return null;
            }  

       
        }
    }
}
