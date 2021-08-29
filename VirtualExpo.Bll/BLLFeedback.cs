using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Entities.Filters;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Services;
using VisrtualExpo.Dll;

namespace VisrtualExpo.Bll
{
	/// <summary>
    /// This class deals with Feedback table
    /// </summary>
    public class BLLFeedback
    {
		private DllFeedback dalFeedback = new DllFeedback();

		/// <summary>
       /// This function calls insert function of dal class  
	   /// to insert a new record of Feedback
       /// </summary>
       /// <param name="Id"></param>
       /// <returns>returns Primary Key of new record</returns>
        public Feedback GetByPK(int Id)
        {
            return dalFeedback.GetByPK(Id);
        }


	   /// <summary>
       /// This function calls insert function of dal class  
	   /// to insert a new record of Feedback
       /// </summary>
       /// <param name="Id"></param>
       /// <returns>returns Primary Key of new record</returns>
       public long Insert(Feedback Feedback)
       {
          return dalFeedback.Insert(Feedback);
       }

	    /// <summary>
       /// This function calls update function of dal class  
       /// </summary>
       /// <param name="Feedback"></param>
        public void Update(Feedback Feedback)
        {
            dalFeedback.Update(Feedback);
        }

	   /// <summary>
       /// This function calls insert function of dal class  
	   /// to insert a new record of Feedback
       /// </summary>
       /// <param name="Feedback"></param>
	   /// <returns>List of Feedback</returns>
       public List<Feedback> GetAllFeedbacks()
       {
           return dalFeedback.GetAllFeedbacks();
       }

		/// <summary>
        /// This function deletes Feedback by its Primary Key 
		/// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean DeleteFeedback(Int32 Id)
        {
           return dalFeedback.Delete(Id);
        }

		/// <summary>
        /// This function performs search query after applying different filters
        /// This function also does sorting and pagination as per the parameters of filter object
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>IEnumerable<dynamic></returns>
        public List<FeedbackModel> Search(FeedbackSearchFilter filters)
        {
            return dalFeedback.Search(filters);
        }

        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(FeedbackSearchFilter filters)
        {
            return dalFeedback.GetSearchCount(filters);
        }

      
    }
}
