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
            Session["Cart"] = null;
            return Content("<script> alert('Thank you for shopping with us, you have successfully sign out!');window.location='/Home/Index'</script>");
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
