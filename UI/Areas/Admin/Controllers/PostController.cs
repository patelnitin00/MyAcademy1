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
    public class PostController : BaseController
    {
        // GET: Admin/Post
        PostBLL bll = new PostBLL();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPost()
        {
            PostDTO model = new PostDTO();

            //here is a little difference than other add
            //here we will first show the catogery list from category table
            // so we will call 2 bll methods

            //create static method for categories
            model.Categories = CategoryBLL.GetCategoriesForDropDown();


            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO model)
        {
            if(model.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                foreach(var item in model.PostImage)
                {
                    Bitmap image = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if(ext!=".png" && ext!=".jpeg" && ext!=".jpg")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropDown();
                        return View(model);
                    }
                }

                List<PostImageDTO> imageList = new List<PostImageDTO>();

                foreach(var postedfile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postedfile.InputStream);
                    Bitmap resizeImage = new Bitmap(image, 750, 422);

                    string filename = "";
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedfile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));

                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    imageList.Add(dto);


                }

                model.PostImages = imageList;

                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
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


            model.Categories = CategoryBLL.GetCategoriesForDropDown();
            return View(model);
        }


        public ActionResult PostList()
        {
            CountDTO count = new CountDTO();
            count = bll.GetAllCounts();
            ViewData["AllCounts"] = count;

            List<PostDTO> postList =  new List<PostDTO>();
            postList = bll.GetPostList();
            return View(postList);
        }

        public ActionResult UpdatePost(int ID)
        {
            //we dont need images table in add post view but we need
            //it in update - still confusing see video
            //so we will use a bool variable in dto to seperate add and update views

            PostDTO model = new PostDTO();
            model = bll.GetPostWithID(ID);
            model.Categories = CategoryBLL.GetCategoriesForDropDown();
            model.isUpdate = true;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDTO model)
        {
            //for photos we dont change(update) photos
            //if we change any of the photos we are going to add them
            //if you want to delete we delete the photo
            // so no update - just add and delete for photos

            //we won't make delete right now - we will just add it (59. Update Post)

            List<SelectListItem> selectList =CategoryBLL.GetCategoriesForDropDown();

            //now we can start update operation

            if (ModelState.IsValid)
            {
                //add photos
                if(model.PostImage[0] != null)
                {
                    foreach (var item in model.PostImage)
                    {
                        Bitmap image = new Bitmap(item.InputStream);
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".png" && ext != ".jpeg" && ext != ".jpg")
                        {
                            ViewBag.ProcessState = General.Messages.ExtensionError;
                            model.Categories = CategoryBLL.GetCategoriesForDropDown();
                            return View(model);
                        }
                    }

                    List<PostImageDTO> imageList = new List<PostImageDTO>();

                    foreach (var postedfile in model.PostImage)
                    {
                        Bitmap image = new Bitmap(postedfile.InputStream);
                        Bitmap resizeImage = new Bitmap(image, 750, 422);

                        string filename = "";
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;
                        resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));

                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = filename;
                        imageList.Add(dto);


                    }

                    model.PostImages = imageList;
                }

                if (bll.UpdatePost(model))
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

            model = bll.GetPostWithID(model.ID);
            model.Categories = selectList;
            model.isUpdate = true;
            return View(model);

        }

        public JsonResult DeletePostImage(int ID)
        {
            string imagePath = bll.DeletePostImage(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagePath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagePath));
            }
            return Json("");
        }

        public JsonResult DeletePost(int ID)
        {
            List<PostImageDTO> imageList = bll.DeletePost(ID);

            foreach(var item in imageList)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath));
                }
            }

            //we will use triggers to delete tags

            return Json("");
        }



        //methods to get counts
        public JsonResult GetCounts()
        {
            CountDTO countDTO = new CountDTO();
            countDTO = bll.GetCounts();
            return Json(countDTO, JsonRequestBehavior.AllowGet);
        }


    }
}