using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VirtualExpo.Model.Services;

namespace VisrtualExpo.Dll
{
    public class DllExhibitorDescription
    {
        /// <summary>
        /// This function get User object by Primary Key
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ExhibitorDescription GetByPK(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.ExhibitorDescription.FirstOrDefault(p => p.Id == Id);
            }
        }

        public ExhibitorDescription GetByUserid(int Id)
        {
            using (var entities = new ApplicationDbContext())
            {
                return entities.ExhibitorDescription.FirstOrDefault(p => p.UserId == Id);
            }
        }



        /// <summary>
        /// This function inserts a new record of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns Primary Key of new record</returns>
        public int Insert(ExhibitorDescription Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                entities.ExhibitorDescription.Add(Exhibition);
                entities.SaveChanges();
                return Exhibition.Id;
            }
        }
        public void Update(ExhibitorDescription Exhibition)
        {
            using (var entities = new ApplicationDbContext())
            {
                ExhibitorDescription dbExhibition = entities.ExhibitorDescription.SingleOrDefault(p => p.Id == Exhibition.Id);


                dbExhibition.Name = Exhibition.Name;
                dbExhibition.Offer = Exhibition.Offer;
                dbExhibition.Moto = Exhibition.Moto;

                entities.SaveChanges();
            }
        }



        /// <summary>
        /// This function returns all records of User
        /// </summary>
        /// <returns>List of User</returns>
        public List<ExhibitorDescription> GetAllExhibitions()
        {
            using (var entities = new ApplicationDbContext())
            {
                try
                {
                    return entities.ExhibitorDescription.ToList();
                }
                catch (Exception ex)
                {
                    // Log Exception
                    throw ex;
                }
            }
        }


        public List<ExhibitorDescription> GetAllExhibitorsWithRespectToExhibition(int id)
        {
            using (var entities = new ApplicationDbContext())
            {
                try
                {
                    return entities.ExhibitorDescription.Where(p=> p.Exibition_id == id).ToList();
                }
                catch (Exception ex)
                {
                    // Log Exception
                    throw ex;
                }
            }
        }

        public List<ExhibitorDescriptionModel> GetAllExhibitorUserInfo(int id)
        {
            //int skip = (filters.PageIndex - 1) * filters.PageSize;

            using (var entities = new ApplicationDbContext())
            {
                var query = from Exhibitor in entities.ExhibitorDescription
                            join ExhibitorUser in entities.Users on Exhibitor.UserId equals ExhibitorUser.Id
                            where Exhibitor.Exibition_id == id
                            select new ExhibitorDescriptionModel
                            {
                                Id = Exhibitor.Id,
                                Name = Exhibitor.Name,
                                Email = ExhibitorUser.Email,
                                Telephone = ExhibitorUser.Telephone,
                                Moto = Exhibitor.Moto
                            };


                var lst = query.ToList();
                return lst;
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
                ExhibitorDescription dbExhibition = entities.ExhibitorDescription.SingleOrDefault(p => p.Id == Id);

                if (dbExhibition == null)
                    return false;

                entities.ExhibitorDescription.Remove(dbExhibition);

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
        //public List<ExhibitorDescription> Search(ExhibitorDescription filters)
        //{
        //    int skip = (filters.PageIndex - 1) * filters.PageSize;

        //    using (var entities = new ApplicationDbContext())
        //    {
        //        var query = from Exhibition in entities.Exhibitions
        //                    select new ExhibitionModel
        //                    {
        //                        Id = Exhibition.Id,
        //                        Name = Exhibition.Name,
        //                        OraganizerName = Exhibition.OraganizerName,
        //                        Organizer_User_Id = Exhibition.Organizer_User_Id,
        //                        Description = Exhibition.Description,
        //                        StartDate = Exhibition.StartDate,
        //                        EndDate = Exhibition.EndDate,
        //                        CreatedDate = Exhibition.CreatedDate,
        //                        ExhibitionStatus = Exhibition.ExhibitionStatus

        //                    };


        //        if (string.IsNullOrEmpty(filters.Sort))
        //        {
        //            filters.Sort = "Id Desc";
        //        }

        //        var lst = query.OrderBy(filters.Sort).Skip(skip).Take(filters.PageSize).ToList();
        //        foreach (var Exhibitionss in lst)
        //        {
        //            Exhibitionss.CreatedDateStr = string.Format("{0:MM/dd/yyyy}", Exhibitionss.CreatedDate);
        //            Exhibitionss.StartdateSrt = string.Format("{0:MM/dd/yyyy}", Exhibitionss.StartDate);
        //            Exhibitionss.EndDateStr = string.Format("{0:MM/dd/yyyy}", Exhibitionss.EndDate);
        //            Exhibitionss.ExhibitionStatusStr = System.Enum.Parse(typeof(ExhibitionStatus), Exhibitionss.ExhibitionStatus.ToString()).ToString();

        //        }
        //        return lst;
        //    }
        //}



        ///// <summary>
        ///// This function executes count query after applying different filters
        ///// </summary>
        ///// <param name="filters"></param>
        ///// <returns>Count of searched recored as integer value</returns>
        //public int GetSearchCount(ExhibitionSearchFilter filters)
        //{
        //    using (var entities = new ApplicationDbContext())
        //    {
        //        var query = from Exhibition in entities.Exhibitions
        //                    select Exhibition;





        //        return query.Count();
        //    }
        //}

    }
}
