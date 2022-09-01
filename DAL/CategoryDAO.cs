using DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public class CategoryDAO: PostContext
    {
        public int AddCategory(Category category)
    {
        try
        {

            db.Categories.Add(category);
            db.SaveChanges();

            return category.ID;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<CategoryDTO> GetCategoryList()
    {
        List<CategoryDTO> list = new List<CategoryDTO>();

        List<Category> categories = db.Categories.Where(x => x.isDeleted == false)
            .OrderBy(x => x.AddDate).ToList();

        foreach (var category in categories)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.ID = category.ID;
            categoryDTO.CategoryName = category.CategoryName;

            list.Add(categoryDTO);
        }
        return list;


    }

    public static List<SelectListItem> GetCategoriesForDropdown()
    {
        List<SelectListItem> categoryList = db.Categories.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).Select(x => new SelectListItem()
        {
            Text = x.CategoryName,
            Value = SqlFunctions.StringConvert((double)x.ID)
        }).ToList();

        return categoryList;
    }

    public CategoryDTO GetCategoryWithID(int ID)
    {
        CategoryDTO categoryDTO = new CategoryDTO();

        Category category = db.Categories.First(x => x.ID == ID);

        categoryDTO.ID = category.ID;
        categoryDTO.CategoryName = category.CategoryName;

        return categoryDTO;
    }



    public void UpdateCategory(CategoryDTO model)
    {
        try
        {
            Category category = db.Categories.First(x => x.ID == model.ID);

            category.CategoryName = model.CategoryName;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public List<Post> DeleteCategory(int ID)
    {
        try
        {
            Category category = db.Categories.First(x => x.ID == ID);
            category.isDeleted = true;
            category.DeletedDate = DateTime.Now;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;

            db.SaveChanges();

            List<Post> postList = db.Posts.Where(x => x.CategoryID == ID && x.isDeleted == false).ToList();
            return postList;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
}



















        //////
        //////
    /*{
        public int AddCategory(Category category)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {

                    db.Categories.Add(category);
                    db.SaveChanges();

                    return category.ID;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
          
        }

        public List<CategoryDTO> GetCategoryList()
        {
            using(POSTDATAEntities db= new POSTDATAEntities())
            {
                List<CategoryDTO> list = new List<CategoryDTO>();

                List<Category> categories = db.Categories.Where(x => x.isDeleted == false)
                    .OrderBy(x => x.AddDate).ToList();

                foreach (var category in categories)
                {
                    CategoryDTO categoryDTO = new CategoryDTO();
                    categoryDTO.ID = category.ID;
                    categoryDTO.CategoryName = category.CategoryName;

                    list.Add(categoryDTO);
                }
                return list;
            }
            


        }

        public static List<SelectListItem> GetCategoriesForDropdown()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<SelectListItem> categoryList = db.Categories.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = SqlFunctions.StringConvert((double)x.ID)
                }).ToList();

                return categoryList;
            }
           
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                CategoryDTO categoryDTO = new CategoryDTO();

                Category category = db.Categories.First(x => x.ID == ID);

                categoryDTO.ID = category.ID;
                categoryDTO.CategoryName = category.CategoryName;

                return categoryDTO;
            }
            
        }

       

        public void UpdateCategory(CategoryDTO model)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Category category = db.Categories.First(x => x.ID == model.ID);

                    category.CategoryName = model.CategoryName;
                    category.LastUpdateDate = DateTime.Now;
                    category.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
          
        }


        public List<Post> DeleteCategory(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Category category = db.Categories.First(x => x.ID == ID);
                    category.isDeleted = true;
                    category.DeletedDate = DateTime.Now;
                    category.LastUpdateDate = DateTime.Now;
                    category.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();

                    List<Post> postList = db.Posts.Where(x => x.CategoryID == ID && x.isDeleted == false).ToList();
                    return postList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }

    }
}*/
