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

            return View(ConvertToViewModelList(products));
        }


        public ActionResult ProductManage()
        {
            var products = service.GetProducts();
            return View(ConvertToViewModelList(products).ProductList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Upload(FileUploadViewModel fileUpload)
        {
          
            int productId = Convert.ToInt32(Request["ProductId"]);
            string url = productId + ".jpg";
            fileUpload.FileToUpload.SaveAs(@"D:\MyWifeWeb\OnlineShoppingWeb\OnlineShoppingWeb\Image\" + url);
            var product = service.GetProducts().Single(x => x.Id == productId);
            product.Url = url;
            service.SaveProduct(product);
            ViewBag.Message = "The file is saved";

            return RedirectToAction("ProductManage");

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
            var productViewModel = ConvertToViewModelFromProduct(product);
            return View(productViewModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {
            var product = service.GetProducts().Single(prod => prod.Id == id);
            var productViewModel = ConvertToViewModelFromProduct(product);
            TryUpdateModel(productViewModel);
            if (ModelState.IsValid)
            {
                var modifiedProduct = ConvertToProductFromViewModel(productViewModel);
                service.SaveProduct(modifiedProduct);
                return RedirectToAction("ProductManage");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = service.GetProducts().Single(prod => prod.Id == id);
            if (ModelState.IsValid)
            {
                service.DeleteProduct(product);
                return RedirectToAction("ProductManage");
            }
            return View();
        }

        public ProductViewModelList ConvertToViewModelList(List<Product> products)
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

        public ProductViewModel ConvertToViewModelFromProduct(Product product)
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Id = product.Id;
            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price.ToString();
            productViewModel.Url = product.Url;
            return productViewModel;
        }

        public Product ConvertToProductFromViewModel(ProductViewModel productViewModel)
        {
            var product = new Product { 
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Price = Convert.ToInt32(productViewModel.Price),
                Url = productViewModel.Url
            };
            return product;
        }
    }
}
