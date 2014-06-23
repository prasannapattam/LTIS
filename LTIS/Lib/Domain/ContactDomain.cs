using LTIS.Lib.Act;
using LTIS.Lib.Repository;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTIS.Lib.Domain
{
    public static class ContactDomain
    {
        public static void UpdateContacts(List<ContactViewModel> contacts)
        {
            //getting all the contacts
            var dbContacts = LTRepository.ContactGetAll();

            foreach (var contact in contacts)
            {
                var dbContact = dbContacts.Find(m => m.ContactID == contact.ContactID);
                if (contact.SelectedOption == ContactOption.Import)
                    ContactIntegration.InsertContact(dbContact);
                else if (contact.SelectedOption == ContactOption.Update)
                    ContactIntegration.UpdateContact(dbContact);

            }

            //deleting the contacts
            int[] contactids = (from c in contacts
                                 where c.SelectedOption != ContactOption.Later
                                 select c.ContactID).ToArray();
            if (contactids.Length > 0)
                LTRepository.ContactDelete(contactids);
        }
    }
}