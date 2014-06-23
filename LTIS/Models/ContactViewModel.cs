using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Models
{
    public class ContactViewModel : ContactModel
    {
        public int ContactID { get; set; }
        public bool DuplicateInd { get; set; }
        public DateTime CreateDate { get; set; }
        public ContactOption SelectedOption { get; set; }

        public List<KeyValuePair<int, string>> options = new List<KeyValuePair<int, string>>{
            new KeyValuePair<int, string>(1, "Import"),
            new KeyValuePair<int, string>(2, "Update"),
            new KeyValuePair<int, string>(3, "Remove"),
            new KeyValuePair<int, string>(4, "Later")
        };
    }

    public enum ContactOption
    {
        Import = 1,
        Update = 2,
        Remove = 3,
        Later = 4
    }
}