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
        public ServiceResponse SaveUser(UserModel user)
        {
            try
            {
               
                if (bllUser.GetByPK(user.Id) == null)
                {
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
                    result.IsSucceeded = true;
                    result.TotalCount = UserId;
                    result.Message = "User is Successfully Created";

                }
                else
                {
                    User dbUser = bllUser.GetByPK(user.Id);

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
        public ServiceResponse SaveAdmin(UserModel user)
        {
            try
            {

                if (bllUser.GetByPK(user.Id) == null)
                {
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
                    dbUser.CNIC = user.CNIC;
                    dbUser.CreatedDate = DateTime.Now;
                    GenderType gender = (GenderType)Enum.Parse(typeof(GenderType), user.GenderTypename);
                    UserRoleType usertyp = (UserRoleType)Enum.Parse(typeof(UserRoleType), user.UserTypeName);
                    dbUser.UserType = Convert.ToInt32(usertyp);
                    dbUser.GenderType = Convert.ToInt32(gender);


                    int UserId = bllUser.Insert(dbUser);
                    result.IsSucceeded = true;
                    result.TotalCount = UserId;
                    result.Message = "Admin is Successfully Created";

                }
                else
                {
                    User dbUser = bllUser.GetByPK(user.Id);

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
        public ServiceResponse SaveExhibitor(ExhibitorDescriptionModel user)
        {
            try
            {

                if (bllUser.GetByPK(user.Id) == null)
                {
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


    }
}



/*
BllUser bllUser = new BllUser();
private ServiceResponse result = new ServiceResponse { IsSucceeded = true };

*/
