using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        AdsDAO adsDAO = new AdsDAO();
        
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            //now we will fill necessary areas 1 by 1

            //1. Slider
            dto.SliderPost = dao.GetSliderPost();

            //2. Breaking News 
            dto.BreakingPost = dao.GetBreakingPosts();

            //3. Popular videos
            dto.PopularPost = dao.GetPopularPosts();

            //4. Most Viewed 
            dto.MostViewedPost = dao.GetMostViewedPosts();

            //5. Videos
            dto.Videos = dao.GetVideos();

            //6. Ads - we already have method in ads DAO 
            //we do not have much ads for now so we can use it
            //for now we have only 2 ads in database so we will use them directly in index page 
            dto.AdsList = adsDAO.GetAdsList();




            return dto;

        }

        public GeneralDTO GetPostDetailPageItemsWithID(int ID)
        {
            GeneralDTO dto = new GeneralDTO();

            //in addition we need breaking training and ads - so first get that
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsDAO.GetAdsList();

            dto.PostDetail = dao.GetPostDetail(ID);

            return dto;
        }


        CategoryDAO categoryDAO = new CategoryDAO();
        public GeneralDTO GetCategoryPostList(string categoryName)
        {
           GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsDAO.GetAdsList();

            //check for video and post
            if(categoryName == "video")
            {
                dto.Videos = dao.GetAllVideos();
            }
            else
            {
                //for category postlist we need category ID of similar post
                List<CategoryDTO> categoryList = categoryDAO.GetCategoryList();
                //now find true id from list
                int categoryID = 0;
                foreach(var item in categoryList)
                {
                    //category name was changed by friendly url - so to take true result we need to call method again
                    if(categoryName == SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }

                //now we have categoryID
                dto.CateggoryPostList = dao.GetCategoryPostList(categoryID);

            }


            return dto;
        }

        AddressDAO addressDAO = new AddressDAO();
        public GeneralDTO GetContactPageItems()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsDAO.GetAdsList();
            dto.Address = addressDAO.GetAddressList().First();


            return dto;
        }

        public GeneralDTO GetSearchPosts(string searchText)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.AdsList = adsDAO.GetAdsList();
            dto.CateggoryPostList = dao.GetSearchPosts(searchText);
            dto.SearchText = searchText;
            return dto;
        }
    }
}
