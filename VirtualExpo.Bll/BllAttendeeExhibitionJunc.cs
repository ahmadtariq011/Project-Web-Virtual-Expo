using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VisrtualExpo.Dll;

namespace VirtualExpo.Bll
{
    public class BllAttendeeExhibitionJunc
    {
        DllAttendeeExhibitorJunc dllAttendeeExhibitorJunc = new DllAttendeeExhibitorJunc();
        public AttendeeExhibitionJunction GetByPK(int Id)
        {
            return dllAttendeeExhibitorJunc.GetByPK(Id);
        }


        public AttendeeExhibitionJunction GetByAttendeeId(int Id)
        {
            return dllAttendeeExhibitorJunc.GetByAttendeeId(Id);
        }


        /// <summary>
        /// This function inserts a new record of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns Primary Key of new record</returns>
        public int Insert(AttendeeExhibitionJunction Exhibition)
        {
            return dllAttendeeExhibitorJunc.Insert(Exhibition);

        }
        public void Update(AttendeeExhibitionJunction Exhibition)
        {
            dllAttendeeExhibitorJunc.Update(Exhibition);

        }



        /// <summary>
        /// This function returns all records of User
        /// </summary>
        /// <returns>List of User</returns>
        public List<AttendeeExhibitionJunction> GetAllMediaLinks(int id)
        {
            return dllAttendeeExhibitorJunc.GetAllMediaLinks(id);

        }

        /// <summary>
        /// This function deletes User by its Primary Key 
        /// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean Delete(int Id)
        {
            return dllAttendeeExhibitorJunc.Delete(Id);

        }
    }
}
