using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {
        // GET: Admin/Address
        AddressBLL bll = new AddressBLL();
        public ActionResult AddAddress()
        {
            AddressDTO address = new AddressDTO();
            return View(address);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
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
            return View(model);
        }


        public ActionResult AddressList()
        {
            List<AddressDTO> list = new List<AddressDTO>();

            list = bll.GetAddressList();

            return View(list);
        }

        public ActionResult UpdateAddress(int ID)
        {
            AddressDTO address = new AddressDTO();
            address = bll.GetUserWithID(ID);
            return View(address);
            /*  List<AddressDTO> list = new List<AddressDTO>();
              list = bll.GetAddressList();
              AddressDTO address = list.First(x=>x.ID == ID);
              return View(address);*/
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
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
            return View(model);
        }


        //spelling of method DeleteAddress is wrong intentionally
        public JsonResult DeleteAddress(int ID)
        {
            bll.DeleteAdress(ID);
            return Json("");
        }


    }
}