using Microsoft.AspNetCore.Mvc;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using PlacementManagement.Models;

namespace CRUDwithGenericRepository.Controllers
{
    public class ProductController : Controller
    {       

        //public ProductController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    try
        //    {                
        //        var products = _unitOfWork.Product.GetAll();
        //        return View(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(ProductViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var product = new Product()
        //            {
        //                ProductName = model.ProductName,
        //                Price = model.Price,
        //                Qty = model.Qty                        
        //            };

        //            _unitOfWork.Product.Insert(product);
        //            _unitOfWork.Save();
        //            TempData["SuccessMessage"] = "New product created.";
        //            return RedirectToAction("Index");
        //        }
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}

        //[HttpGet]
        //public IActionResult Edit(int Id)
        //{
        //    try
        //    {
        //        var product = _unitOfWork.Product.GetById(Id);

        //        if (product != null)
        //        {
        //            var productViewModel = new ProductViewModel()
        //            {
        //                Id = product.Id,
        //                ProductName = product.ProductName,
        //                Price = product.Price,
        //                Qty = product.Qty
        //            };
        //            return View(productViewModel);
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //public IActionResult Edit(ProductViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var product = new Product()
        //            {
        //                Id = model.Id,
        //                ProductName = model.ProductName,
        //                Price = model.Price,
        //                Qty = model.Qty                        
        //            };

        //            _unitOfWork.Product.Update(product);
        //            _unitOfWork.Save();
        //            TempData["SuccessMessage"] = "Product details updated.";
        //            return RedirectToAction("Index");
        //        }
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}

        //[HttpGet]
        //public IActionResult Delete(int Id)
        //{
        //    try
        //    {
        //        var product = _unitOfWork.Product.GetById(Id);

        //        if (product != null)
        //        {
        //            var productViewModel = new ProductViewModel()
        //            {
        //                Id = product.Id,
        //                ProductName = product.ProductName,
        //                Price = product.Price,
        //                Qty = product.Qty                        
        //            };
        //            return View(productViewModel);
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int Id)
        //{
        //    try
        //    {
        //        var product = _unitOfWork.Product.GetById(Id);

        //        if (product != null)
        //        {
        //            _unitOfWork.Product.Delete(product);
        //            _unitOfWork.Save();
        //            TempData["SuccessMessage"] = "Product details deleted.";
        //            return RedirectToAction("Index");
        //        }
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}
    }
}
