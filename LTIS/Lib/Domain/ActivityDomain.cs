using Act.Framework.Activities;
using LTIS.Lib.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTIS.Lib.Domain
{
    public class ActivityDomain
    {
        public static List<SelectListItem> GetActivityTypes()
        {
            using (ACTConnection act = new ACTConnection())
            {
                ActivityType[] activityTypes = ContactIntegration.GetActivityTypes(act.Framework);

                var types = (from t in activityTypes
                             select new SelectListItem
                             {
                                 Text = t.Name,
                                 Value = t.ActivityTypeId.ToString()
                             }).ToList();

                return types;
            }
        }

    }
}