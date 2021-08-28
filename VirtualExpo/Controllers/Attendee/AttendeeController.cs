using VirtualExpo.Bll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using Microsoft.AspNetCore.Authorization;

namespace VirtualExpo.Web.Controllers.Admin
{
    [Authorize(Roles = "Attendee", AuthenticationSchemes = "Attendee")]
    public class AttendeeController : Controller
    {
        public IActionResult UserIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Users/Index.cshtml");
        }
        public IActionResult Dashboard()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Dashboard/Index.cshtml");
        }
        public IActionResult ExhibitionHome(int id)
        {
            ViewBag.ExibitionId = id;
            return View("Views/ExpoHome/Exhibition/ExhibitionHome/ExibitionHome.cshtml");
        }
        public IActionResult ExpoOrganizer(int id, int OrganizerId)
        {
            ViewBag.OrganizerId = OrganizerId;
            ViewBag.ExibitionId = id;
            return View("Views/ExpoHome/Exhibition/Profile/OrganizerProfile.cshtml");
        }
        public IActionResult ExpoBrands(int id)
        {
            var idof = User.Identity.Name;
            ViewBag.ExibitionId = id;
            return View("Views/ExpoHome/Exhibition/ExpoBrands/Index.cshtml");
        }
        public IActionResult ExpoInfo(int id)
        {
            var idof = User.Identity.Name;
            ViewBag.ExibitionId = id;
            return View("Views/ExpoHome/Exhibition/ExpoBrands/ExpoInfoIndex.cshtml");
        }
        public IActionResult ExpoBrandInfo(int id, int brandInfo)
        {
            ViewBag.ExibitionId = id;
            ViewBag.BrandId = brandInfo;
            return View("Views/ExpoHome/Exhibition/ExpoBrands/ExpoBrandsInfo.cshtml");
        }
        public IActionResult AddEditUser(int id = 0)
        {
            BllUser blluser = new BllUser();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add User";
                ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit User";
                ViewBag.IsAdd = false;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/Users/AddEditUser.cshtml");
        }
        public IActionResult MyProfile(int id, int exhibitionid)
        {
            ViewBag.ExibitionId = exhibitionid;
            BllUser blluser = new BllUser();

            BllEducation bllEducation = new BllEducation();
            BllWorkingExperience bllWorkingExperience = new BllWorkingExperience();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Register";
                ViewBag.IsAdd = false;
                WorkExperience dbWorkExperience = new WorkExperience();
                ViewBag.WorkingExperiencedata = dbWorkExperience;
                Education dbEducation = new Education();

                ViewBag.Educationdata = dbEducation;
            }
            else
            {

                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "My Profile";
                ViewBag.IsAdd = false;
                ViewBag.WorkingExperiencedata = bllWorkingExperience.GetByAttendeeId(ViewBag.data.Id);

                ViewBag.Educationdata = bllEducation.GetByAttendeeId(ViewBag.data.Id);
            }
            return View("Views/ExpoHome/Exhibition/Profile/Index.cshtml");
        }

    }
}
