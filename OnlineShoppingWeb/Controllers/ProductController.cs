using OnlineShoppingWeb.Business;
using OnlineShoppingWeb.Models;
using OnlineShoppingWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingWeb.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        private ProductService service;

        public ProductController()
        {
            service = new ProductService();
        }

        [AllowAnonymous]
        public ActionResult OnlineShopping()
        {
            var products = service.GetProducts();

            return View(ConvertToViewModel(products));
        }


        public ActionResult ProductManage()
        {
            var products = service.GetProducts();
            return View(ConvertToViewModel(products).ProductList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Upload(FileUploadViewModel fileUpload)
        {

            fileUpload.FileToUpload.SaveAs(@"D:\MyWifeWeb\OnlineShoppingWeb\OnlineShoppingWeb\Image\" + fileUpload.FileToUpload.FileName);

            ViewBag.Message = "The file is saved";

            return View("Create");

        }


        //public ActionResult NotRight()
        //{
        //    throw new Exception("You hit me");

        //    return View("Index", new FileUploadViewModel());

        //}
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            var product = service.GetProducts().Single(prod => prod.Id == id);
            var productViewModel = new ProductViewModel();
            productViewModel.Id = product.Id;
            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price.ToString();
            productViewModel.Url = product.Url;
            return View(productViewModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {
            var product = service.GetProducts().Single(prod => prod.Id == id);
            TryUpdateModel(product);
            if (ModelState.IsValid)
            {
                service.SaveProduct(product);
                return RedirectToAction("ProductManage");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = service.GetProducts().Single(prod => prod.Id == id);
            TryUpdateModel(product);
            if (ModelState.IsValid)
            {
                service.DeleteProduct(product);
                return RedirectToAction("ProductManage");
            }
            return View();
        }

        public ProductViewModelList ConvertToViewModel(List<Product> products)
        {
            ProductViewModelList list = new ProductViewModelList();
            if (products != null)
            {
                list.ProductList = new List<ProductViewModel>();
            }
            foreach (var item in products)
            {
                var productViewModel = new ProductViewModel();
                productViewModel.Id = item.Id;
                productViewModel.Name = item.Name;
                productViewModel.Price = item.Price.ToString();
                productViewModel.Url = item.Url;
                list.ProductList.Add(productViewModel);
            }
            return list;
        }
    }
}
