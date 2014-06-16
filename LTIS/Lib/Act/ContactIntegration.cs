using Act.Framework;
using Act.Framework.Contacts;
using Act.Framework.Notes;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LTIS.Lib.Act
{
    public class ContactIntegration
    {
        public static void InsertContact(ContactModel model)
        {
            using (ActFramework ACTFM = new ActFramework())
            {
                string actPad = ConfigurationManager.AppSettings["ActPad"];
                string actUserName = ConfigurationManager.AppSettings["ActUserName"];
                string actPassword = ConfigurationManager.AppSettings["ActPassword"];

                ACTFM.LogOn(actPad, actUserName, actPassword);
                //Contact actContact = ACTFM.Contacts.GetMyRecord(); 
                Contact actContact = ACTFM.Contacts.CreateContact();
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
                    Note actNote = ACTFM.Notes.CreateNote(noteType, model.Notes, DateTime.Now, false, actContact);
                    actNote.Update();
                }
            }
            

        }
    }
}