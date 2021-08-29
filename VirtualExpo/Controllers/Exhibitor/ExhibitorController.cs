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
        public IActionResult AddEditMediaLinks(int Exhibitionid = 0)
        {
            BllMediaLinks blluser = new BllMediaLinks();
            if (blluser.GetByExhibitor(Exhibitionid) == null)
            {
                MediaLinks dbuser = new MediaLinks();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Media Link";
                ViewBag.IsAdd = false;
                ViewBag.ExhibitorDescription = Exhibitionid;
            }
            else
            {
                ViewBag.data = blluser.GetByExhibitor(Exhibitionid);
                ViewBag.title = "Edit Media Link";
                ViewBag.IsAdd = false;
                ViewBag.ExhibitorDescription = Exhibitionid;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/MediaLinks.cshtml");
        }
        public IActionResult AddEditSocialNetwork(int Exhibitionid = 0)
        {
            BllSocialNetwork blluser = new BllSocialNetwork();
            if (blluser.GetByExhibitor(Exhibitionid) == null)
            {
                SocialNetwork dbuser = new SocialNetwork();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Social Network";
                ViewBag.IsAdd = false;
                ViewBag.ExhibitorDescription = Exhibitionid;
            }
            else
            {
                ViewBag.data = blluser.GetByExhibitor(Exhibitionid);
                ViewBag.title = "Edit Social Network";
                ViewBag.IsAdd = false;
                ViewBag.ExhibitorDescription = Exhibitionid;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/SocialNetwork.cshtml");
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
