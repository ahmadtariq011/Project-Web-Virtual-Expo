using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ExhibitionApiController : Controller
    {
        BllExhibition bllExhibition = new BllExhibition();

        ServiceResponse result = new ServiceResponse();
        [HttpPost]
        public ServiceResponse GetExhibitionWithCount(ExhibitionSearchFilter filter)
        {
            try
            {
                var lst = bllExhibition.Search(filter);
                result.Message = lst;
                result.TotalCount = bllExhibition.GetSearchCount(filter);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }


        [HttpPost]
        public ServiceResponse SaveExhibition(ExhibitionModel Exhibition)
        {
            try
            {
                //bool IsAdmin = User.IsInRole(UserRoleType.Admin.ToString());
                //bool IsManager = User.IsInRole(UserRoleType.Manager.ToString());
                //bool IsUser = User.IsInRole(UserRoleType.User.ToString());
                //string typeofUserLogin = "";
                //if (IsAdmin)
                //{
                //    typeofUserLogin = "Admin";
                //}
                //if (IsManager)
                //{
                //    typeofUserLogin = "Manager";
                //}
                //if (IsUser)
                //{
                //    typeofUserLogin = "User";
                //}
                if (bllExhibition.GetByPK(Exhibition.Id) == null)
                {
                    Exhibition dbExhibition = new Exhibition();


                    dbExhibition.Name = Exhibition.Name;
                    dbExhibition.OraganizerName = Exhibition.OraganizerName;
                    dbExhibition.Description = Exhibition.Description;
                    dbExhibition.StartDate = Exhibition.StartDate;
                    dbExhibition.EndDate = Exhibition.EndDate;
                    dbExhibition.Status = 2;
                    dbExhibition.Organizer_User_Id = Exhibition.Organizer_User_Id;
                    ExhibitionStatus classt = (ExhibitionStatus)Enum.Parse(typeof(ExhibitionStatus), Exhibition.ExhibitionStatusStr);
                    dbExhibition.ExhibitionStatus = Convert.ToInt32(classt);


                    int UserId = bllExhibition.Insert(dbExhibition);
                    result.IsSucceeded = true;
                    result.TotalCount = UserId;
                    result.Message = "Exhibition is Successfully Created";

                }
                else
                {
                    Exhibition dbExhibition = bllExhibition.GetByPK(Exhibition.Id);

                    dbExhibition.Name = Exhibition.Name;
                    dbExhibition.OraganizerName = Exhibition.OraganizerName;
                    dbExhibition.Description = Exhibition.Description;
                    dbExhibition.StartDate = Exhibition.StartDate;
                    dbExhibition.EndDate = Exhibition.EndDate;
                    dbExhibition.Organizer_User_Id = Exhibition.Organizer_User_Id;

                    ExhibitionStatus classt = (ExhibitionStatus)Enum.Parse(typeof(ExhibitionStatus), Exhibition.ExhibitionStatusStr);
                    dbExhibition.ExhibitionStatus = Convert.ToInt32(classt);

                    bllExhibition.Update(dbExhibition);
                    result.IsSucceeded = true;
                    result.Message = "Exhibition is Successfully Updated";
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

        public ServiceResponse ChangeStatus(ExhibitionModel exhibitionModel)
        {
            try
            {
                if (exhibitionModel.Id != 0)
                {
                    Exhibition dbExhibition = bllExhibition.GetByPK(exhibitionModel.Id);

                    ExhibitionStatus status = (ExhibitionStatus)Enum.Parse(typeof(ExhibitionStatus), exhibitionModel.ExhibitionStatusStr);
                    dbExhibition.ExhibitionStatus = Convert.ToInt32(status);
                    if(exhibitionModel.ExhibitionStatusStr== "Approved")
                    {
                        dbExhibition.Status = 2;
                    }
                    bllExhibition.ChangeStatus(dbExhibition);
                    result.IsSucceeded = true;
                    result.Message = "Status is updated to " + exhibitionModel.ExhibitionStatusStr;
                }
            }
            catch (Exception e)
            {
                result.IsSucceeded = false;
                result.Message = e.Message + "<br>" + e.StackTrace;
            }
            return result;
        }


        [HttpPost]

        public ServiceResponse ChangeExhibitionStatus(ExhibitionModel exhibitionModel)
        {
            try
            {
                if (exhibitionModel.Id != 0)
                {
                    Exhibition dbExhibition = bllExhibition.GetByPK(exhibitionModel.Id);

                    ExhibitionStatusActiveOrNot status = (ExhibitionStatusActiveOrNot)Enum.Parse(typeof(ExhibitionStatusActiveOrNot), exhibitionModel.ExhibitionStatusStr);
                    dbExhibition.Status = Convert.ToInt32(status);

                    bllExhibition.ChangeExhibitionStatus(dbExhibition);
                    result.IsSucceeded = true;
                    result.Message = "Exhibition Status is updated to " + exhibitionModel.ExhibitionStatusStr;
                }
            }
            catch (Exception e)
            {
                result.IsSucceeded = false;
                result.Message = e.Message + "<br>" + e.StackTrace;
            }
            return result;
        }

    }
}
