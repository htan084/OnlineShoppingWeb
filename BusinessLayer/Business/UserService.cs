﻿using OnlineShoppingWeb;
using OnlineShoppingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Business
{
    public class UserService
    {
        private CustomerContext _customerContext;

        public UserService()
        {
            _customerContext = new CustomerContext();
        }
        public bool IsValidUser(string userName, string password)
        {
           
                var customer = _customerContext.Customers.Where(x => x.UserName == userName).FirstOrDefault();
                if (customer != null)
                {
                    return customer.UserPass == password;
                }
                else
                {
                    return false;
                }
        }

        public bool IsUserNameOccupied(string userName)
        {

            var customer = _customerContext.Customers.Where(x => x.UserName == userName).FirstOrDefault();
            if (customer != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}