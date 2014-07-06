using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTIS.Models
{
    public class ContactViewModel : ContactModel
    {
        public int ContactID { get; set; }
        public bool DuplicateInd { get; set; }
        public DateTime CreateDate { get; set; }

        public List<SelectListItem> ActionOptions = new List<SelectListItem>{
            new SelectListItem() { Text = "ADD TO DATABASE", Value = ContactOption.Import.ToString() },
            new SelectListItem() { Text = "ADD TO EXISTING CONTACT", Value = ContactOption.Update.ToString() },
            new SelectListItem() { Text = "DELETE INQUIRY", Value = ContactOption.Remove.ToString() }
        };

        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> ActivityTypes { get; set; }
    }

    public enum ContactOption
    {
        Import = 1,
        Update = 2,
        Remove = 3,
        NONE = 4
    }
}