using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SocialMediaDAO 
    {
        public int AddSocialMedia(SocialMedia social)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.SocialMedias.Add(social);
                    db.SaveChanges();
                    return social.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
          
        }

        public List<SocialMediaDTO> GetSocialMediaList()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<SocialMedia> list = new List<SocialMedia>();
                list = db.SocialMedias.Where(x => x.isDeleted == false).ToList();

                List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();

                foreach (var item in list)
                {
                    SocialMediaDTO dto = new SocialMediaDTO();
                    dto.Name = item.Name;
                    dto.Link = item.Link;
                    dto.ImagePath = item.ImagePath;
                    dto.ID = item.ID;
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                SocialMediaDTO dto = new SocialMediaDTO();
                SocialMedia socialMedia = new SocialMedia();
                socialMedia = db.SocialMedias.First(x => x.ID == ID);

                dto.ID = socialMedia.ID;
                dto.Name = socialMedia.Name;
                dto.Link = socialMedia.Link;
                dto.ImagePath = socialMedia.ImagePath;

                return dto;
            }
          
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    SocialMedia socialMedia = db.SocialMedias.First(x => x.ID == model.ID);
                    string oldImagePath = socialMedia.ImagePath;
                    socialMedia.Name = model.Name;
                    socialMedia.Link = model.Link;

                    if (model.ImagePath != null)
                        socialMedia.ImagePath = model.ImagePath;

                    socialMedia.LastUpdateDate = DateTime.Now;
                    socialMedia.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();

                    return oldImagePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            

        }

        public string DeleteSocialMedia(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    SocialMedia social = db.SocialMedias.First(x => x.ID == ID);
                    string imagePath = social.ImagePath;
                    social.isDeleted = true;
                    social.DeletedDate = DateTime.Now;
                    social.LastUpdateDate = DateTime.Now;
                    social.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                    return imagePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }
    }
}
