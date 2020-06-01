using E_CommerceProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_CommerceProject.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string Rolename)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
            IdentityRole role = new IdentityRole();
            role.Name = Rolename;
            manager.Create(role);
            return RedirectToAction("Index", "Home");
        }


        // GET: Role
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(string Rolename)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
            IdentityRole role = new IdentityRole();
            role.Name = Rolename;
            manager.Delete(role);
            return RedirectToAction("Admin", "Dashboard");
        }
    }
}