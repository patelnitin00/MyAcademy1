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
    public class UserController : BaseController
    {
        // GET: Admin/User
        UserBLL bll = new UserBLL();
        public ActionResult AddUser()
        {
            UserDTO model = new UserDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if (model.userImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;

            }
            else if (ModelState.IsValid)
            {
                string filename = "";
                HttpPostedFileBase postedFile = model.userImage;
                Bitmap UserImage = new Bitmap(postedFile.InputStream);

                //resizing image
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedFile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    //creting unique number using GUID
                    string UniqueNumber = Guid.NewGuid().ToString();
                    filename = UniqueNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename));
                    model.ImagePath = filename;

                    bll.AddUser(model);
                    ViewBag.ProcessState = General.Messages.AddSuccess;

                    ModelState.Clear();
                    model = new UserDTO();


                }
                else
                {
                    ViewBag.ProcessState = General.Messages.ExtensionError;
                }

            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }


            return View(model);
        }


        public ActionResult UserList()
        {
            List<UserDTO> users = new List<UserDTO>();
            users = bll.GetUserList();
            return View(users);
        }


        public ActionResult UpdateUser(int ID)
        {
            UserDTO user = new UserDTO();

            user = bll.GetUserWithID(ID);

            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                //if user image is change
                if (model.userImage != null)
                {
                    string filename = "";
                    HttpPostedFileBase postedFile = model.userImage;
                    Bitmap UserImage = new Bitmap(postedFile.InputStream);

                    //resizing image
                    Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedFile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        //creting unique number using GUID
                        string UniqueNumber = Guid.NewGuid().ToString();
                        filename = UniqueNumber + postedFile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename));
                        model.ImagePath = filename;

                    }
                }

                    //now we need to delete old image so we need todefine a method to delete old image path

                    string oldImagePath = bll.UpdateUser(model);

                    if (model.userImage != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePath)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePath));
                        }
                        ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    }
                
            }

                return View(model);
           
        }

        public JsonResult DeleteUser(int ID)
        {
            string imagePath = bll.DeleteUser(ID);

            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagePath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagePath));
            }

            return Json("");
        }

    }
}