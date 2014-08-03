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
        public static List<SelectListItem> GetActivities(Guid OrganizeUserId)
        {
            using (ACTConnection act = new ACTConnection())
            {
                
                ActivityList activities = ActivityIntegration.GetActivities(act.Framework, OrganizeUserId);

                List<SelectListItem> items = new List<SelectListItem>();

                foreach(Activity activity in activities)
                {
                    items.Add(new SelectListItem() { Text = activity.ScheduledWith, Value = activity.ActivityId.ToString() });
                }

                return items;
            }
        }
        public static void ClearActivity(Guid activityID)
        {
            using (ACTConnection act = new ACTConnection())
            {
                ActivityIntegration.ClearActivity(act.Framework, activityID);
            }
        }
    }

}