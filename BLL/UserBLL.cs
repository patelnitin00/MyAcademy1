using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userDAO = new UserDAO();        
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userDAO.GetUserWithUsernameAndPassword(model);

            return dto;
        }

        public void AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ImagePath = model.ImagePath;
            user.NameSurname = model.Name;
            user.isAdmin = model.isAdmin;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;

            int ID = userDAO.AddUser(user);

            LogDAO.AddLog(General.ProcessType.UserAdd,
                General.TableName.User, ID);
            
        }

        public List<UserDTO> GetUserList()
        {
            return userDAO.GetUserList();
        }

        public UserDTO GetUserWithID(int ID)
        {
            return userDAO.GetUserWithID(ID);
        }

        public string UpdateUser(UserDTO model)
        {
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID);
            return userDAO.UpdateUser(model);
        }

        public string DeleteUser(int ID)
        {
            string imagePath = userDAO.DeleteUser(ID);
            LogDAO.AddLog(General.ProcessType.UserDelete, General.TableName.User, ID);

            return imagePath;

        }
    }
}
