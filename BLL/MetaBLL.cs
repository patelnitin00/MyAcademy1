using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MetaBLL
    {
        MetaDAO dao = new MetaDAO();
        public bool AddMeta(MetaDTO model)
        {
            //make logic operation here
            //create Meta Class and send it to DAO

            Meta meta = new Meta();
            meta.Name = model.Name;
            meta.MetaContent = model.MetaContent;
            meta.AddDate = DateTime.Now;
            //isdelete is false by default
            meta.LastUpdateUserID=UserStatic.UserID;
            meta.LastUpdateDate = DateTime.Now;

            int MetaID = dao.AddMeta(meta);

            LogDAO.AddLog(General.ProcessType.MetaAdd, General.TableName.Meta, MetaID);
            return true;

        }

        public List<MetaDTO> GetMetaListData()
        {
            List<MetaDTO> dtoList = new List<MetaDTO>();
            dtoList = dao.GetMetaListData();
            return dtoList;
        }

        public MetaDTO GetMetaWithID(int ID)
        {
            MetaDTO metaDTO = new MetaDTO();
            metaDTO = dao.GetMetaWithID(ID);
            return metaDTO; 
        }

        public bool UpdateMeta(MetaDTO model)
        {
            dao.UpdateMeta(model);
            LogDAO.AddLog(General.ProcessType.MetaUpdate,
                General.TableName.Meta, model.MetaID);
            return true;
        }

        public void DeleteMeta(int ID)
        {
            dao.DeleteMeta(ID);
            LogDAO.AddLog(General.ProcessType.MetaDelete,
                General.TableName.Meta, ID);
        }
    }
}
