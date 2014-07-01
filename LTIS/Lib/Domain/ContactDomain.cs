using Act.Framework.Activities;
using Act.Framework.Contacts;
using LTIS.Lib.Act;
using LTIS.Lib.Repository;
using LTIS.Lib.Shared;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActContact = Act.Framework.Contacts.Contact;
using DbContact = LTIS.Lib.Repository.Contact;

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
                    if (dbContact != null && (contact.Action == ContactOption.Import || contact.Action == ContactOption.Update))
                    {
                        dbContact.Action = contact.Action;
                        dbContact.SalesRep = contact.SalesRep;
                        dbContact.Task = contact.Task;
                        ContactIntegration.CreateUpdateContact(dbContact, act.Framework);
                    }
                }
            }

            //deleting the contacts
            int[] contactids = (from c in contacts
                                where c.Action != ContactOption.NONE
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

        public static List<SelectListItem> GetUsers()
        {
            using (ACTConnection act = new ACTConnection())
            {
                ContactList contacts = ContactIntegration.GetUsers(act.Framework);

                var users = (from c in contacts
                             orderby ((ActContact)c).FullName
                            select new SelectListItem
                            {
                                Text = ((ActContact)c).FullName,
                                Value = c.ID.ToString()
                            }).ToList();

                //getting the current contact
                ActContact current = ContactIntegration.GetCurrentUser(act.Framework);
                users.Find(m => m.Value == current.ID.ToString()).Selected = true;

                return users;
            }
        }

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

        public static List<ContactViewModel> GetUIContacts()
        {
            var model = LTRepository.ContactGetAll();

            //getting users
            var users = ContactDomain.GetUsers();
            users.Add(new SelectListItem() { Text = Constants.None, Value = Constants.None });

            var types = ContactDomain.GetActivityTypes();

            //checking for duplicates & setting or removing update
            foreach (var contact in model)
            {
                if (contact.DuplicateInd)
                    contact.ActionOptions[1].Selected = true;
                else
                    contact.ActionOptions.RemoveAt(1);

                contact.Users = users;
                contact.ActivityTypes = types;
            }

            return model;
        }
    }
}