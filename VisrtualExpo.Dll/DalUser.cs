using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;
using VirtualExpo.Model.Data;
using VirtualExpo.Models.Filters;

namespace VirtualExpo.Dal
{
    public class DalUser
    {
        /// <summary>
        /// This function get User object by Primary Key
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public User GetByPK(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Users.FirstOrDefault(p => p.Id == Id);
            }
        }

        public User Login(string username, string password)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Users.FirstOrDefault(p => p.UserName == username && p.Password == password);
            }
        }
  
        public User GetByUserName(string UserName)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Users.SingleOrDefault(p => p.UserName == UserName);
            }
        }
        public User GetByEmail(string email)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Users.SingleOrDefault(p => p.Email == email);
            }
        }

        //public int GetTotalUsers()
        //{
        //    using (var entities = new ApplicationDbContext())
        //    {
        //        int type = Convert.ToInt32(UserRoleType.User);
        //        return entities.Users.Count(p => p.UserType == type);
        //    }
        //}




        /// <summary>
        /// This function inserts a new record of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns Primary Key of new record</returns>
        public int Insert(User user)
        {
            using (var entities = new ApplicationDbContext())
            {
                entities.Users.Add(user);
                entities.SaveChanges();
                return user.Id;
            }
        }

        public Boolean DeletePicture(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                User dbUser = entities.Users.SingleOrDefault(p => p.Id == Id);

                if (dbUser == null)
                    return false;
                else
                    return true;

            }
        }


        /// <summary>
        /// This function updates User
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            using (var entities = new ApplicationDbContext())
            {
                //User dbUser = entities.Users.SingleOrDefault(p => p.Id == user.Id);
                //dbUser.FirstName = user.FirstName;
                //dbUser.LastName = user.LastName;
                //dbUser.Email = user.Email;
                //dbUser.Picture = user.Picture;
                //dbUser.Password = user.Password;
                //dbUser.Telephone = user.Telephone;
                //dbUser.CNIC = user.CNIC;
                //dbUser.UserType = user.UserType;
                //dbUser.GenderType = user.GenderType;
                //entities.SaveChanges();
            }
        }


        /// <summary>
        /// This function returns all records of User
        /// </summary>
        /// <returns>List of User</returns>
        public List<User> GetAllUsers()
        {
            using (var entities = new ApplicationDbContext())
            {
            
                return entities.Users.ToList();
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
                User dbUser = entities.Users.SingleOrDefault(p => p.Id == Id);

                if (dbUser == null)
                    return false;

                entities.Users.Remove(dbUser);

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
        public List<User> Search(UserSearchFilter filters)
        {
            int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from user in entities.Users
                            select user;

                var lst = query.ToList();
                return lst;
            }
        }

        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(UserSearchFilter filters)
        {
            using (var entities = new ApplicationDbContext())
            {
                var query = from user in entities.Users
                            select user;

                if (!string.IsNullOrEmpty(filters.Keyword))
                {
                    query = query.Where(p => p.FirstName.Contains(filters.Keyword) || p.LastName.Contains(filters.Keyword) || p.Email.Contains(filters.Keyword));
                }

                return query.Count();
            }
        }


    }
}
