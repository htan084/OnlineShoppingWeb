using BusinessLayer;
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
            FormsAuthentication.SignOut();
            return View("OnlineShoppingWeb/Views/Home/Index");
        }

        [HttpPost]
        public ActionResult DoLogin(UserViewModel userDetails)
        {
            if (ModelState.IsValid)
            {
                
                var isUserValid = _userService.IsValidUser(userDetails.UserName, userDetails.Password);
                if (isUserValid)
                {
                    UserStatus status = _userService.GetUserValidity(userDetails.UserName);
                    var isAdmin = status == UserStatus.AuthenticatedAdmin ? true : false;
                    Session["IsAdmin"] = isAdmin;
                    
                    FormsAuthentication.SetAuthCookie(userDetails.UserName, false);
                    string name = System.Web.HttpContext.Current.User.Identity.Name;
                    Session["UserName"] = userDetails.UserName;
                    var returnUrl = Request.QueryString["ReturnURL"];
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }else{
                        return RedirectToAction("Index", "Home");
                    }
                }else{  ModelState.AddModelError("UserName", "Invalid Username or Password");
                    return View("Login");
                }
           
            }else{
                return null;
            }  

       
        }
    }
}
