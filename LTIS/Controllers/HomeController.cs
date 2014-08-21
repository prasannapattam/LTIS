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
            //this action is not used. Instead of this LTISAct solution is used.
            return new HttpNotFoundResult();

            ViewBag.Message = "";
            //getting all contacts
            var model = ContactDomain.GetUIContacts();            
            return View(model);
        }

        public ActionResult Act()
        {
            ViewBag.Message = "";
            //getting all contacts
            var model = ContactDomain.GetUIContacts();
            return View(model);
        }

        public ActionResult Activities(string id)
        {
            //acttestser: 73256b16-ebed-43df-a3ca-38efb5d13c7b
            //prasannapattam: 02de3e18-2bd0-436f-8745-9176b0e4be8d

            Guid OrganizeUserId = new Guid(id);

            
            //List<SelectListItem> activities = ActivityDomain.GetActivities(OrganizeUserId);
            List<SelectListItem> activities = ActivityDomain.GetActivities(OrganizeUserId);

            //for testing cleaing the first activity
            //ActivityDomain.ClearActivity(new Guid(activities[0].Value));
            ViewData["LTIS_ACTUSERID"] = id;
         
            return View(activities);
        }

        [HttpPost] 
        public ActionResult GetActivitiesCount(string id)
        {
            //acttestser: 73256b16-ebed-43df-a3ca-38efb5d13c7b
            //prasannapattam: 02de3e18-2bd0-436f-8745-9176b0e4be8d

            Guid OrganizeUserId = new Guid(id);

            List<SelectListItem> activities = ActivityDomain.GetActivities(OrganizeUserId);
            return Json(activities.Count);
                        
        }
	}
}