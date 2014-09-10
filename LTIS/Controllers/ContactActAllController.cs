using LTIS.Lib.Domain;
using LTIS.Lib.Shared;
using LTIS.Models;
using System;
using System.Net.Http;
using System.Web.Http;

namespace LTIS.Controllers
{
    public class ContactActAllController : ApiController
    {
        public AjaxModel<string> Post()
        {
            AjaxModel<string> ajax = null;

            try
            {
                ContactDomain.UpdateContacts();
                ajax = new AjaxModel<string>() { Success = true, Message = Constants.ContactUpdateSuccess, Model = null };
            }
            catch (Exception exp)
            {
                ajax = new AjaxModel<string>() { Success = false, Message = Constants.ContactUpdateError + exp.Message, Model = null };
            }
            return ajax;
        }
    }
}
