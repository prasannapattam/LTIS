using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Models
{
    public class ContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string EmailAddress { get; set; }
        public string StreetAddress { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        public string Notes { get; set; }
        public string AttachmentUrl { get; set; }

        public ContactOption Action { get; set; }
        public string SalesRep { get; set; }
        public string Task { get; set; }

    }
}