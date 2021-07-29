using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtualExpo.Bll.Helpers;
using VirtualExpo.Entities.Filters;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Services;
using VirtualExpo.Models.Services;
using VisrtualExpo.Bll;

namespace LillyLifestyle.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ContactUsApiController : Controller
    {
        
        private BLLContactUs bLLContactUs = new BLLContactUs();
        private ServiceResponse result = new ServiceResponse { IsSucceeded = true };

        public ServiceResponse SaveComment(ContactUs contactUsModel)
        {
            try
            {
                ContactUs dBcontactUs = new ContactUs();

                dBcontactUs.Name = contactUsModel.Name;
                dBcontactUs.Email = contactUsModel.Email;
                dBcontactUs.Telephone = contactUsModel.Telephone;
                dBcontactUs.Message = contactUsModel.Message;
                dBcontactUs.CreatedDate = DateTime.Now;

                bLLContactUs.Insert(dBcontactUs);
                result.Message = "Your email has been received and a member of our support team will reply shortly.";
                result.TotalCount = dBcontactUs.Id;
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        public ServiceResponse LoadMessageWithCount(ContactUsSearchFilter filter)
        {
            try
            {
                var lst = bLLContactUs.Search(filter);
                result.Message = lst;
                result.TotalCount = bLLContactUs.GetSearchCount(filter);
                
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public ServiceResponse GetMessage(ContactUsSearchFilter filter)
        {
            try
            {
                result.Message = bLLContactUs.Search(filter);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }



        public ServiceResponse DeleteMessage(ContactUs model)
        {
            try
            {
                ContactUs contactUs = bLLContactUs.GetByPK(model.Id);

                if (contactUs == null)
                {
                    result.IsSucceeded = false;
                    result.Message = "Message is not found.";
                    return result;
                }


                bLLContactUs.DeleteContactUs(model.Id);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }



        [HttpPost]
        public ServiceResponse GetReplyTemplate(int id)
        {
            try
            {
                ContactUs dbContactUs = new ContactUs();
                dbContactUs = bLLContactUs.GetByPK(id);
                result.Message = dbContactUs.Email;
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }
        [HttpPost]
        public async Task<ServiceResponse> SendReply(ReplyEmailModel EmailModel)
        {
            try
            {
                string subject = "Reply From : Web Educational Expo Support Team";
                string senderName = "Web Educational Expo Support Team";
                string senderEmail = "info@WebEducationalExpo.com";
                string ToEmail = EmailModel.ToEmail;
                string body = EmailModel.Message;

                await Utility.DeliverEmail(ToEmail, senderEmail, senderName, subject, body);

                result.Message = "Reply has been send successfully";
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Message = ex.Message + "<br>" + ex.StackTrace;
            }
            return result;
        }

        [HttpPost]
        public ServiceResponse CheckRead(int id)
        {
            try
            {
                ContactUs dbContactUs = new ContactUs();
                dbContactUs = bLLContactUs.GetByPK(id);
                dbContactUs.IsRead = true;
                bLLContactUs.Update(dbContactUs);
                result.IsSucceeded = true;
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
