using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LogBLL
    {
        LogDAO dao = new LogDAO();
        public static void AddLog(int ProcessType, string TableName,
            int ProcessID)
        {
            //using this method only for login - not for other operations
            LogDAO.AddLog(ProcessType, TableName, ProcessID);
        }

        public List<LogDTO> GetLogsList()
        {
            return dao.GetLogsList();
        }
    }
}
