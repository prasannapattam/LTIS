using LTIS.Lib.Domain;
using LTIS.Lib.Shared;
using LTIS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LTIS.Controllers
{
    public class ActivitiesActController : ApiController
    {
        public AjaxModel<string> Post([FromBody] ActivitiesModel model)
        {
            AjaxModel<string> ajax = null;

            try
            {
                ActivityDomain.ClearActivity(new Guid(model.ActivityID));
                //ajax = new AjaxModel<string>() { Success = true, Message = Constants.ActivityRemoveSuccess, Model = JsonConvert.SerializeObject(model, Formatting.Indented) };
                ajax = new AjaxModel<string>() { Success = true, Message = Constants.ActivityRemoveSuccess, Model = model.ActivityID };
            }
            catch (Exception exp)
            {
                ajax = new AjaxModel<string>() { Success = false, Message = Constants.ContactUpdateError + exp.Message, Model = null };
            }
            return ajax;
        }

    }
}
