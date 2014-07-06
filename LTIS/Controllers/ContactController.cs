using LTIS.Lib.Act;
using LTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LTIS.Lib.Repository;
using LTIS.Lib.Domain;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace LTIS.Controllers
{
    public class ContactController : ApiController
    {
        public async Task<string> Post()
        {
            ContactModel model = new ContactModel();

            string root = HttpContext.Current.Server.MapPath("~/Data");
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            //getting the user details
            model.FirstName = provider.FormData["FirstName"];
            model.LastName = provider.FormData["LastName"];
            model.Organization = provider.FormData["Organization"];
            model.EmailAddress = provider.FormData["EmailAddress"];
            model.StreetAddress = provider.FormData["StreetAddress"];
            model.Address2 = provider.FormData["Address2"];
            model.City = provider.FormData["City"];
            model.State = provider.FormData["State"];
            model.Zip = provider.FormData["Zip"];
            model.Phone = provider.FormData["Phone"];
            model.Notes = provider.FormData["Notes"];
            model.Notes = model.Notes == null ? "" : model.Notes.Replace("\r\n", "\n");


            if (provider.FileData.Count > 0)
            {
                MultipartFileData fileData = provider.FileData[0];
                FileInfo fi = new FileInfo(fileData.LocalFileName);

                //getting the file saving path
                string clientFileName = fileData.Headers.ContentDisposition.FileName.Replace(@"""", "");
                if (clientFileName != "")
                {
                    string clientExtension = clientFileName.Substring(clientFileName.LastIndexOf('/'));
                    model.AttachmentUrl = Guid.NewGuid().ToString() + clientExtension.Replace('/', '.').Replace("jpeg", "jpg");
                    string serverFileName = fi.DirectoryName + @"\" + model.AttachmentUrl;
                    fi.MoveTo(serverFileName);
                }
                else
                {
                    if (fi.Exists)
                        fi.Delete();
                }
            }
            

            //checking for duplicates
            bool duplicateInd = ContactDomain.ContactExists(model.EmailAddress);
            //bool duplicateInd = false;

            //saving the contact to database
            LTRepository.ContactAdd(model, duplicateInd);
            return "success";
        }
    }
}