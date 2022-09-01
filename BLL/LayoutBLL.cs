using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        SocialMediaDAO socialMediaDAO = new SocialMediaDAO();
        FavDAO favDAO = new FavDAO();
        MetaDAO metaDAO = new MetaDAO();
        AddressDAO addressDAO = new AddressDAO();
        PostDAO postDAO = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            //in this method we will set all the elements 1 by 1
            HomeLayoutDTO dto = new HomeLayoutDTO();

            //1. Categories
            dto.Categories = categoryDAO.GetCategoryList();

            //2. Socail media
            List<SocialMediaDTO> socialMediaList = new List<SocialMediaDTO>();
            socialMediaList = socialMediaDAO.GetSocialMediaList();
            //now we will get value one by one - we can use loop but not necessary
            dto.Facebook = socialMediaList.First(x=>x.Link.Contains("facebook"));
            dto.Twitter = socialMediaList.First(x => x.Link.Contains("twitter"));
            dto.Instagram = socialMediaList.First(x => x.Link.Contains("instagram"));
            dto.Youtube = socialMediaList.First(x => x.Link.Contains("youtube"));
            //dto.LinkedIn = socialMediaList.First(x => x.Link.Contains("linkedin"));

            //3. FavIcon and Title
            dto.FavDto = favDAO.GetFav();

            //4. Meta Data
            dto.MetaList = metaDAO.GetMetaListData();

            //5. Address
            dto.Address = addressDAO.GetAddressList().First();

            //6. Hot News - we can use GetPostList() method but it will get all posts
            //not efficient - so generate seperate method
            dto.HotNews = postDAO.GetHotNews();

            return dto;

        }
    }
}
