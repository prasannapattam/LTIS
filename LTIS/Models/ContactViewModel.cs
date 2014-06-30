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
            new SelectListItem() { Value = Convert.ToString(ContactOption.Import), Text = ContactOption.Import.ToString() },
            new SelectListItem() { Value = Convert.ToString(ContactOption.Update), Text = ContactOption.Update.ToString() },
            new SelectListItem() { Value = Convert.ToString(ContactOption.Remove), Text = ContactOption.Remove.ToString() },
            new SelectListItem() { Value = Convert.ToString(ContactOption.NONE), Text = ContactOption.NONE.ToString() }
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