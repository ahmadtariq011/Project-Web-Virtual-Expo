using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VisrtualExpo.Dll;

namespace VirtualExpo.Bll
{
    public class BllMessage
    {

        DllMessage dllMessage = new DllMessage();

        public List<Message> GetAllMessageByExhibition(string id)
        {
            return dllMessage.GetAllMessageByExhibition(id);
        }
        public Message GetByPK(int Id)
        {
            return dllMessage.GetByPK(Id);
        }
        public Message GetByExhibition(string Id)
        {
            return dllMessage.GetByExhibition(Id);
        }

        public int Insert(Message Exhibition)
        {
            return dllMessage.Insert(Exhibition);
        }

        public void Update(Message Exhibition)
        {
            dllMessage.Update(Exhibition);
        }
        /// <summary>
        /// This function deletes User by its Primary Key 
        /// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean DeleteExhibitions(int Id)
        {
            return dllMessage.Delete(Id);
        }

        /// <summary>
        /// This function performs search query after applying different filters
        /// This function also does sorting and pagination as per the parameters of filter object
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>IEnumerable<dynamic></returns>


    }
}
