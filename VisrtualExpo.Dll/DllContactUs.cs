using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Filters;
using VirtualExpo.Models.Services;
using VirtualExpo.Entities.Filters;

namespace VisrtualExpo.Dll
{
    public class DllContactUs
    {
        /// <summary>
        /// This function get ContactUs object by Primary Key
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ContactUs GetByPK(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.ContactUs.FirstOrDefault(p => p.Id == Id);
            }
        }

        /// <summary>
        /// This function inserts a new record of ContactUs
        /// </summary>
        /// <param name="contactus"></param>
        /// <returns>returns Primary Key of new record</returns>
        public long Insert(ContactUs contactus)
        {
            using (var entities = new ApplicationDbContext())
            {
                entities.ContactUs.Add(contactus);
                entities.SaveChanges();
                return contactus.Id;
            }
        }

        /// <summary>
        /// This function updates ContactUs
        /// </summary>
        /// <param name="contactus"></param>
        public void Update(ContactUs contactus)
        {
            using (var entities = new ApplicationDbContext())
            {
                ContactUs dbContactUs = entities.ContactUs.SingleOrDefault(p => p.Id == contactus.Id);
                dbContactUs.Id = contactus.Id;
                dbContactUs.Name = contactus.Name;
                dbContactUs.Email = contactus.Email;
                dbContactUs.Telephone = contactus.Telephone;
                dbContactUs.Message = contactus.Message;
                dbContactUs.CreatedDate = contactus.CreatedDate;
                dbContactUs.IsRead = contactus.IsRead;

                entities.SaveChanges();
            }
        }

        /// <summary>
        /// This function returns all records of ContactUs
        /// </summary>
        /// <returns>List of ContactUs</returns>
        public List<ContactUs> GetAllContactUss()
        {
            using (var entities = new ApplicationDbContext())
            {
                try
                {
                    return entities.ContactUs.ToList();
                }
                catch (Exception ex)
                {
                    // Log Exception
                    throw ex;
                }
            }
        }

        /// <summary>
        /// This function deletes ContactUs by its Primary Key 
        /// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean Delete(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                ContactUs dbContactUs = entities.ContactUs.SingleOrDefault(p => p.Id == Id);

                if (dbContactUs == null)
                    return false;

                entities.ContactUs.Remove(dbContactUs);

                entities.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// This function performs search query after applying different filters
        /// This function also does sorting and pagination as per the parameters of filter object
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>IEnumerable<dynamic></returns>
        public List<ContactUsModel> Search(ContactUsSearchFilter filters)
        {
            int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from contactus in entities.ContactUs
                            select new ContactUsModel
                            {
                                Id = contactus.Id,
                                Name = contactus.Name,
                                Email = contactus.Email,
                                Telephone = contactus.Telephone,
                                Message = contactus.Message,
                                CreatedDate = contactus.CreatedDate,
                                IsRead = contactus.IsRead
                            };


                if (!string.IsNullOrEmpty(filters.Keyword))
                {
                    query = query.Where(p => p.Name.Contains(filters.Keyword) || p.Email.Contains(filters.Keyword) || p.Telephone.Contains(filters.Keyword) || p.Message.Contains(filters.Keyword));
                }

                if (string.IsNullOrEmpty(filters.Sort))
                {
                    filters.Sort = "Id Desc";
                }

                var lst = query.OrderBy(filters.Sort).Skip(skip).Take(filters.PageSize).ToList();
                foreach (var contactus in lst)
                {
                    contactus.CreatedDateStr = string.Format("{0:MM/dd/yyyy}", contactus.CreatedDate);
                }

                return lst;
            }
        }

        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(ContactUsSearchFilter filters)
        {
            using (var entities = new ApplicationDbContext())
            {
                var query = from contactus in entities.ContactUs
                            select contactus;

                if (!string.IsNullOrEmpty(filters.Keyword))
                {
                    query = query.Where(p => p.Name.Contains(filters.Keyword) || p.Email.Contains(filters.Keyword) || p.Telephone.Contains(filters.Keyword) || p.Message.Contains(filters.Keyword));
                }


                return query.Count();
            }
        }
    }
}
