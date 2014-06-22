using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Models
{
    public class ContactUpdateOption
    {
        public int ContactID { get; set; }
        public ContactOption Option { get; set; }
    }

    public enum ContactOption
    {

    }
}