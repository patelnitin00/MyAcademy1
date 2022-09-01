using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        MetaBLL bll = new MetaBLL();

        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMeta()
        {
            MetaDTO dto = new MetaDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddMeta(MetaDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            MetaDTO newModel = new MetaDTO();
            return View(newModel);
        }


        public ActionResult MetaList()
        {
            //on this page we will send Meata list so add it
            List<MetaDTO> metaList = new List<MetaDTO>();

            metaList = bll.GetMetaListData();

            return View(metaList);
        }

        public ActionResult UpdateMeta(int ID)
        {
            MetaDTO model = new MetaDTO();
            model = bll.GetMetaWithID(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }


        public JsonResult DeleteMeta(int ID)
        {
           
            bll.DeleteMeta(ID);
            return Json("");
        }



    }
}