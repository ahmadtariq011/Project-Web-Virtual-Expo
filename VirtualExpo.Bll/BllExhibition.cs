using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Filters;
using VirtualExpo.Model.Services;
using VisrtualExpo.Dll;

namespace VirtualExpo.Bll
{
    public class BllExhibition
    {
        DalExhibition dalExhibition = new DalExhibition();

        public List<Exhibition> ByOrganizerId(int id)
        {
            return dalExhibition.ByOrganizerId(id);
        }
        public List<Exhibition> GetAll()
        {
            return dalExhibition.GetAll();
        }
        public List<Exhibition> GetOrganizersExhibition(int id)
        {
            return dalExhibition.GetOrganizersExhibition(id);
        }
        public List<Exhibition> GetAllApproveAndUpcoming()
        {
            return dalExhibition.GetAllApproveAndUpcoming();
        }
        
        public List<Exhibition> GetAllLive()
        {
            return dalExhibition.GetAllLive();
        }
        public List<Exhibition> GetAllUpcoming()
        {
            return dalExhibition.GetAllUpcoming();
        }
        public Exhibition GetByPK(int Id)
        {
            return dalExhibition.GetByPK(Id);
        }
        public void ChangeStatus(Exhibition Exhibition)
        {
            dalExhibition.ChangeStatus(Exhibition);
        }
        public void ChangeExhibitionStatus(Exhibition Exhibition)
        {
            dalExhibition.ChangeExhibitionStatus(Exhibition);
        }
        public int Insert(Exhibition Exhibition)
        {
            return dalExhibition.Insert(Exhibition);
        }
       
        public void Update(Exhibition Exhibition)
        {
            dalExhibition.Update(Exhibition);
        }
        public List<Exhibition> GetAllExhibitions()
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
        public List<ExhibitionModel> Search(ExhibitionSearchFilter filters)
        {
            return dalExhibition.Search(filters);
        }
        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(ExhibitionSearchFilter filters)
        {
            return dalExhibition.GetSearchCount(filters);
        }

    }
}
