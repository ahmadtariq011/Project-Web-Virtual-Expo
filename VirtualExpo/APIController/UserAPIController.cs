using FlightoUs.Bll;
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

    }
}



/*
BllUser bllUser = new BllUser();
private ServiceResponse result = new ServiceResponse { IsSucceeded = true };

*/
