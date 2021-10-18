using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Services;
using VisrtualExpo.Dll;

namespace VirtualExpo.Bll
{
    public class BllExhibitorDescription
    {
        DllExhibitorDescription dalExhibition = new DllExhibitorDescription();

        public ExhibitorDescription GetByPK(int Id)
        {
            return dalExhibition.GetByPK(Id);
        }

        public int Insert(ExhibitorDescription Exhibition)
        {
            return dalExhibition.Insert(Exhibition);
        }

        public void Update(ExhibitorDescription Exhibition)
        {
            dalExhibition.Update(Exhibition);
        }
        public List<ExhibitorDescription> GetAllExhibitions()
        {
            return dalExhibition.GetAllExhibitions();
        }
        public List<ExhibitorDescriptionModel> GetAllExhibitorsWithRespectToExhibition(int id)
        {
            return dalExhibition.GetAllExhibitorsWithRespectToExhibition(id);
        }
        
        public List<ExhibitorDescriptionModel> GetAllExhibitorUserInfo(int id)
        {
            return dalExhibition.GetAllExhibitorUserInfo(id);
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
        public ExhibitorDescription GetByUserid(int Id)
        {
            return dalExhibition.GetByUserid(Id);
        }
    }
}
