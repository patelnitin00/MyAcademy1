using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO 
    {
        public int AddUser(Ad ads)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Ads.Add(ads);
                    db.SaveChanges();

                    return ads.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

        public List<AdsDTO> GetAdsList()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<AdsDTO> adsList = new List<AdsDTO>();

                List<Ad> ads = db.Ads.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

                foreach (var ad in ads)
                {
                    AdsDTO adDTO = new AdsDTO();
                    adDTO.ID = ad.ID;
                    adDTO.Name = ad.Name;
                    adDTO.Link = ad.Link;
                    adDTO.ImageSize = ad.Size;
                    adDTO.ImagePath = ad.ImagePath;

                    adsList.Add(adDTO);
                }
                return adsList;
            }
           
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    AdsDTO ads = new AdsDTO();

                    Ad ad = db.Ads.First(x => x.ID == ID);
                    ads.ID = ad.ID;
                    ads.Name = ad.Name;
                    ads.Link = ad.Link;
                    ads.ImageSize = ad.Size;
                    ads.ImagePath = ad.ImagePath;
                    return ads;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
         
            
        }

        
        public string UpdateAds(AdsDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {

                    Ad ad = db.Ads.First(x => x.ID == model.ID);

                    string oldImagePath = ad.ImagePath;

                    ad.Name = model.Name;
                    ad.Link = model.Link;
                    ad.Size = model.ImageSize;
                    if (model.ImagePath != null)
                    {
                        ad.ImagePath = model.ImagePath;
                    }

                    ad.LastUpdateDate = DateTime.Now;
                    ad.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();

                    return oldImagePath;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
           
        }


        public string DeleteAds(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Ad ad = db.Ads.First(x => x.ID == ID);
                    string imagePath = ad.ImagePath;
                    ad.isDeleted = true;
                    ad.DeletedDate = DateTime.Now;
                    ad.LastUpdateDate = DateTime.Now;
                    ad.LastUpdateUserID = UserStatic.UserID;

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
