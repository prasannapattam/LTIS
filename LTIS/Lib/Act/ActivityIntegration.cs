using Act.Framework;
using Act.Framework.Activities;
using Act.Framework.CalendarDelegates;
using Act.Framework.Histories;
using LTIS.Lib.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Lib.Act
{
    public class ActivityIntegration
    {
        public static ActivityList GetActivities(ActFramework act, Guid OrganizeUserId)
        {
            // Create a new comparison filter criteria
            ActivityComparisonFilterCriteria[] criteria = {
                new ActivityComparisonFilterCriteria(new ActivityFieldDescriptor(ActivityField.OrganizeUserId), ComparisonFilterCriteria.Operation.Equals, OrganizeUserId),
                new ActivityComparisonFilterCriteria(new ActivityFieldDescriptor(ActivityField.IsCleared), ComparisonFilterCriteria.Operation.Equals, false),
                new ActivityComparisonFilterCriteria(new ActivityFieldDescriptor(ActivityField.TypeId), ComparisonFilterCriteria.Operation.Equals, Constants.TodoTypeID),
            };

            // Get min and max date range
            DateTime startRange = act.Activities.MIN_SMALL_DATE_TIME;
            DateTime endRange = act.Activities.MAX_SMALL_DATE_TIME;

   
            // Get the activity list
            ActivityList activities = act.Activities.GetActivityList(null, criteria, startRange, endRange);

            return activities;
        }

        public static void ClearActivity(ActFramework act, Guid activityID)
        {
            Activity activity = act.Activities.GetActivityInstancesByActivityId(new Guid[] { activityID })[0];

            //act.Activities.ClearActivity(activity)
            HistoryType historyType = act.Histories.GetHistoryType("Call Completed");
            DateTime dt = DateTime.Now;
            CalendarDelegateGrantor grantor = act.CalendarDelegates.GetCalendarDelegateGrantorByAccessorId(activity.AccessorId);

            act.Activities.ClearActivity(activity, grantor, historyType, false, dt, dt, "Closed by LTIS");
        }
    }
}