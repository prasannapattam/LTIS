using LTIS.Lib.Domain;
using LTIS.Lib.Shared;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTIS.Controllers
{
    public class ContactActController : ApiController
    {
        public AjaxModel<string> Post([FromBody] ContactViewModel model)
        {
            AjaxModel<string> ajax = null;

            //ProfileModel profile = PosRepository.ProfileGet(model);
            try
            {
                ContactDomain.UpdateContact(model);
                ajax = new AjaxModel<string>() { Success = true, Message = Constants.ContactUpdateSuccess, Model = null };
                if (model.Action == ContactOption.Remove)
                    ajax.Message = Constants.ContactRemoveSuccess;
            }
            catch(Exception exp)
            {
                ajax = new AjaxModel<string>() { Success = false, Message = Constants.ContactUpdateError + exp.Message, Model = null };
            }
            return ajax;
        }
    }

}
