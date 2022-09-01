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
    public class FavController : BaseController
    {
        // GET: Admin/Fav
        FavBLL bll = new FavBLL();
        public ActionResult UpdateFav()
        {
            FavDTO dto = new FavDTO();
            dto = bll.GetFav();
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateFav(FavDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.FavImage != null)
                {
                    string favname = "";
                    HttpPostedFileBase postedFileFav = model.FavImage;
                    Bitmap favImage = new Bitmap(postedFileFav.InputStream);
                    Bitmap resizeFavImage = new Bitmap(favImage, 100, 100);

                    string ext = Path.GetExtension(postedFileFav.FileName);

                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string favunique = Guid.NewGuid().ToString();
                        favname = favunique + postedFileFav.FileName;
                        resizeFavImage.Save(Server.MapPath("~/Areas/Admin/Content/favImage/" +favname));
                        model.Fav = favname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }

                if (model.LogoImage != null)
                {
                    string logoname = "";
                    HttpPostedFileBase postedFileFav = model.LogoImage;
                    Bitmap logoImage = new Bitmap(postedFileFav.InputStream);
                    Bitmap resizeLogoImage = new Bitmap(logoImage, 100, 100);

                    string ext = Path.GetExtension(postedFileFav.FileName);

                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string logounique = Guid.NewGuid().ToString();
                        logoname = logounique + postedFileFav.FileName;
                        resizeLogoImage.Save(Server.MapPath("~/Areas/Admin/Content/favImage/" + logoname));
                        model.Fav = logoname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }

                FavDTO returnDTO = new FavDTO();
                returnDTO = bll.UpdateFav(model);

                if(model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/favImage/" + returnDTO.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/favImage/" + returnDTO.Logo));
                    }
                }
                if(model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/favImage/" + returnDTO.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/favImage/" + returnDTO.Fav));
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;

            }
            //this 2 line was not initially in the code - I have written that
            //comment and check
           // ModelState.Clear();
            //model = new FavDTO();
            //above 2 line was not initially in the code - I have written that
            return View(model);
        }
    }
}