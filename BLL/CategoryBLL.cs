using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO dao = new CategoryDAO();
        public bool AddCategory(CategoryDTO model)
        {
            Category category = new Category();
            category.ID = model.ID;
            category.CategoryName = model.CategoryName;
            category.AddDate = DateTime.Now;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;

            int ID = dao.AddCategory(category);

            LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, ID);

            return true;
        }

        public static List<SelectListItem> GetCategoriesForDropDown()
        {
            return CategoryDAO.GetCategoriesForDropdown();
        }

        public List<CategoryDTO> GetCategoryList()
        {
            return dao.GetCategoryList();
        }
    
        public CategoryDTO GetCategoryWithID(int ID)
        {
            
            CategoryDTO category = new CategoryDTO();
            category = dao.GetCategoryWithID(ID);
            return category;
        }

        public bool UpdateCategory(CategoryDTO model)
        {
            dao.UpdateCategory(model);

            LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, model.ID);

            return true;
        }

        PostBLL postbll = new PostBLL();
        public List<PostImageDTO> DeleteCategory(int ID)
        {
            List<Post> postList = dao.DeleteCategory(ID);
            LogDAO.AddLog(General.ProcessType.CategoryDelete, General.TableName.Category, ID);

            List<PostImageDTO> imageList = new List<PostImageDTO>();
            foreach(var item in postList)
            {
                List<PostImageDTO> imageList2 = postbll.DeletePost(item.ID);
                LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, item.ID);

                foreach(var item2 in imageList2)
                {
                    imageList.Add(item2);
                }
            }
            
            
            return imageList;
        }
    }
}
