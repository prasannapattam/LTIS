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

namespace LTIS.Lib.Act
{
    public class ContactIntegration
    {
        public static void InsertContact(ContactModel model, ActFramework act)
        {
            //Contact actContact = ACTFM.Contacts.GetMyRecord(); 
            Contact actContact = act.Contacts.CreateContact();
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

            if (model.Notes != null && model.Notes != "")
            {
                NoteType noteType = new NoteType(SystemNoteType.Note);
                Note actNote = act.Notes.CreateNote(noteType, model.Notes, DateTime.Now, false, actContact);
                actNote.Update();
            }
        }

        public static void UpdateContact(ContactModel model, ActFramework act)
        {
            //getting the contact from email
            Contact actContact = GetContactsFromEmail(model.EmailAddress, act)[0];

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



            if (model.Notes != null && model.Notes != "")
            {

                //converting line breaks to what ACT understands

                model.Notes = "{\\rtf1 " + model.Notes.Replace("\r\n", "\n").Replace("\n", "\\par\r\n") + "}";

                NoteType noteType = new NoteType(SystemNoteType.Note);
                Note actNote = act.Notes.CreateNote(noteType, model.Notes, DateTime.Now, false, actContact);

//                    actNote.Attachment = act.Framework.SupplementalFileManager.CreateAttachment(AttachmentMate.Note, "", "", false);
                    

                actNote.Update();
            }
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

    }
}

