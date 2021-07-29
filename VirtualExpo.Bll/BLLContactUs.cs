using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Entities.Filters;
using VirtualExpo.Model.Data;
using VirtualExpo.Models.Services;
using VisrtualExpo.Dll;

namespace VisrtualExpo.Bll
{
	/// <summary>
    /// This class deals with ContactUs table
    /// </summary>
   public class BLLContactUs
    {
		private DllContactUs dalContactUs = new  DllContactUs();

		/// <summary>
       /// This function calls insert function of dal class  
	   /// to insert a new record of ContactUs
       /// </summary>
       /// <param name="Id"></param>
       /// <returns>returns Primary Key of new record</returns>
        public ContactUs GetByPK(int Id)
        {
            return dalContactUs.GetByPK(Id);
        }


	   /// <summary>
       /// This function calls insert function of dal class  
	   /// to insert a new record of ContactUs
       /// </summary>
       /// <param name="Id"></param>
       /// <returns>returns Primary Key of new record</returns>
       public long Insert(ContactUs contactus)
       {
          return dalContactUs.Insert(contactus);
       }

	    /// <summary>
       /// This function calls update function of dal class  
       /// </summary>
       /// <param name="contactus"></param>
        public void Update(ContactUs contactus)
        {
            dalContactUs.Update(contactus);
        }

	   /// <summary>
       /// This function calls insert function of dal class  
	   /// to insert a new record of ContactUs
       /// </summary>
       /// <param name="contactus"></param>
	   /// <returns>List of ContactUs</returns>
       public List<ContactUs> GetAllContactUss()
       {
           return dalContactUs.GetAllContactUss();
       }

		/// <summary>
        /// This function deletes ContactUs by its Primary Key 
		/// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean DeleteContactUs(Int32 Id)
        {
           return dalContactUs.Delete(Id);
        }

		/// <summary>
        /// This function performs search query after applying different filters
        /// This function also does sorting and pagination as per the parameters of filter object
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>IEnumerable<dynamic></returns>
        public List<ContactUsModel> Search(ContactUsSearchFilter filters)
        {
            return dalContactUs.Search(filters);
        }

        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(ContactUsSearchFilter filters)
        {
            return dalContactUs.GetSearchCount(filters);
        }

      
    }
}
