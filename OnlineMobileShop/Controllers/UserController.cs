using OnlineMobileShop.Entity;
using OnlineMobileShop.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMobileShop.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        // GET: User
       
        public ActionResult ViewBrand()
        {
            BrandRepository brandRepository = new BrandRepository();
            IEnumerable<Entity.Brand> brand = brandRepository.ViewBrand();
            return View(brand);
        }
        [HttpGet]
        public ActionResult ViewMobile(int id)
        {
            BrandRepository brandRepository = new BrandRepository();
            List<Mobile> products = (List<Mobile>)brandRepository.GetProductCategory(id);
            return View(products);
        }
    }
}