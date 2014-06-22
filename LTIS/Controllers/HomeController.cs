using LTIS.Lib.Repository;
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
            //getting all contacts
            var model = LTRepository.ContactGetAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            ViewBag.Message = "Successfully updated ACT";
            var model = LTRepository.ContactGetAll();
            return View(model);
        }
	}
}