using BusinessLayer;
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
        public ActionResult OnlineShopping(string search="",int id = 1)
        {
            var products = service.GetProducts().Where(x => x.Name.StartsWith(search) || search == "").ToList();
            int pageIndex = id;
            var productViewModelList = ShowPaging(products, pageIndex);

            return View(productViewModelList);
        }

        [AdminFilter]
        public ActionResult ProductManage()
        {
            var products = service.GetProducts();
            return View(ConvertToViewModelList(products).ProductList);
        }
        [HttpGet]
        [ActionName("Create")]
        [AdminFilter]
        public ActionResult Create_Get()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ActionName("Create")]
        [AdminFilter]
        public ActionResult Create_Post(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = ConvertToProductFromViewModel(productViewModel);
                service.CreateProduct(product);
                return RedirectToAction("ProductManage");
            }else
            return View(productViewModel);
        }
        [AdminFilter]
        public ActionResult Upload(FileUploadViewModel fileUpload)
        {
            string name = fileUpload.FileToUpload.FileName;
            string imageType = name.Substring(name.LastIndexOf('.'));
            int productId = Convert.ToInt32(Request["ProductId"]);
            string url = productId + imageType;
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
        [AdminFilter]
        public ActionResult Edit_Get(int id)
        {
            var product = service.GetProducts().Single(prod => prod.Id == id);
            var productViewModel = ConvertToViewModelFromProduct(product);
            return View(productViewModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        [AdminFilter]
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
        [AdminFilter]
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

        int rowNumber = 5;
        [HttpGet]
        public ActionResult ShowProductShoppingPage(string search = "", int id = 1)
        {
            

            //service.GetProductsFromStoreProcedure(5, 1);

            var productList = service.GetProducts().Where(x => x.Name.StartsWith(search) || search == "").ToList();
            int pageIndex = id;
            var productViewModelList = ShowPaging(productList, pageIndex);
            return View(productViewModelList.ProductList);
        }

        [AdminFilter]
        public ActionResult SpecialProductManage()
        {
            var specialProducs = service.GetProducts().Where(x => x.OnSpecial == true).ToList();
            var SpecialProductsModel = ConvertToViewModelList(specialProducs);
            var unSpecialProducts = service.GetProducts().Where(x=>x.OnSpecial == false || x.OnSpecial == null).ToList();
            var unSpecialProductsModel = ConvertToViewModelList(unSpecialProducts);
            ViewBag.unSpecialProducts = unSpecialProductsModel.ProductList;
            return View(SpecialProductsModel.ProductList);
        }

        public ProductViewModelList ShowPaging(List<Product> productList, int pageIndex)
        {

            int start = (pageIndex - 1) * rowNumber;

            var pagedList = productList.Skip(start).Take(rowNumber).ToList();

            var productViewModelList = ConvertToViewModelList(pagedList);

            double page = Math.Ceiling(productList.Count * 1.0 / rowNumber * 1.0);

            PageModel pageItem = new PageModel()
            {
                CurrentIndex = pageIndex,
                PageNumber = Convert.ToInt32((page)),
            };
            ViewBag.pageItem = pageItem;
            return productViewModelList;
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
                Price = Convert.ToDecimal(productViewModel.Price),
                Url = productViewModel.Url
            };
            return product;
        }
    }
}
