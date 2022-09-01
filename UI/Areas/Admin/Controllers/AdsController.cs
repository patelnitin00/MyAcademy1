using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AdsController : BaseController
    {
        // GET: Admin/Ads
        AdsBLL bll = new AdsBLL();
        public ActionResult AddAds()
        {
            AdsDTO dto = new AdsDTO();
            return View(dto);

        }

        [HttpPost]
        public ActionResult AddAds(AdsDTO model)
        {

            if (model.AdsImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                string filename = "";
                HttpPostedFileBase postedFile = model.AdsImage;
                Bitmap UserImage = new Bitmap(postedFile.InputStream);

                //resizing image
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedFile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    //creting unique number using GUID
                    string UniqueNumber = Guid.NewGuid().ToString();
                    filename = UniqueNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
                    model.ImagePath = filename;

                    bll.AddUser(model);
                    ViewBag.ProcessState = General.Messages.AddSuccess;

                    ModelState.Clear();
                    model = new AdsDTO();
                }

                }
                else
            {
              ViewBag.ProcessState = General.Messages.GeneralError;
            }

            return View(model);

            }
       
        
        public ActionResult AdsList()
        {
            List<AdsDTO> adsList = new List<AdsDTO>();
            adsList = bll.GetAdsList();
            return View(adsList);
        }

        public ActionResult UpdateAds(int ID)
        {
            AdsDTO ad = new AdsDTO();
            ad = bll.GetUserWithID(ID);
            return View(ad);
        }

        [HttpPost]
        public ActionResult UpdateAds(AdsDTO model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                if (model.AdsImage != null)
                {
                    string filename = "";
                    HttpPostedFileBase postedFile = model.AdsImage;
                    Bitmap UserImage = new Bitmap(postedFile.InputStream);

                    //resizing image
                    Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedFile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        //creting unique number using GUID
                        string UniqueNumber = Guid.NewGuid().ToString();
                        filename = UniqueNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
                        model.ImagePath = filename;
                    }

                   
                }
                string oldImagePath = bll.UpdateAds(model);

                if(model.AdsImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + oldImagePath));
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;

                ModelState.Clear();
                model = new AdsDTO();
            }


            return View(model);
        }

        public JsonResult DeleteAds(int ID)
        {
            string imagePath = bll.DeleteAds(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + imagePath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + imagePath));
            }
            return Json("");
        }

    }
}