using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        ContactBLL contactBLL = new ContactBLL();
        // GET: Admin/Contact
        public ActionResult UnReadMessage()
        {
            List<ContactDTO> list  = new List<ContactDTO>();
            list = contactBLL.GetUnreadMessages();
            return View(list);
        }


        public ActionResult AllMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = contactBLL.GetAllMessages();
            return View(list);
        }


        public ActionResult ReadMessage(int ID)
        {
            contactBLL.ReadMessage(ID);
            return RedirectToAction("UnReadMessage");
        }

        public ActionResult ReadMessage2(int ID)
        {
            contactBLL.ReadMessage(ID);
            return RedirectToAction("AllMessages");
        }

        public JsonResult DeleteMessage(int ID)
        {
            contactBLL.DeleteMessage(ID);
            return Json("");
        }



    }
}