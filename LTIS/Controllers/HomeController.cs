using LTIS.Lib.Domain;
using LTIS.Lib.Repository;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTIS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.Message = "";
            //getting all contacts
            var model = LTRepository.ContactGetAll();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(List<ContactViewModel> contacts)
        {
            ViewBag.Message = "Successfully updated ACT";

            ContactDomain.UpdateContacts(contacts);

            var model = LTRepository.ContactGetAll();
            return View(model);
        }
	}
}