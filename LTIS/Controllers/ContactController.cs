using LTIS.Lib.Act;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LTIS.Lib.Repository;

namespace LTIS.Controllers
{
    public class ContactController : ApiController
    {
        public string Post([FromBody] ContactModel model)
        {
            //ContactIntegration.InsertContact(model);

            //checking for duplicates
            bool duplicateInd = false;

            //saving the contact to database
            LTRepository.ContactAdd(model, duplicateInd);
            return "success";
        }
    }
}