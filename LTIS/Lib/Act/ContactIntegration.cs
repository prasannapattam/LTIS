using Act.Framework;
using Act.Framework.Contacts;
using Act.Framework.Notes;
using Act.Framework.Lookups;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Act.Framework.SupplementalFiles;
using Act.Framework.Activities;
using LTIS.Lib.Shared;
using Act.Framework.Users;

namespace LTIS.Lib.Act
{
    public class ContactIntegration
    {
        public static void CreateUpdateContact(ContactModel model, ActFramework act)
        {
            Contact actContact = null;

            if (model.Action == ContactOption.Update)
            {
                //checking for the contact from email
                ContactList list = GetContactsFromEmail(model.EmailAddress, act);
                if (list.Count > 0)
                    actContact = list[0];
                else
                    actContact = act.Contacts.CreateContact();
            }
            else
            {
                actContact = act.Contacts.CreateContact();
            }

            SetContactAttributes(model, actContact);
            CreateNoteAndAttachment(model, act, actContact);

            if (model.SalesRep != Constants.None)
            {
                //setting record manager
                actContact.Fields["Contact.Record Manager", false] = new Guid(model.SalesRep);
                actContact.Update();
                CreateContactActivity(model, act, actContact);
            }
        }

        private static void SetContactAttributes(ContactModel model, Contact actContact)
        {
            actContact.FullName = model.FirstName + " " + model.LastName;
            actContact.Company = model.Organization;
            actContact.Fields["Contact.E-mail", false] = model.EmailAddress;
            actContact.Fields["Contact.Address 1", false] = model.StreetAddress;
            //actContact.Fields["Contact.Address 2", false] = model.Address2;
            actContact.Fields["Contact.City", false] = model.City;
            actContact.Fields["Contact.State", false] = model.State;
            actContact.Fields["Contact.ZIP Code", false] = model.Zip;
            actContact.Fields["Contact.Phone", false] = model.Phone;
            actContact.Update();
        }

        private static void CreateNoteAndAttachment(ContactModel model, ActFramework act, Contact actContact)
        {
            if (model.Notes != "" || model.AttachmentUrl != null)
            {

                //converting line breaks to what ACT understands
                model.Notes = "{\\rtf1 " + model.Notes.Replace("\n", "\\par\r\n") + "}";

                NoteType noteType = new NoteType(SystemNoteType.Note);
                Note actNote = act.Notes.CreateNote(noteType, model.Notes, DateTime.Now, false, actContact);

                if (model.AttachmentUrl != null)
                {
                    string attachmentUrl = HttpContext.Current.Server.MapPath("~/Data/" + model.AttachmentUrl);
                    actNote.Attachment = act.SupplementalFileManager.CreateAttachment(AttachmentMate.Note, attachmentUrl, "Attachment", false);
                }

                actNote.Update();
            }
        }

        private static void CreateContactActivity(ContactModel model, ActFramework act, Contact actContact)
        {
            ActivityType type = act.Activities.GetActivityType(Convert.ToInt32(model.Task));

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddMinutes(10);

            ActivityTemplate template = act.Activities.CreateActivity(type, startTime, endTime, new Guid(model.SalesRep));
            template.ActivityContacts.Add(actContact);
            template.Update();
        }

        public static ContactList GetContactsFromEmail(string email, ActFramework act)
        {
            CriteriaColumn actColumn = act.Lookups.GetCriteriaColumn("TBL_CONTACT", "BUSINESS_EMAIL", true);
            Criteria[] actCriteria = {
                    new Criteria(LogicalOperator.End, 0, 0, actColumn, OperatorEnum.EqualTo, email)
                };
            
            ContactLookup actLookup = act.Lookups.LookupContactsReplace(actCriteria, true, true);
            ContactList actContacts = actLookup.GetContacts(null);

            return actContacts;
        }

        public static User[] GetUsers(ActFramework act)
        {
            return act.Users.ActiveUsers;
        }

        public static Contact GetCurrentUser(ActFramework act)
        {
            return act.Contacts.GetMyRecord();
        }

        public static ActivityType[] GetActivityTypes(ActFramework act)
        {
            return act.Activities.GetActivityTypes();

        }
    }
}

