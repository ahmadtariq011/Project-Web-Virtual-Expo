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
    [Authorize(Roles = "Exhibitor", AuthenticationSchemes = "Exhibitor")]
    public class ExhibitorController : Controller
    {
        public IActionResult UserIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Users/Index.cshtml");
        }
        public IActionResult Dashboard()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Dashboard/Index.cshtml");
        }
        public IActionResult AttendeeIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Attendee/Index.cshtml");
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
        public IActionResult AddEditExhibitor(int id = 0)
        {
            BllUser blluser = new BllUser();

            BllExhibitorDescription bllExhibitorDescription = new BllExhibitorDescription();

            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Exhibitor";
                ViewBag.IsAdd = false;
                ExhibitorDescription dbWorkExperience = new ExhibitorDescription();
                ViewBag.ExhibitorDescription = dbWorkExperience;
            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit Exhibitor";
                ViewBag.IsAdd = false;
                var WorkingExperiencedata = bllExhibitorDescription.GetByUserid(ViewBag.data.Id);
                if (bllExhibitorDescription.GetByUserid(ViewBag.data.Id) != null)
                {
                    ViewBag.ExhibitorDescription = bllExhibitorDescription.GetByUserid(ViewBag.data.Id);
                }
                else
                {
                    ExhibitorDescription dbWorkExperience = new ExhibitorDescription();
                    ViewBag.ExhibitorDescription = dbWorkExperience;
                }
            }
            return View("Views/ExpoAdmin/ExpoDashboard/Users/AddEditExhibitor.cshtml");
        }
    }
}
