﻿using Act.Framework.Activities;
using Act.Framework.Contacts;
using Act.Framework.Users;
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
        public static void UpdateContacts()
        {
            //getting all the contacts
            var dbContacts = LTRepository.ContactGetAll();

            using (ACTConnection act = new ACTConnection())
            {

                var task = ContactIntegration.GetActivityType(act.Framework, Constants.Todo).ActivityTypeId.ToString();
                User rep = act.Framework.Users.GetUser(Constants.UnassignedUser);
                string salesRep = Constants.None;
                if (rep != null)
                    salesRep = rep.ID.ToString();

                foreach (var dbContact in dbContacts)
                {
                    dbContact.Action = dbContact.DuplicateInd ? ContactOption.Update : ContactOption.Import;
                    dbContact.SalesRep = salesRep;
                    dbContact.Task = task;
                    ContactIntegration.CreateUpdateContact(dbContact, act.Framework);
                }
            }

            //deleting the contacts
            int[] contactids = (from c in dbContacts
                                select c.ContactID).ToArray();
            if (contactids.Length > 0)
                LTRepository.ContactDelete(contactids);
        }

        public static void UpdateContact(ContactViewModel contact)
        {
            var dbContact = LTRepository.ContactGet(contact.ContactID);
            dbContact.Action = contact.Action;
            dbContact.SalesRep = contact.SalesRep;
            dbContact.Task = contact.Task;
            if (contact.Action == ContactOption.Import || contact.Action == ContactOption.Update)
            {
                using (ACTConnection act = new ACTConnection())
                {
                    //setting the default task if no task is passed
                    if (String.IsNullOrEmpty(contact.Task))
                    {
                        dbContact.Task = ContactIntegration.GetActivityType(act.Framework, Constants.Todo).ActivityTypeId.ToString();
                    }
                    else
                    {
                        dbContact.Task = contact.Task;
                    }
                    ContactIntegration.CreateUpdateContact(dbContact, act.Framework);
                }
            }

            if (contact.Action != ContactOption.NONE)
                LTRepository.ContactDelete(contact.ContactID);
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
                User[] actUsers = ContactIntegration.GetUsers(act.Framework);

                var users = (from u in actUsers
                             orderby u.DisplayName
                            select new SelectListItem
                            {
                                Text = u.DisplayName,
                                Value = u.ID.ToString()
                            }).ToList();

                //getting the current contact
                //ActContact current = ContactIntegration.GetCurrentUser(act.Framework);
                //users.Find(m => m.Value == current.ID.ToString()).Selected = true;

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
            users.Insert(0, new SelectListItem() { Text = "No", Value = Constants.None });
            users.Insert(0, new SelectListItem() { Text = "[SELECT]", Value = "" });
            users[0].Selected = true;

            var types = ContactDomain.GetActivityTypes();

            //checking for duplicates & setting or removing update
            foreach (var contact in model)
            {
                if (contact.DuplicateInd)
                {
                    contact.ActionOptions[1].Selected = true;
                    contact.Action = ContactOption.Update;
                }
                else
                {
                    contact.ActionOptions.RemoveAt(1);
                    contact.ActionOptions[0].Selected = true;
                    contact.Action = ContactOption.Import;
                }

                contact.Users = users;
                contact.ActivityTypes = types;
                var todoSelectItem = types.Find(t => t.Text == Constants.Todo);
                contact.Task = todoSelectItem != null ? todoSelectItem.Value : types[0].Value;
            }

            return model;
        }
    }
}