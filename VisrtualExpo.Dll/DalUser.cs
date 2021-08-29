using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;
using VirtualExpo.Model.Data;
using VirtualExpo.Models.Filters;
using VirtualExpo.Model.Enums;

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

        public List<User> GetOrganizers()
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Users.Where(p => p.UserType == Convert.ToInt32(UserRoleType.Organizer)).ToList();
            }
        }




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
                User dbUser = entities.Users.SingleOrDefault(p => p.Id == user.Id);
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.Email = user.Email;
                dbUser.Password = user.Password;
                dbUser.Telephone = user.Telephone;
                dbUser.CNIC = user.CNIC;
                dbUser.Picture = user.Picture;
                dbUser.GenderType = user.GenderType;
                dbUser.Description = user.Description;
                entities.SaveChanges();
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
                            where user.UserType==2
                            select user;

                var lst = query.ToList();
                return lst;
            }
        }
        public List<User> SearchExhibitors(UserSearchFilter filters)
        {
            int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from user in entities.Users
                            where user.UserType == 3
                            select user;

                var lst = query.ToList();
                return lst;
            }
        }

        public List<User> SearchAttendee(UserSearchFilter filters)
        {
            int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from user in entities.Users
                            join attendeeuser in entities.AttendeeExhibitionJunctions on user.Id equals attendeeuser.Attendee_Id
                            where user.UserType == 4 && attendeeuser.Exibition_id == filters.Exhibition_Id
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
                            where user.UserType == 2
                            select user;

                if (!string.IsNullOrEmpty(filters.Keyword))
                {
                    query = query.Where(p => p.FirstName.Contains(filters.Keyword) || p.LastName.Contains(filters.Keyword) || p.Email.Contains(filters.Keyword));
                }

                return query.Count();
            }
        }
        public int GetSearchCountExhibitor(UserSearchFilter filters)
        {
            using (var entities = new ApplicationDbContext())
            {
                var query = from user in entities.Users
                            where user.UserType == 3
                            select user;

                if (!string.IsNullOrEmpty(filters.Keyword))
                {
                    query = query.Where(p => p.FirstName.Contains(filters.Keyword) || p.LastName.Contains(filters.Keyword) || p.Email.Contains(filters.Keyword));
                }

                return query.Count();
            }
        }

        public int GetSearchCountAttendee(UserSearchFilter filters)
        {
            using (var entities = new ApplicationDbContext())
            {

                var query = from user in entities.Users
                            join attendeeuser in entities.AttendeeExhibitionJunctions on user.Id equals attendeeuser.Attendee_Id
                            where user.UserType == 4 && attendeeuser.Exibition_id==filters.Exhibition_Id
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
