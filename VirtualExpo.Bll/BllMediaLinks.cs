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
    public class BllMediaLinks
    {
        DllMediaLinks dalExhibition = new DllMediaLinks();

        public List<MediaLinks> GetAllMediaLinks(int id)
        {
            return dalExhibition.GetAllMediaLinks(id);
        }
        public MediaLinks GetByPK(int Id)
        {
            return dalExhibition.GetByPK(Id);
        }
        public MediaLinks GetByExhibitor(int Id)
        {
            return dalExhibition.GetByExhibitor(Id);
        }

        public int Insert(MediaLinks Exhibition)
        {
            return dalExhibition.Insert(Exhibition);
        }

        public void Update(MediaLinks Exhibition)
        {
            dalExhibition.Update(Exhibition);
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
        public List<MediaLinks> Search(RequestAdminFilter filters)
        {
            return dalExhibition.Search(filters);
        }
        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(RequestAdminFilter filters)
        {
            return dalExhibition.GetSearchCount(filters);
        }

    }
}
