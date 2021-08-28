using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Bll;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Enums;
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
        BllUser bllUser = new BllUser();

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
        public ServiceResponse ChangeStatusAdmin(ExhibitionModel exhibitionModel)
        {
            try
            {
                if (exhibitionModel.Id != 0)
                {
                    RequestAdmin dbExhibition = BllRequestAdmin.GetByPK(exhibitionModel.Id);
                    var username = dbExhibition.Name + RandomString(5) + RandomNumber(1, dbExhibition.Name.Length);
                    if (bllUser.GetByUserName(username) != null)
                    {
                        result.IsSucceeded = false;
                        result.Message = "UserName is already exists.";
                        return result;
                    }
                    User dbUser = new User();
                    dbUser.FirstName = dbExhibition.Name;
                    dbUser.LastName = dbExhibition.Name;
                    dbUser.UserName = username;
                    dbUser.Email = dbExhibition.Name;
                    dbUser.Password = RandomPassword();
                    dbUser.Telephone = dbExhibition.Telephone;
                    dbUser.CNIC = dbExhibition.Telephone;
                    dbUser.CreatedDate = DateTime.Now;
                    dbUser.UserType = Convert.ToInt32(UserRoleType.Organizer);
                    dbUser.GenderType = Convert.ToInt32(GenderType.Male);
                    int UserId = bllUser.Insert(dbUser);
                    result.IsSucceeded = true;
                    result.Message = "Status is updated to " + exhibitionModel.ExhibitionStatusStr + " and user is created successfully";
                    BllRequestAdmin.Delete(exhibitionModel.Id);
                }
            }
            catch (Exception e)
            {
                result.IsSucceeded = false;
                result.Message = e.Message + "<br>" + e.StackTrace;
            }
            return result;
        }
        public ServiceResponse ChangeStatus(ExhibitionModel exhibitionModel)
        {
            try
            {
                if (exhibitionModel.Id != 0)
                {
                    RequestOrganizer dbExhibition = bllRequestOrganizer.GetByPK(exhibitionModel.Id);

                    ExhibitionStatus status = (ExhibitionStatus)Enum.Parse(typeof(ExhibitionStatus), exhibitionModel.ExhibitionStatusStr);
                    dbExhibition.Status = Convert.ToInt32(status);

                    bllRequestOrganizer.Update(dbExhibition);
                    var username = dbExhibition.Name + RandomString(5) + RandomNumber(1, dbExhibition.Name.Length);
                    if (bllUser.GetByUserName(username) != null)
                    {
                        result.IsSucceeded = false;
                        result.Message = "UserName is already exists.";
                        return result;
                    }
                    User dbUser = new User();
                    dbUser.FirstName = dbExhibition.Name;
                    dbUser.LastName = dbExhibition.Name;
                    dbUser.UserName = username;
                    dbUser.Email = dbExhibition.WebsiteLink;
                    dbUser.Password = RandomPassword();
                    dbUser.Telephone = dbExhibition.Telephone;
                    dbUser.CNIC = dbExhibition.Telephone;
                    dbUser.CreatedDate = DateTime.Now;
                    dbUser.UserType = Convert.ToInt32(UserRoleType.Exhibitor);
                    dbUser.GenderType = Convert.ToInt32(GenderType.Male);
                    int UserId = bllUser.Insert(dbUser);
                    result.IsSucceeded = true;
                    result.Message = "Status is updated to " + exhibitionModel.ExhibitionStatusStr + " and user is created successfully";
                    bllRequestOrganizer.Delete(exhibitionModel.Id);
                }
            }
            catch (Exception e)
            {
                result.IsSucceeded = false;
                result.Message = e.Message + "<br>" + e.StackTrace;
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
            dbUser.sponsorList = vm.Image.FileName;
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
            dbUser.BrandImage = vm.Image.FileName;
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

            result.IsSucceeded = true;
            result.TotalCount = UserId;
            result.Message = "Request is successfully submitted to concerned organizer";
            return result;
        }
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }
        private readonly Random _random = new Random();
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
