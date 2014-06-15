using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTIS.Controllers
{
    public class ContactController : ApiController
    {
        public string Post([FromBody] ContactModel model)
        {
            return "success Body - " + model.FirstName + " - " + model.LastName;
        }
    }
}