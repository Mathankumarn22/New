using System.Collections.Generic;
using OnlineMobileShop.Entity;
using OnlineMobileShop.Respository;
using System.Web.Mvc;
using System.Data;
using OnlineMobileShop.BL;
using OnlineMobileShop.Models;

namespace OnlineMobileShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MobileController : Controller
    {
        // GET: Mobile
        MobileRespository repository = new MobileRespository();
        MobileBL mobileBL = new MobileBL();
        public ActionResult RunMaster()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult MobileDetails()
        {
            IEnumerable<Mobile> mobile = mobileBL.MobileDetails();

            return View(mobile);
        }
        [HttpGet]
        public ActionResult Create()
        {
            BrandRepository brandRepository = new BrandRepository();
            TempData["brand"] = new SelectList(brandRepository.ViewBrand(), "BrandID", "BrandName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Mobile mobile)
        {

            BrandRepository brandRepository = new BrandRepository();
            TempData["brand"] = new SelectList(brandRepository.ViewBrand(), "BrandID", "BrandName");
            if (ModelState.IsValid)
            {
                mobile.Brand = mobile.Brand;
                mobile.Model = mobile.Model;
                mobile.Battery = mobile.Battery;
                mobile.RAM = mobile.RAM;
                mobile.ROM = mobile.ROM;
                mobile.Price = mobile.Price;
                if (mobileBL.Add(mobile) > 0)
                {
                    ViewBag.message = "Successfull";
                    return RedirectToAction("MobileDetails");
                }
                ViewBag.message = "falied";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BrandRepository repository = new BrandRepository();
           
            ViewBag.Brand = new SelectList(repository.ViewBrand(), "BrandID", "BrandName");

            Mobile editMobile = mobileBL.Edit(id);
            return View(editMobile);
        }

        [HttpPost]
        public ActionResult Edit(Mobile mobile)
        {
            MobileRespository repository = new MobileRespository();
            if (ModelState.IsValid)
            {
                mobile.Brand = mobile.Brand;
                mobile.Model = mobile.Model;
                mobile.Battery = mobile.Battery;
                mobile.RAM = mobile.RAM;
                mobile.ROM = mobile.ROM;
                mobile.Price = mobile.Price;
                repository.Update(mobile);
                return RedirectToAction("MobileDetails", "Mobile");

            }
            BrandRepository brandRepository = new BrandRepository();
            IEnumerable<Entity.Brand> brand = brandRepository.ViewBrand();

            ViewBag.Brand = new SelectList(brand, "BrandID", "BrandName");
            return RedirectToAction("Edit");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            mobileBL.Delete(id);
            return RedirectToAction("MobileDetails");
        }




        [HttpGet]
        public ActionResult AddBrand()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBrand(Models.Brand brand)
        {
            if (ModelState.IsValid)
            {
                Entity.Brand brandDetails = new Entity.Brand()
                {
                    BrandName = brand.BrandName,
                };
                mobileBL.AddBrand(brandDetails);
                return RedirectToAction("Create");
            }

            return View();
        }
        public ActionResult ViewBrand()
        {
            BrandRepository brandRepository = new BrandRepository();
            IEnumerable<Entity.Brand> brand = brandRepository.ViewBrand();
            return View(brand);
        }
        [HttpGet]
        public ActionResult AdminPage()
        {
            return View();
        }
    }
}
