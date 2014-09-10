using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Lib.Shared
{
    public class Constants
    {
        public const string None = "NONE";
        public const string Todo = "To-do";
        public const int TodoTypeID = 2;
        public const string UnassignedUser = "Unassigned";

        //messages
        public const string ContactUpdateSuccess = "Successfully updated ACT";
        public const string ContactRemoveSuccess = "Successfully deleted inquiry";
        public const string ContactUpdateError = "Error in updating ACT. ";
        public const string ActivityRemoveSuccess = "Successfully deleted Activity";
        public const string ActivityRemoveError = "Error in deleting Activity";
    }
}