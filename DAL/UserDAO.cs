using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class UserDAO
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                UserDTO dto = new UserDTO();

                T_User user = db.T_User.Where(x => x.Username == model.Username).FirstOrDefault();
                //T_User user = db.T_User.First(x=>x.Username == model.Username);

                if (user != null && user.ID != 0)
                {
                    dto.ID = user.ID;
                    dto.Username = user.Username;
                    dto.Name = user.NameSurname;
                    dto.ImagePath = user.ImagePath;
                    dto.isAdmin = user.isAdmin;
                }

                return dto;
            }
           
        }

        public int AddUser(T_User user)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.T_User.Add(user);
                    db.SaveChanges();
                    return user.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

        public List<UserDTO> GetUserList()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    List<UserDTO> list = new List<UserDTO>();

                    List<T_User> users = db.T_User.ToList();

                    foreach (var item in users)
                    {
                        UserDTO user = new UserDTO();
                        user.ID = item.ID;
                        user.Username = item.Username;
                        user.Password = item.Password;
                        user.Email = item.Email;
                        user.isAdmin = item.isAdmin;
                        user.ImagePath = item.ImagePath;
                        user.Name = item.NameSurname;

                        list.Add(user);

                    }

                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }

       

        public UserDTO GetUserWithID(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                UserDTO dto = new UserDTO();

                T_User user = db.T_User.First(x => x.ID == ID);

                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Password = user.Password;
                dto.Email = user.Email;
                dto.isAdmin = user.isAdmin;
                dto.ImagePath = user.ImagePath;
                dto.Name = user.NameSurname;

                return dto;
            }
               
            
        }

        public string UpdateUser(UserDTO model)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    T_User user = db.T_User.First(x => x.ID == model.ID);
                    string oldImagePath = user.ImagePath;

                    user.NameSurname = model.Name;
                    user.Username = model.Username;
                    user.Password = model.Password;
                    user.Email = model.Email;

                    if (model.ImagePath != null)
                    {
                        user.ImagePath = model.ImagePath;
                    }
                    user.LastUpdateDate = DateTime.Now;
                    user.LastUpdateUserID = UserStatic.UserID;
                    user.isAdmin = model.isAdmin;

                    db.SaveChanges();

                    return oldImagePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }


        public string DeleteUser(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    T_User user = db.T_User.First(x => x.ID == ID);
                    user.idDeleted = true;
                    user.DeleteDate = DateTime.Now;
                    user.LastUpdateDate = DateTime.Now;
                    user.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();

                    return user.ImagePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

    }
}

