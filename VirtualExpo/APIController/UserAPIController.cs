using VirtualExpo.Bll;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualExpo.Model.Services;
using VirtualExpo.Models.Filters;
using VirtualExpo.Model.Enums;
using VirtualExpo.Model.Data;

namespace VirtualExpo.Web.APIController
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class UserAPIController : Controller
    {
        BllUser bllUser = new BllUser();
        private IWebHostEnvironment _environment;

        public UserAPIController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        ServiceResponse result = new ServiceResponse();
        [HttpPost]
        public ServiceResponse GetUsersWithCount(UserSearchFilter filter)
        {
            try
            {
                var lst = bllUser.Search(filter);
                result.Message = lst;
                result.TotalCount = bllUser.GetSearchCount(filter);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public ServiceResponse GetExhibitorsWithCount(UserSearchFilter filter)
        {
            try
            {
                var lst = bllUser.SearchExhibitors(filter);
                result.Message = lst;
                result.TotalCount = bllUser.GetSearchCountExhibitor(filter);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public ServiceResponse GetAttendeeWithCount(UserSearchFilter filter)
        {
            try
            {
                var lst = bllUser.SearchAttendee(filter);
                result.Message = lst;
                result.TotalCount = bllUser.GetSearchCountAttendee(filter);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public async Task<ServiceResponse> SaveUser(UserModel user)
        {
            try
            {
               
                if (bllUser.GetByPK(user.Id) == null)
                {
                    if (user.Image != null)
                    {
                        string fileExtentsion = System.IO.Path.GetExtension(user.Image.FileName).ToLower();
                        if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
                        {
                            result.IsSucceeded = false;
                            result.Message = "Please Upload Image type object ";
                        }
                    }
                    if (bllUser.GetByUserName(user.UserName) != null)
                    {
                        result.IsSucceeded = false;
                        result.Message = "UserName is already exists.";
                        return result;
                    }
                    User dbUser = new User();
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.UserName = user.UserName;
                    dbUser.Email = user.Email;
                    dbUser.Description = user.Description;
                    dbUser.Password = user.Password;
                    dbUser.Telephone = user.Telephone;
                    dbUser.CNIC = user.CNIC;
                    dbUser.CreatedDate = DateTime.Now;
                    GenderType gender = (GenderType)Enum.Parse(typeof(GenderType), user.GenderTypename);
                    UserRoleType usertyp = (UserRoleType)Enum.Parse(typeof(UserRoleType), user.UserTypeName);
                    dbUser.UserType = Convert.ToInt32(usertyp);
                    dbUser.GenderType = Convert.ToInt32(gender);


                    int UserId = bllUser.Insert(dbUser);


                    var uploads = Path.Combine(_environment.WebRootPath, "images/User/" + UserId);

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    if (user.Image.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, user.Image.FileName), FileMode.Create))
                        {
                            await user.Image.CopyToAsync(fileStream);
                        }
                    }

                    result.IsSucceeded = true;
                    result.TotalCount = UserId;
                    result.Message = "User is Successfully Created";

                }
                else
                {
                    User dbUser = bllUser.GetByPK(user.Id);
                    if (user.Image != null)
                    {
                        string fileExtentsion = System.IO.Path.GetExtension(user.Image.FileName).ToLower();
                        if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
                        {
                            result.IsSucceeded = false;
                            result.Message = "Please Upload Image type object ";
                        }
                        dbUser.Picture = user.Image.FileName;
                    }
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.UserName = user.UserName;
                    dbUser.Email = user.Email;
                    dbUser.Password = user.Password;
                    dbUser.Telephone = user.Telephone;
                    dbUser.Description = user.Description;
                    dbUser.CNIC = user.CNIC;
                    dbUser.CreatedDate = DateTime.Now;
                    GenderType gender = (GenderType)Enum.Parse(typeof(GenderType), user.GenderTypename);
                    dbUser.GenderType = Convert.ToInt32(gender);


                    bllUser.Update(dbUser);

                    if (user.Image != null)
                    {
                        var uploads = Path.Combine(_environment.WebRootPath, "images/User/" + user.Id);

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        if (user.Image.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, user.Image.FileName), FileMode.Create))
                            {
                                await user.Image.CopyToAsync(fileStream);
                            }
                        }
                    }
                    result.IsSucceeded = true;
                    result.Message = "User is Successfully Updated";
                }
            }
            catch (Exception ex)
            {

                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public async Task<ServiceResponse> SaveAdmin(UserModel user)
        {
            try
            {

                if (bllUser.GetByPK(user.Id) == null)
                {

                    string fileExtentsion = System.IO.Path.GetExtension(user.Image.FileName).ToLower();
                    if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
                    {
                        result.IsSucceeded = false;
                        result.Message = "Please Upload Image type object ";
                    }
                    if (bllUser.GetByUserName(user.UserName) != null)
                    {
                        result.IsSucceeded = false;
                        result.Message = "UserName is already exists.";
                        return result;
                    }

                    User dbUser = new User();
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.UserName = user.UserName;
                    dbUser.Email = user.Email;
                    dbUser.Password = user.Password;
                    dbUser.Telephone = user.Telephone;
                    dbUser.Picture = user.Image.FileName;
                    dbUser.CNIC = user.CNIC;
                    dbUser.CreatedDate = DateTime.Now;
                    GenderType gender = (GenderType)Enum.Parse(typeof(GenderType), user.GenderTypename);
                    UserRoleType usertyp = (UserRoleType)Enum.Parse(typeof(UserRoleType), user.UserTypeName);
                    dbUser.UserType = Convert.ToInt32(usertyp);
                    dbUser.GenderType = Convert.ToInt32(gender);
                    int UserId = bllUser.Insert(dbUser);
                    var uploads = Path.Combine(_environment.WebRootPath, "images/User/" + UserId);

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    if (user.Image.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, user.Image.FileName), FileMode.Create))
                        {
                            await user.Image.CopyToAsync(fileStream);
                        }
                    }

                    result.IsSucceeded = true;
                    result.TotalCount = UserId;
                    result.Message = "Admin is Successfully Created";

                }
                else
                {
                    User dbUser = bllUser.GetByPK(user.Id);

                    
                    if(user.Image != null)
                    {
                        string fileExtentsion = System.IO.Path.GetExtension(user.Image.FileName).ToLower();
                        if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
                        {
                            result.IsSucceeded = false;
                            result.Message = "Please Upload Image type object ";
                        }
                        dbUser.Picture = user.Image.FileName;
                    }
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.UserName = user.UserName;
                    dbUser.Email = user.Email;
                    dbUser.Password = user.Password;
                    dbUser.Telephone = user.Telephone;
                    dbUser.CNIC = user.CNIC;
                    dbUser.CreatedDate = DateTime.Now;
                    GenderType gender = (GenderType)Enum.Parse(typeof(GenderType), user.GenderTypename);
                    dbUser.GenderType = Convert.ToInt32(gender);
                    bllUser.Update(dbUser);
                    if (user.Image!=null)
                    {
                        var uploads = Path.Combine(_environment.WebRootPath, "images/User/" + user.Id);

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        if (user.Image.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, user.Image.FileName), FileMode.Create))
                            {
                                await user.Image.CopyToAsync(fileStream);
                            }
                        }
                    }
                    
                    result.IsSucceeded = true;
                    result.Message = "Admin is Successfully Updated";
                }
            }
            catch (Exception ex)
            {

                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public async Task<ServiceResponse> SaveExhibitor(ExhibitorDescriptionModel user)
        {
            try
            {

                if (bllUser.GetByPK(user.Id) == null)
                {
                    if (user.Image != null)
                    {
                        string fileExtentsion = System.IO.Path.GetExtension(user.Image.FileName).ToLower();
                        if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
                        {
                            result.IsSucceeded = false;
                            result.Message = "Please Upload Image type object ";
                        }
                    }
                    if (bllUser.GetByUserName(user.UserName) != null)
                    {
                        result.IsSucceeded = false;
                        result.Message = "UserName is already exists.";
                        return result;
                    }
                    User dbUser = new User();
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.UserName = user.UserName;
                    dbUser.Email = user.Email;
                    dbUser.Description = user.Description;
                    dbUser.Password = user.Password;
                    dbUser.Telephone = user.Telephone;
                    dbUser.CNIC = user.CNIC;
                    dbUser.CreatedDate = DateTime.Now;
                    GenderType gender = (GenderType)Enum.Parse(typeof(GenderType), user.GenderTypename);
                    UserRoleType usertyp = (UserRoleType)Enum.Parse(typeof(UserRoleType), user.UserTypeName);
                    dbUser.UserType = Convert.ToInt32(usertyp);
                    dbUser.GenderType = Convert.ToInt32(gender);


                    int UserId = bllUser.Insert(dbUser);

                    var uploads = Path.Combine(_environment.WebRootPath, "images/User/" + UserId);

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    if (user.Image.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, user.Image.FileName), FileMode.Create))
                        {
                            await user.Image.CopyToAsync(fileStream);
                        }
                    }


                    ExhibitorDescription exhibitorDescription = new ExhibitorDescription();
                    exhibitorDescription.Name = user.Name;
                    exhibitorDescription.Moto = user.Moto;
                    exhibitorDescription.Offer = user.Offer;
                    exhibitorDescription.Exibition_id = user.ExhibitionId;
                    exhibitorDescription.UserId = UserId;
                    BllExhibitorDescription bllExhibitorDescription = new BllExhibitorDescription();
                    bllExhibitorDescription.Insert(exhibitorDescription);
                    result.IsSucceeded = true;
                    result.TotalCount = UserId;
                    result.Message = "User is Successfully Created";

                }
                else
                {

                    User dbUser = bllUser.GetByPK(user.Id);

                    if (user.Image != null)
                    {
                        string fileExtentsion = System.IO.Path.GetExtension(user.Image.FileName).ToLower();
                        if (fileExtentsion != ".png" && fileExtentsion != ".jpg" && fileExtentsion != ".jpeg" && fileExtentsion != ".gif")
                        {
                            result.IsSucceeded = false;
                            result.Message = "Please Upload Image type object ";
                        }
                        dbUser.Picture = user.Image.FileName;
                    }

                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.UserName = user.UserName;
                    dbUser.Email = user.Email;
                    dbUser.Password = user.Password;
                    dbUser.Telephone = user.Telephone;
                    dbUser.Description = user.Description;
                    dbUser.CNIC = user.CNIC;
                    dbUser.CreatedDate = DateTime.Now;
                    GenderType gender = (GenderType)Enum.Parse(typeof(GenderType), user.GenderTypename);
                    dbUser.GenderType = Convert.ToInt32(gender);
                    bllUser.Update(dbUser);
                    if (user.Image != null)
                    {
                        var uploads = Path.Combine(_environment.WebRootPath, "images/User/" + user.Id);

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        if (user.Image.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, user.Image.FileName), FileMode.Create))
                            {
                                await user.Image.CopyToAsync(fileStream);
                            }
                        }
                    }
                    ExhibitorDescription exhibitorDescription = new ExhibitorDescription();
                    exhibitorDescription.Name = user.Name;
                    exhibitorDescription.Moto = user.Moto;
                    exhibitorDescription.Offer = user.Offer;
                    exhibitorDescription.Exibition_id = user.ExhibitionId;
                    exhibitorDescription.UserId = user.Id;
                    BllExhibitorDescription bllExhibitorDescription = new BllExhibitorDescription();
                    if(bllExhibitorDescription.GetByUserid(user.Id)!=null)
                    {
                        bllExhibitorDescription.Update(exhibitorDescription);
                    }
                    else
                    {
                        bllExhibitorDescription.Insert(exhibitorDescription);
                    }
                    result.IsSucceeded = true;
                    result.Message = "User is Successfully Updated";
                }
            }
            catch (Exception ex)
            {

                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }


        [HttpPost]
        public ServiceResponse Logout(UserSearchFilter type)
        {
            HttpContext.SignOutAsync(type.Keyword);
            result.Message = "/Home/Login";
            return result;
        }

        [HttpPost]
        public ServiceResponse GetUserImage(User filter)
        {
            try
            {
                User category = new User();
                category = bllUser.GetByPK(filter.Id);
                if (category == null)
                {
                    return result;
                }
                if (category.Picture != null)
                {
                    result.TotalCount = 1;
                }
                //result.Message = bllProductImage.GetProductImagesByProductId(Convert.ToInt32(filter.Product_Id));
                result.Message = category;
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        public ServiceResponse DeletePicture(User model)
        {
            try
            {
                User dbsetting = bllUser.GetByPK(model.Id);
                var directoryPath = Path.Combine(_environment.WebRootPath, "images/User/"+dbsetting.Id+"/"+ dbsetting.Picture);

                if (!bllUser.DeletePicture(model.Id))
                {
                    result.IsSucceeded = false;
                    result.Message = "Admin Image is not found.";
                    return result;
                }

                System.IO.File.Delete(directoryPath);
                dbsetting.Picture = null;
                bllUser.Update(dbsetting);
                result.IsSucceeded = true;
                result.Message = "Admin Picture has been successfully deleted.";
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}



/*
BllUser bllUser = new BllUser();
private ServiceResponse result = new ServiceResponse { IsSucceeded = true };

*/
