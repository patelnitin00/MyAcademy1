using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AdsBLL
    {
        AdsDAO adsDAO = new AdsDAO();
        public void AddUser(AdsDTO model)
        {
            Ad ads = new Ad();
            ads.Name = model.Name;
            ads.ImagePath = model.ImagePath;
            ads.Link = model.Link;
            ads.Size = model.ImageSize;
            ads.AddDate = DateTime.Now;
            ads.LastUpdateDate = DateTime.Now;
            ads.LastUpdateUserID = UserStatic.UserID;

            int logID = adsDAO.AddUser(ads);
            LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Ads,logID);
        }

        public List<AdsDTO> GetAdsList()
        {
            return adsDAO.GetAdsList();
        }

        public AdsDTO GetUserWithID(int ID)
        {
           AdsDTO ads = new AdsDTO();

            ads = adsDAO.GetAdsWithID(ID);

            return ads;
        }

        public string UpdateAds(AdsDTO model)
        {
            LogDAO.AddLog(General.ProcessType.AdsUpdate, General.TableName.Ads, model.ID);
            return adsDAO.UpdateAds(model);
        }

        public string DeleteAds(int ID)
        {
            string imagePath = adsDAO.DeleteAds(ID);
            LogDAO.AddLog(General.ProcessType.AdsDelete, General.TableName.Ads,ID);
            return imagePath;
        }
    }
}
