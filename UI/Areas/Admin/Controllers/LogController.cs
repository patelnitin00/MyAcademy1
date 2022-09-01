﻿using DTO;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UI.Areas.Admin.Controllers
{
    public class LogController : BaseController
    {
        // GET: Admin/Log
        LogBLL bll = new LogBLL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogList()
        {
            List<LogDTO> list = new List<LogDTO>();
            list = bll.GetLogsList();
            return View(list);
        }

    }
}