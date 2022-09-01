using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FavDAO 
    {
        public FavDTO GetFav()
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                FavLogoTitle fav = db.FavLogoTitles.First();

                //why dto aleays - because in tire architucture you
                //do not access DAL layer in UI - so we convert it to DTO

                FavDTO favDTO = new FavDTO();
                favDTO.ID = fav.ID;
                favDTO.Title = fav.Title;
                favDTO.Logo = fav.Logo;
                favDTO.Fav = fav.Fav;

                return favDTO;
            }
            
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    FavLogoTitle fav = db.FavLogoTitles.First();
                    FavDTO dto = new FavDTO();

                    //to return
                    dto.ID = fav.ID;
                    dto.Fav = fav.Fav;
                    dto.Logo = fav.Logo;

                    //here to update
                    fav.Title = model.Title;
                    if (model.Logo != null)
                    {
                        fav.Logo = model.Logo;
                    }
                    if (model.Fav != null)
                    {
                        fav.Fav = model.Fav;
                    }
                    db.SaveChanges();
                    return dto;


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
          
        }
    }
}
