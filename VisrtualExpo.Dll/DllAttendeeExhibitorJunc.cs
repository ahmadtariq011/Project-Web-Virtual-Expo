using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;

namespace VisrtualExpo.Dll
{
    public class DllAttendeeExhibitorJunc
    {
        public AttendeeExhibitionJunction GetByPK(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.AttendeeExhibitionJunctions.FirstOrDefault(p => p.Id == Id);
            }
        }


        public AttendeeExhibitionJunction GetByAttendeeId(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.AttendeeExhibitionJunctions.FirstOrDefault(p => p.Attendee_Id == Id);
            }
        }



        /// <summary>
        /// This function inserts a new record of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns Primary Key of new record</returns>
        public int Insert(AttendeeExhibitionJunction Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                entities.AttendeeExhibitionJunctions.Add(Exhibition);
                entities.SaveChanges();
                return Exhibition.Id;
            }
        }
        public void Update(AttendeeExhibitionJunction Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                AttendeeExhibitionJunction dbExhibition = entities.AttendeeExhibitionJunctions.SingleOrDefault(p => p.Id == Exhibition.Id);
                dbExhibition.Exibition_id = Exhibition.Exibition_id;

                entities.SaveChanges();
            }
        }



        /// <summary>
        /// This function returns all records of User
        /// </summary>
        /// <returns>List of User</returns>
        public List<AttendeeExhibitionJunction> GetAllMediaLinks(int id)
        {
            using (var entities = new ApplicationDbContext())
            {
                try
                {
                    return entities.AttendeeExhibitionJunctions.ToList();
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
                AttendeeExhibitionJunction dbExhibition = entities.AttendeeExhibitionJunctions.SingleOrDefault(p => p.Id == Id);

                if (dbExhibition == null)
                    return false;

                entities.AttendeeExhibitionJunctions.Remove(dbExhibition);

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
       



        ///// <summary>
        ///// This function executes count query after applying different filters
        ///// </summary>
       

    }
}
