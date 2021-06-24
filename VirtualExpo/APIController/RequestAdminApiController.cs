using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Bll;
using VirtualExpo.Model.Filters;
using VirtualExpo.Model.Services;

namespace VirtualExpo.Web.APIController
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class RequestAdminApiController : Controller
    {
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
    }
}
