using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Bll;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Enums;
using VirtualExpo.Model.Services;
using VirtualExpo.Models.Filters;

namespace VirtualExpo.Web.APIController
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AttendeeApiController : Controller
    {
        BllUser bllUser = new BllUser();
        BllEducation bllEducation = new BllEducation();
        BllWorkingExperience bllWorkingExperience = new BllWorkingExperience();

        ServiceResponse result = new ServiceResponse();
        [HttpPost]
        public ServiceResponse SaveAttendee(AttendeeModel user)
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
                    dbUser.UserType = Convert.ToInt32(UserRoleType.Attendee);
                    dbUser.GenderType = Convert.ToInt32(gender);


                    int UserId = bllUser.Insert(dbUser);

                    Education education = new Education();
                    education.DegreeName = user.DegreeName;
                    education.PassingYear = user.PassingYear;
                    education.Institute = user.Institute;
                    education.Grade = user.Grade;
                    education.Attendee_User_Id = UserId;
                    bllEducation.Insert(education);

                    WorkExperience workExperience = new WorkExperience();
                    workExperience.EmployeerName = user.EmployeerName;
                    workExperience.Designation = user.Designation;
                    workExperience.From = user.From;
                    workExperience.To = user.To;
                    workExperience.IndustryName = user.IndustryName;
                    workExperience.Location = user.Location;
                    WorkingStatus working = (WorkingStatus)Enum.Parse(typeof(WorkingStatus), user.WorkingStatusStr);
                    workExperience.WorkingStatus = Convert.ToInt32(working);
                    workExperience.Attendee_User_Id = UserId;
                    bllWorkingExperience.Insert(workExperience);

                    result.IsSucceeded = true;
                    result.TotalCount = UserId;
                    result.Message = "Attendee is Successfully Registered";

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

                    Education education = new Education();
                    education.DegreeName = user.DegreeName;
                    education.PassingYear = user.PassingYear;
                    education.Institute = user.Institute;
                    education.Grade = user.Grade;
                    education.Attendee_User_Id = dbUser.Id;
                    bllEducation.Insert(education);

                    WorkExperience workExperience = new WorkExperience();
                    workExperience.EmployeerName = user.EmployeerName;
                    workExperience.Designation = user.Designation;
                    workExperience.From = user.From;
                    workExperience.To = user.To;
                    workExperience.IndustryName = user.IndustryName;
                    workExperience.Location = user.Location;
                    WorkingStatus working = (WorkingStatus)Enum.Parse(typeof(WorkingStatus), user.WorkingStatusStr);
                    workExperience.WorkingStatus = Convert.ToInt32(working);
                    workExperience.Attendee_User_Id = dbUser.Id;
                    bllWorkingExperience.Insert(workExperience);
                    if (bllEducation.GetByAttendeeId(user.Id) != null)
                    {
                        bllEducation.Update(education);
                    }
                    else
                    {
                        bllEducation.Insert(education);
                    }
                    if (bllWorkingExperience.GetByAttendeeId(user.Id) != null)
                    {
                        bllWorkingExperience.Update(workExperience);
                    }
                    else
                    {
                        bllWorkingExperience.Insert(workExperience);
                    }
                    result.IsSucceeded = true;
                    result.Message = "Attendee is Successfully Updated";
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
            result.Message = "/Home/EventHome";
            return result;
        }
    }
}
