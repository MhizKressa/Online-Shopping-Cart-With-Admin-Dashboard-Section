using KressaFashionHub.Models.Data;
using KressaFashionHub.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KressaFashionHub.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index/{pages}
        //public ActionResult Index(string page = "")
        //{
        //    //Get/Set page slug
        //    if (page == "")
        //        page = "home";

        //    //Declare model and DTO
        //    PageVM model;
        //    PageDTO dto;

        //    //Check if page exists
        //    using (Database db = new Database())
        //    {
        //        if (!db.Pages.Any(x => x.Slug.Equals(page)))
        //        {
        //            return RedirectToAction("Index", new { page = "" });
        //        }
        //    }

        //    //get page dto
        //    using (Database db = new Database())
        //    {
        //        dto = db.Pages.Where(x => x.Slug == page).FirstOrDefault();
        //    }

        //    //set page title
        //    ViewBag.PageTitle = dto.Title;

        //    //check for sidebar
        //    if (dto.HasSidebar == true)
        //    {
        //        ViewBag.HasSidebar = "Yes";
        //    }
        //    else
        //    {
        //        ViewBag.HasSidebar = "No";
        //    }

        //    //initialize the model
        //    model = new PageVM(dto);

        //    //return view with model

        //    return View(model);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult WeddingDresses()
        {
            return View();
        }

    }
}