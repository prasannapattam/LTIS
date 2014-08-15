using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LTIS.Lib.Repository
{
    public static class LTRepository
    {
        public static void ContactAdd(ContactModel model, bool duplicateInd)
        {
            using (var db = new LTISEntities())
            {
                Contact contact = new Contact();
                contact.FirstName = model.FirstName;
                contact.LastName = model.LastName;
                contact.Organization = model.Organization;
                contact.EmailAddress = model.EmailAddress;
                contact.StreetAddress = model.StreetAddress;
                contact.Address2 = model.Address2;
                contact.City = model.City;
                contact.State = model.State;
                contact.Zip = model.Zip;
                contact.Phone = model.Phone;
                contact.Notes = model.Notes;
                contact.DuplicateInd = duplicateInd;
                contact.AttachmentUrl = model.AttachmentUrl;
                contact.CreateDate = DateTime.Now;
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
        }

        public static List<ContactViewModel> ContactGetAll()
        {
            using (var db = new LTISEntities())
            {
                var query = from contact in db.Contacts
                            orderby contact.CreateDate
                            select new ContactViewModel
                            {
                                ContactID = contact.ContactID,
                                FirstName = contact.FirstName,
                                LastName = contact.LastName,
                                Organization = contact.Organization,
                                EmailAddress = contact.EmailAddress,
                                StreetAddress = contact.StreetAddress,
                                Address2 = contact.Address2,
                                City = contact.City,
                                State = contact.State,
                                Zip = contact.Zip,
                                Phone = contact.Phone,
                                Notes = contact.Notes,
                                AttachmentUrl = contact.AttachmentUrl,
                                DuplicateInd = contact.DuplicateInd,
                                CreateDate = contact.CreateDate
                            };

                return query.ToList();
            }
        }

        public static ContactViewModel ContactGet(int contactID)
        {
            using (var db = new LTISEntities())
            {
                var query = from contact in db.Contacts
                            where contact.ContactID == contactID 
                            select new ContactViewModel
                            {
                                ContactID = contact.ContactID,
                                FirstName = contact.FirstName,
                                LastName = contact.LastName,
                                Organization = contact.Organization,
                                EmailAddress = contact.EmailAddress,
                                StreetAddress = contact.StreetAddress,
                                Address2 = contact.Address2,
                                City = contact.City,
                                State = contact.State,
                                Zip = contact.Zip,
                                Phone = contact.Phone,
                                Notes = contact.Notes,
                                AttachmentUrl = contact.AttachmentUrl,
                                DuplicateInd = contact.DuplicateInd,
                                CreateDate = contact.CreateDate
                            };

                return query.First();
            }
        }

        public static void ContactDelete(int[] contactids)
        {
            string sql = "DELETE FROM Contact WHERE ContactID in (" + String.Join(",", contactids) + ")";

            using (var db = new LTISEntities())
            {
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();
            }
        }

        public static void ContactDelete(int contactID)
        {
            using (var db = new LTISEntities())
            {
                var dbContact = from contact in db.Contacts where contact.ContactID == contactID select contact;
                db.Contacts.Remove(dbContact.First());
                db.SaveChanges();
            }
        }

        public static async Task<int> ContactCount()
        {
            using (var db = new LTISEntities())
            {
                var dbContact = from contact in db.Contacts select contact.ContactID;
                return dbContact.Count();
            }
        }
    }
}