using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

using VirtualExpo.Entities.Filters;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Services;

namespace VisrtualExpo.Dll
{
    public class DllFeedback
    {
        /// <summary>
        /// This function get Feedback object by Primary Key
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Feedback GetByPK(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Feedback.FirstOrDefault(p => p.Id == Id);
            }
        }

        /// <summary>
        /// This function inserts a new record of Feedback
        /// </summary>
        /// <param name="Feedback"></param>
        /// <returns>returns Primary Key of new record</returns>
        public long Insert(Feedback Feedback)
        {
            using (var entities = new ApplicationDbContext())
            {
                entities.Feedback.Add(Feedback);
                entities.SaveChanges();
                return Feedback.Id;
            }
        }

        /// <summary>
        /// This function updates Feedback
        /// </summary>
        /// <param name="Feedback"></param>
        public void Update(Feedback Feedback)
        {
            using (var entities = new ApplicationDbContext())
            {
                Feedback dbFeedback = entities.Feedback.SingleOrDefault(p => p.Id == Feedback.Id);
                dbFeedback.Id = Feedback.Id;
                dbFeedback.Name = Feedback.Name;
                dbFeedback.Email = Feedback.Email;
                dbFeedback.Telephone = Feedback.Telephone;
                dbFeedback.Message = Feedback.Message;
                dbFeedback.CreatedDate = Feedback.CreatedDate;
                dbFeedback.IsRead = Feedback.IsRead;

                entities.SaveChanges();
            }
        }

        /// <summary>
        /// This function returns all records of Feedback
        /// </summary>
        /// <returns>List of Feedback</returns>
        public List<Feedback> GetAllFeedbacks()
        {
            using (var entities = new ApplicationDbContext())
            {
                try
                {
                    return entities.Feedback.ToList();
                }
                catch (Exception ex)
                {
                    // Log Exception
                    throw ex;
                }
            }
        }

        /// <summary>
        /// This function deletes Feedback by its Primary Key 
        /// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean Delete(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                Feedback dbFeedback = entities.Feedback.SingleOrDefault(p => p.Id == Id);

                if (dbFeedback == null)
                    return false;

                entities.Feedback.Remove(dbFeedback);

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
        public List<FeedbackModel> Search(FeedbackSearchFilter filters)
        {
            int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from feedback in entities.Feedback
                            join exhibitions in entities.Exhibitions on feedback.ExhibitionId equals exhibitions.Id
                            where exhibitions.Organizer_User_Id == filters.Userlog
                            select new FeedbackModel
                            {
                                Id = feedback.Id,
                                Name = feedback.Name,
                                Email = feedback.Email,
                                Telephone = feedback.Telephone,
                                Message = feedback.Message,
                                CreatedDate = feedback.CreatedDate,
                                IsRead = feedback.IsRead,
                                ExhibitionName = exhibitions.Name
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
                foreach (var Feedback in lst)
                {
                    Feedback.CreatedDateStr = string.Format("{0:MM/dd/yyyy}", Feedback.CreatedDate);
                }

                return lst;
            }
        }

        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(FeedbackSearchFilter filters)
        {
            using (var entities = new ApplicationDbContext())
            {
                var query = from feedback in entities.Feedback
                            join exhibitions in entities.Exhibitions on feedback.ExhibitionId equals exhibitions.Id
                            where exhibitions.Organizer_User_Id == filters.Userlog
                            select feedback;

                if (!string.IsNullOrEmpty(filters.Keyword))
                {
                    query = query.Where(p => p.Name.Contains(filters.Keyword) || p.Email.Contains(filters.Keyword) || p.Telephone.Contains(filters.Keyword) || p.Message.Contains(filters.Keyword));
                }


                return query.Count();
            }
        }
    }
}
