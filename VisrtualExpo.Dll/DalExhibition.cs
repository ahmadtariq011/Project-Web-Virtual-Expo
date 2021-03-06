using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

using VirtualExpo.Model.Data;
using VirtualExpo.Model.Filters;
using VirtualExpo.Model.Services;
using VirtualExpo.Model.Enums;

namespace VisrtualExpo.Dll
{
    public class DalExhibition
    {
        public List<Exhibition> ByOrganizerId(int id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.Where(p => p.Organizer_User_Id == id).ToList();
            }
        }

        public List<Exhibition> GetAll()
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.ToList();
            }
        }
        public List<Exhibition> GetOrganizersExhibition(int id )
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.Where(p=>p.Organizer_User_Id==id).ToList();
            }
        }
        public List<Exhibition> GetAllApproveAndUpcoming()
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.Where(p=>p.ExhibitionStatus==2 && p.Status==2).ToList();
            }
        }
        public List<Exhibition> GetAllLive()
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.Where(p=>p.Status==1 && p.ExhibitionStatus==2).ToList();
            }
        }

        public Exhibition GetLatestLive()
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.First(p => p.Status == 1 || p.ExhibitionStatus == 2 || p.Status == 2 || p.ExhibitionStatus == 1);
            }
        }
        public List<Exhibition> GetAllUpcoming()
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.Where(p => p.Status == 2 && p.ExhibitionStatus == 2).ToList();
            }
        }

        /// <summary>
        /// This function get User object by Primary Key
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Exhibition GetByPK(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.Exhibitions.FirstOrDefault(p => p.Id == Id);
            }
        }

        public void ChangeStatus(Exhibition Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                Exhibition dbExhibition = entities.Exhibitions.SingleOrDefault(p => p.Id == Exhibition.Id);
                dbExhibition.ExhibitionStatus = Exhibition.ExhibitionStatus;
                entities.SaveChanges();

            }
        }
        public void ChangeExhibitionStatus(Exhibition Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                Exhibition dbExhibition = entities.Exhibitions.SingleOrDefault(p => p.Id == Exhibition.Id);
                dbExhibition.Status = Exhibition.Status;
                entities.SaveChanges();

            }
        }

        /// <summary>
        /// This function inserts a new record of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns Primary Key of new record</returns>
        public int Insert(Exhibition Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                entities.Exhibitions.Add(Exhibition);
                entities.SaveChanges();
                return Exhibition.Id;
            }
        }
        public void Update(Exhibition Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                Exhibition dbExhibition = entities.Exhibitions.SingleOrDefault(p => p.Id == Exhibition.Id);


                dbExhibition.Name = Exhibition.Name;
                dbExhibition.OraganizerName = Exhibition.OraganizerName;
                dbExhibition.Description = Exhibition.Description;
                dbExhibition.StartDate = Exhibition.StartDate;
                dbExhibition.EndDate = Exhibition.EndDate;
                dbExhibition.Organizer_User_Id = Exhibition.Organizer_User_Id;
                dbExhibition.ExhibitionStatus = Exhibition.ExhibitionStatus;

                entities.SaveChanges();
            }
        }



        /// <summary>
        /// This function returns all records of User
        /// </summary>
        /// <returns>List of User</returns>
        public List<Exhibition> GetAllExhibitions()
        {
            using (var entities = new ApplicationDbContext())
            {
                try
                {
                    return entities.Exhibitions.ToList();
                }
                catch (Exception ex)
                {
                    // Log Exception
                    throw ex;
                }
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
                Exhibition dbExhibition = entities.Exhibitions.SingleOrDefault(p => p.Id == Id);

                if (dbExhibition == null)
                    return false;

                entities.Exhibitions.Remove(dbExhibition);

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
        public List<ExhibitionModel> Search(ExhibitionSearchFilter filters)
        {
            int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from exhibition in entities.Exhibitions
                            where exhibition.Organizer_User_Id == filters.OrganizerId
                            select new ExhibitionModel { 
                                Id= exhibition.Id,
                                Name= exhibition.Name,
                                OraganizerName= exhibition.OraganizerName,
                                Organizer_User_Id= exhibition.Organizer_User_Id,
                                Description= exhibition.Description,
                                StartDate= exhibition.StartDate,
                                EndDate= exhibition.EndDate,
                                CreatedDate= exhibition.CreatedDate,
                                ExhibitionStatus= exhibition.ExhibitionStatus
                                
                            };


                if (string.IsNullOrEmpty(filters.Sort))
                {
                    filters.Sort = "Id Desc";
                }

                var lst = query.OrderBy(filters.Sort).Skip(skip).Take(filters.PageSize).ToList();
                foreach (var Exhibitionss in lst)
                {
                    Exhibitionss.CreatedDateStr = string.Format("{0:MM/dd/yyyy}", Exhibitionss.CreatedDate);
                    Exhibitionss.StartdateSrt = string.Format("{0:MM/dd/yyyy}", Exhibitionss.StartDate);
                    Exhibitionss.EndDateStr = string.Format("{0:MM/dd/yyyy}", Exhibitionss.EndDate);
                    Exhibitionss.ExhibitionStatusStr = System.Enum.Parse(typeof(ExhibitionStatus), Exhibitionss.ExhibitionStatus.ToString()).ToString();

                }
                return lst;
            }
        }



        /// <summary>
        /// This function executes count query after applying different filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Count of searched recored as integer value</returns>
        public int GetSearchCount(ExhibitionSearchFilter filters)
        {
            using (var entities = new ApplicationDbContext())
            {
                var query = from exhibition in entities.Exhibitions
                            where exhibition.Organizer_User_Id == filters.OrganizerId
                            select exhibition;





                return query.Count();
            }
        }

    }
}
