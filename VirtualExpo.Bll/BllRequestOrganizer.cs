using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Filters;
using VisrtualExpo.Dll;

namespace VirtualExpo.Bll
{
    public class BllRequestOrganizer
    {
        DllReuestOrganizer dalExhibition = new DllReuestOrganizer();

        public List<RequestOrganizer> GetAll()
        {
            return dalExhibition.GetAllExhibitions();
        }
        public RequestOrganizer GetByPK(int Id)
        {
            return dalExhibition.GetByPK(Id);
        }


        public int Insert(RequestOrganizer Exhibition)
        {
            return dalExhibition.Insert(Exhibition);
        }

        public void Update(RequestOrganizer Exhibition)
        {
            dalExhibition.Update(Exhibition);
        }
        public List<RequestOrganizer> GetAllExhibitions()
        {
            return dalExhibition.GetAllExhibitions();
        }

        /// <summary>
        /// This function deletes User by its Primary Key 
        /// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean DeleteExhibitions(int Id)
        {
            return dalExhibition.Delete(Id);
        }

        /// <summary>
        /// This function performs search query after applying different filters
        /// This function also does sorting and pagination as per the parameters of filter object
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>IEnumerable<dynamic></returns>
        public List<RequestOrganizer> Search(RequestOrganizerFilter filters)
        {
            return dalExhibition.Search(filters);
        }
        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(RequestOrganizerFilter filters)
        {
            return dalExhibition.GetSearchCount(filters);
        }
    }
}
