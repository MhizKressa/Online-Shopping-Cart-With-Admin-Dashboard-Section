using KressaFashionHub.Models.Data;
using KressaFashionHub.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KressaFashionHub.Controllers
{
    public class PagesController : Controller
    {
        // GET: Index/{pages}
        public ActionResult Index(string page = "")
        {
            //Get/Set page slug
            if (page == "")
                page = "home";

            //Declare model and DTO
            PageVM model;
            PageDTO dto;

            //Check if page exists
            using (Database db = new Database())
            {
                if (!db.Pages.Any(x => x.Slug.Equals(page)))
                {
                    return RedirectToAction("Index", new { page = "" });
                }
            }

            //get page dto
            using (Database db = new Database())
            {
                dto = db.Pages.Where(x => x.Slug == page).FirstOrDefault();
            }

            //set page title
            ViewBag.PageTitle = dto.Title;

            //check for sidebar
            if (dto.HasSidebar == true)
            {
                ViewBag.HasSidebar = "Yes";
            }
            else
            {
                ViewBag.HasSidebar = "No";
            }

            // Init model
            model = new PageVM(dto);

            // Return view with model
            return View(model);
            
        }

        public ActionResult PagesMenuPartial()
        {
            //Declare a list of PageVM
            List<PageVM> pageVMList;

            //Get all pages except home
            using (Database db = new Database())
            {
                pageVMList = db.Pages.ToArray().OrderBy(x => x.Sorting).Where(x => x.Slug != "home").Select(x => new PageVM(x)).ToList();
            }

            //Return partial view
            return PartialView(pageVMList);


        }

        public ActionResult SidebarPartial()
        {
            //Declare model
            SidebarVM model;

            //init model
            using (Database db = new Database())
            {
                SidebarDTO dto = db.Sidebar.Find(1);

                model = new SidebarVM(dto);
            }
            //return partialView with model
            return PartialView(model);
        }

        //public ActionResult About()
        //{
        //    return View();
        //}
    }
}