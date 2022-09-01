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
    public class SocialMediaController : BaseController
    {
        // GET: Admin/SocialMedia
        SocialMediaBLL bll = new SocialMediaBLL();
       public ActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            //before checking model state control img
            if(model.SocialImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
                
            }
            else if (ModelState.IsValid)
            {
                //we haven't activete multiple choice - here for single image
                // so we will use 1st element of posted file base
                HttpPostedFileBase postedFile = model.SocialImage;

                // posted files includes input stream to hold files
                // so we will get image from input stream
                Bitmap SocialMedia = new Bitmap(postedFile.InputStream);

                //we can also resize here - but not in this case
                //we haven't control file extension - so user can add docx as well
                //so we will control it
                //check for valid input - so get 1st extension of file
                //then check for valid extension
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if (ext==".jpg" || ext==".jpeg" || ext==".png" ||ext==".gif")
                {

                    //now make file operations
                    //we need unique value for image - so we will use GUID class
                    //later we will add file name to the unique no.- so we have unique file name
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedFile.FileName;

                    //now we will copy/add file/image to the upload folder - provide path
                    //the above code will copy/add single image into upload folder
                    SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" +filename));

                    //now lets hold file name in model - because we will hold file name in database
                    model.ImagePath = filename;
                    if (bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        model = new SocialMediaDTO();
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.processState = General.Messages.GeneralError;
                    }

                }
                else
                {
                    ViewBag.ProcessState=General.Messages.ExtensionError;
                }

            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            
            
            return View(model);
        }


        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> socialMediaList = new List<SocialMediaDTO>();
            socialMediaList = bll.GetSocialMediaList();
            return View(socialMediaList);
        }

        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessStateMessage = General.Messages.EmptyArea;
            }
            else
            {
                //if we have change an image
                if (model.SocialImage != null)
                {
                    //copy from Add Social Media Action
                    HttpPostedFileBase postedFile = model.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedFile.InputStream);

                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "";

                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedFile.FileName;

                        SocialMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                        model.ImagePath = filename;
                    }
                }

                //after update we need to delete old image from the folder
                //now we will return image path with update method

                string oldImagePath = bll.UpdateSocialMedia(model);

                if(model.SocialImage != null)
                {
                    //first we need to check there is an image or not
                    //for this we will use System.IO

                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath));
                    }

                    ViewBag.ProcessStateMessage = General.Messages.UpdateSuccess;
                }

            }
            return View(model);
        }


        public JsonResult DeleteSocialMedia(int ID)
        {
            string imagePath = bll.DeleteSocialMedia(ID);

            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagePath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + imagePath));
            }

            return Json("");
        }


    }
}