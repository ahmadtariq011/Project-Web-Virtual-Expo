using System;
using System.Collections.Generic;
using VirtualExpo.Dal;
using VirtualExpo.Model.Data;
using VirtualExpo.Models.Filters;

namespace FlightoUs.Bll
{
    /// <summary>
    /// This class deals with User table
    /// </summary>
    public class BllUser
    {
        private DalUser dalUser = new DalUser();

        /// <summary>
        /// This function calls insert function of dal class  
        /// to insert a new record of User
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>returns Primary Key of new record</returns>
        public User GetByPK(int Id)
        {
            return dalUser.GetByPK(Id);
        }
        public User GetByUserName(string UserName)
        {
            return dalUser.GetByUserName(UserName);
        }
        public User GetByEmail(string email)
        {
            return dalUser.GetByEmail(email);
        }
        public User Login(string email, string password)
        {
            return dalUser.Login(email, password);
        }
        /// <summary>
        /// This function calls insert function of dal class  
        /// to insert a new record of User
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>returns Primary Key of new record</returns>
        public int Insert(User user)
        {
            return dalUser.Insert(user);
        }

        /// <summary>
        /// This function calls update function of dal class  
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            dalUser.Update(user);
        }

        public Boolean DeletePicture(int Id)
        {
            return dalUser.DeletePicture(Id);
        }

        /// <summary>
        /// This function calls insert function of dal class  
        /// to insert a new record of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List of User</returns>
        public List<User> GetAllUsers()
        {
            return dalUser.GetAllUsers();
        }
        /// <summary>
        /// This function deletes User by its Primary Key 
        /// and returns True in case of Success
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True/False</returns>
        public Boolean DeleteUser(Int32 Id)
        {
            return dalUser.Delete(Id);
        }

        /// <summary>
        /// This function performs search query after applying different filters
        /// This function also does sorting and pagination as per the parameters of filter object
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>IEnumerable<dynamic></returns>
        public List<User> Search(UserSearchFilter filters)
        {
            return dalUser.Search(filters);
        }

        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(UserSearchFilter filters)
        {
            return dalUser.GetSearchCount(filters);
        }
    }
}
