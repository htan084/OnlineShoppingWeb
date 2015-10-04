using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class PageModel
    {
        int currentIndex;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }
        int pageNumber;

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }
   
    }
}