using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Bll;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Filters;
using VirtualExpo.Model.Services;

namespace VirtualExpo.Web.APIController
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class RequestAdminApiController : Controller
    {
        private IWebHostEnvironment _environment;

        public RequestAdminApiController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        BllRequestAdmin BllRequestAdmin = new BllRequestAdmin();

        ServiceResponse result = new ServiceResponse();
        [HttpPost]
        public ServiceResponse GetAdminRequestWithCount(RequestAdminFilter filter)
        {
            try
            {
                var lst = BllRequestAdmin.Search(filter);
                result.Message = lst;
                result.TotalCount = BllRequestAdmin.GetSearchCount(filter);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        BllRequestOrganizer bllRequestOrganizer = new BllRequestOrganizer();
        [HttpPost]
        public ServiceResponse GetOrganizerRequestWithCount(RequestOrganizerFilter filter)
        {
            try
            {
                var lst = bllRequestOrganizer.Search(filter);
                result.Message = lst;
                result.TotalCount = bllRequestOrganizer.GetSearchCount(filter);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public ServiceResponse RequestOrganizer(RequestOrganizerModel user)
        {
            try
            {
                RequestOrganizer dbUser = new RequestOrganizer();
                dbUser.Name = user.Name;
                dbUser.Description = user.Description;
                dbUser.WebsiteLink = user.WebsiteLink;
                dbUser.Email = user.Email;
                dbUser.Telephone = user.Telephone;
                dbUser.ExhibitionId = user.ExhibitionId;
                int UserId = bllRequestOrganizer.Insert(dbUser);
                result.IsSucceeded = true;
                result.TotalCount = UserId;
                result.Message = "Request is successfully submitted to concerned organizer";
            }
            catch (Exception ex)
            {

                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public async Task<ServiceResponse> RequestAdmin(RequestAdminModel vm)
        {

            string fileExtentsion = System.IO.Path.GetExtension(vm.Image.FileName).ToLower();
            if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
            {
                result.IsSucceeded = false;
                result.Message = "Please Upload Image type object ";
            }


            RequestAdmin dbUser = new RequestAdmin();
            dbUser.Name = vm.Name;
            dbUser.Email = vm.Email;
            dbUser.Telephone = vm.Telephone;
            dbUser.CreatedDate = DateTime.Now;
            dbUser.DateFrom = vm.DateFrom;
            dbUser.DateTo = vm.DateTo;
            dbUser.Expected_Number_Of_Exhibitor = vm.Expected_Number_Of_Exhibitor;
            dbUser.Link = vm.Link;
            dbUser.Location = vm.Location;
            dbUser.moto = vm.moto;
            dbUser.NameOfEvent = vm.NameOfEvent;
            int UserId = BllRequestAdmin.Insert(dbUser);

            var uploads = Path.Combine(_environment.WebRootPath, "images/Requests/RequestRequestAdmin/" + UserId);

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            if (vm.Image.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, vm.Image.FileName), FileMode.Create))
                {
                    await vm.Image.CopyToAsync(fileStream);
                }
            }
            RequestAdmin UploadedOrganizer = BllRequestAdmin.GetByPK(UserId);
            UploadedOrganizer.sponsorList = vm.Image.FileName;
            BllRequestAdmin.Update(UploadedOrganizer);

            result.IsSucceeded = true;
            result.TotalCount = UserId;
            result.Message = "Request is successfully submitted to Admin";
            return result;
        }
        [HttpPost]
        public async Task<ServiceResponse> UploadPic(RequestOrganizerModel vm)
        {

            string fileExtentsion = System.IO.Path.GetExtension(vm.Image.FileName).ToLower();
            if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
            {
                result.IsSucceeded = false;
                result.Message = "Please Upload Image type object ";
            }


            RequestOrganizer dbUser = new RequestOrganizer();
            dbUser.Name = vm.Name;
            dbUser.Description = vm.Description;
            dbUser.WebsiteLink = vm.WebsiteLink;
            dbUser.Email = vm.Email;
            dbUser.Telephone = vm.Telephone;
            dbUser.CreatedDate = DateTime.Now;
            dbUser.ExhibitionId = vm.ExhibitionId;
            int UserId = bllRequestOrganizer.Insert(dbUser);

            var uploads = Path.Combine(_environment.WebRootPath, "images/Requests/RequestOrganizer/" + UserId);

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            if (vm.Image.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, vm.Image.FileName), FileMode.Create))
                {
                    await vm.Image.CopyToAsync(fileStream);
                }
            }
            RequestOrganizer UploadedOrganizer = bllRequestOrganizer.GetByPK(UserId);
            UploadedOrganizer.BrandImage = vm.Image.FileName;
            bllRequestOrganizer.Update(UploadedOrganizer);

            result.IsSucceeded = true;
            result.TotalCount = UserId;
            result.Message = "Request is successfully submitted to concerned organizer";
            return result;
        }
    }
}
