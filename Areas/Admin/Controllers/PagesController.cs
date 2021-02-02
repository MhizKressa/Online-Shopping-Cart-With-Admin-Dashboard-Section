using KressaFashionHub.Models.Data;
using KressaFashionHub.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KressaFashionHub.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Declare list of pagevm
            List<PageVM> pagesList;


            using (Database db = new Database())
            {
                //Initialize the list
                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }

            //Return View with list
            return View(pagesList);
        }

        // GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        // POST: Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
            //Check the state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Database db = new Database())
            {

                //Declare the slug
                string slug;

                //Initialize the pageDTO
                PageDTO dto = new PageDTO();

                //DTO  the title
                dto.Title = model.Title;

                //Check for and set slug if need be
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

                //make sure title and slug are unique
                if (db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That title or slug already exists");
                    return View(model);
                }

                //dto the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100;

                //save the dto
                db.Pages.Add(dto);
                db.SaveChanges();
            }

            //set tempdata message
            TempData["SM"] = "You Have Added A New Page!";

            //redirect
            return RedirectToAction("AddPage");

        }

        // GET: Admin/Pages/EditPage/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {

            // Declare PageVM
            PageVM model;
            using (Database db = new Database())
            {
                // Get the page
                PageDTO dto = db.Pages.Find(id);

                // Confirm page exists
                if (dto == null)
                {
                    return Content("The page already exists");
                }

                //initialize pagevm
                model = new PageVM(dto);

            }
            //return view with model
            return View(model);
        }

        // POST: Admin/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            // check model slug
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Database db = new Database())
            {
                // get the paage id
                int id = model.Id;

                //initialize slug
                string slug = "home";

                //get the page
                PageDTO dto = db.Pages.Find(id);

                //dto the title
                dto.Title = model.Title;

                //check for slug and set it if need be
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }

                //make sure title and slug are unique
                if (db.Pages.Where(x => x.Id != id).Any(x => x.Title == model.Title) ||
                    db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That title or page already exists!");
                    return View(model);
                }

                //dto the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;

                //save the dto
                db.SaveChanges();
            }

            //set tempdata message
            TempData["SM"] = "You Have Edited The Page";
            //redirect
            return RedirectToAction("EditPage");
        }

        // GET: Admin/Pages/PageDetails/id
        public ActionResult PageDetails(int id)
        {
            //declare pagevm
            PageVM model;

            using (Database db = new Database())
            {
                //get the page
                PageDTO dto = db.Pages.Find(id);

                //confirm page details
                if (dto == null)
                {
                    return Content("The page does not exist");
                }

                //initialize pagevm
                model = new PageVM(dto);
            }
            //return view with model
            return View(model);
        }

        // GET: Admin/Pages/DeletePage/id
        public ActionResult DeletePage(int id)
        {
            using (Database db = new Database())
            {
                //get the page
                PageDTO dto = db.Pages.Find(id);

                //remove the page
                db.Pages.Remove(dto);

                //save the page
                db.SaveChanges();
            }
            //redirect
            return RedirectToAction("Index");
        }

        // POST: Admin/Pages/ReorderPages
        [HttpPost]
        public void ReorderPages(int[] id)
        {
            using (Database db = new Database())
            {
                //set initial count
                int count = 1;

                //declare pagedto
                PageDTO dto;

                //set sorting for each page
                foreach (int pageId in id)
                {
                    dto = db.Pages.Find(pageId);
                    dto.Sorting = count;

                    db.SaveChanges();

                    count++;
                }
            }
        }

        //GET: Admin/Pages/EditSidebar
       [HttpGet]
        public ActionResult EditSidebar()
        {
            //declare model
            SidebarVM model;

            using (Database db = new Database())
            {
                //get the dto
                SidebarDTO dto = db.Sidebar.Find(1);

                //initialize the model
                model = new SidebarVM(dto);
            }
            //return view with model
            return View(model);

        }

        //POST: Admin/Pages/EditSidebar
       [HttpPost]
        public ActionResult EditSidebar(SidebarVM model)
        {
            using (Database db = new Database())
            {
                //get the dto
                SidebarDTO dto = db.Sidebar.Find(1);

                //dto the body
                dto.Body = model.Body;

                //save
                db.SaveChanges();
            }

            //set the temp data message
            TempData["SM"] = "You Have Edited The Sidebar";

            //redirect  
            return RedirectToAction("EditSidebar");
        }
    }
}