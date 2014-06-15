﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Models
{
    public class ContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string Message { get; set; }
    }
}