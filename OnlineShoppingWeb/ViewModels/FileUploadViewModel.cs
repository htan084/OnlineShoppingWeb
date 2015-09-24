using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWeb.ViewModels
{
    public class FileUploadViewModel
    {
        public HttpPostedFileBase FileToUpload { get; set; }
    }
}