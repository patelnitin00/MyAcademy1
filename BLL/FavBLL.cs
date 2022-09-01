using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FavBLL
    {
        FavDAO dao = new FavDAO();
        public FavDTO GetFav()
        {
            return dao.GetFav();
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            FavDTO returnDTO = new FavDTO();
            returnDTO = dao.UpdateFav(model);

            LogDAO.AddLog(General.ProcessType.IconUpdate,
                General.TableName.Icon, returnDTO.ID);
            return returnDTO;
        }
    }
}
