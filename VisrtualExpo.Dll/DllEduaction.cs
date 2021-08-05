using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Filters;
using System.Linq.Dynamic.Core;
namespace VisrtualExpo.Dll
{
    public class DllEduaction
    {
        /// <summary>
        /// This function get User object by Primary Key
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Education GetByPK(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Educations.FirstOrDefault(p => p.Id == Id);
            }
        }





        public Education GetByAttendeeId(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Educations.FirstOrDefault(p => p.Attendee_User_Id == Id);
            }
        }



        /// <summary>
        /// This function inserts a new record of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns Primary Key of new record</returns>
        public int Insert(Education Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                entities.Educations.Add(Exhibition);
                entities.SaveChanges();
                return Exhibition.Id;
            }
        }
        public void Update(Education Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                Education dbExhibition = entities.Educations.SingleOrDefault(p => p.Id == Exhibition.Id);


                entities.SaveChanges();
            }
        }



        /// <summary>
        /// This function returns all records of User
        /// </summary>
        /// <returns>List of User</returns>
        public List<Education> GetAttendeeAllEduactions(int id)
        {
            using (var entities = new ApplicationDbContext())
            {
                try
                {
                    return entities.Educations.Where(p=>p.Attendee_User_Id==id).ToList();
                }
                catch (Exception ex)
                {
                    // Log Exception
                    throw ex;
                }
            }
        }

        /// <summary>
        /// This function deletes User by its Primary Key 
        /// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean Delete(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                Education dbExhibition = entities.Educations.SingleOrDefault(p => p.Id == Id);

                if (dbExhibition == null)
                    return false;

                entities.Educations.Remove(dbExhibition);

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
        public List<Education> Search(RequestAdminFilter filters)
        {
            int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from RequestOrganizerFilter in entities.Educations
                            select RequestOrganizerFilter;


                if (string.IsNullOrEmpty(filters.Sort))
                {
                    filters.Sort = "Id Desc";
                }

                var lst = query.OrderBy(filters.Sort).Skip(skip).Take(filters.PageSize).ToList();
                return lst;
            }
        }



        ///// <summary>
        ///// This function executes count query after applying different filters
        ///// </summary>
        ///// <param name="filters"></param>
        ///// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(RequestAdminFilter filters)
        {
            using (var entities = new ApplicationDbContext())
            {
                var query = from Exhibition in entities.Educations
                            select Exhibition;





                return query.Count();
            }
        }

    }
}
