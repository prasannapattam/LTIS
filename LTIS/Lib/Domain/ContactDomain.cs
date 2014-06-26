using Act.Framework.Contacts;
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

            using (ACTConnection act = new ACTConnection())
            {

                foreach (var contact in contacts)
                {
                    var dbContact = dbContacts.Find(m => m.ContactID == contact.ContactID);
                    if (contact.SelectedOption == ContactOption.Import)
                        ContactIntegration.InsertContact(dbContact, act.Framework);
                    else if (contact.SelectedOption == ContactOption.Update)
                        ContactIntegration.UpdateContact(dbContact, act.Framework);

                }
            }
            //deleting the contacts
            int[] contactids = (from c in contacts
                                 where c.SelectedOption != ContactOption.Later
                                 select c.ContactID).ToArray();
            if (contactids.Length > 0)
                LTRepository.ContactDelete(contactids);
        }

        public static bool ContactExists(string emailAddress)
        {
            using (ACTConnection act = new ACTConnection())
            {
                ContactList contacts = ContactIntegration.GetContactsFromEmail(emailAddress, act.Framework);

                return contacts.Count > 0;
            }
        }
    }
}