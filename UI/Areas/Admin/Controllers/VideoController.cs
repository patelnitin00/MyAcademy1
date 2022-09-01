using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBLL bll = new VideoBLL();
        // GET: Admin/Video
        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDTO model)
        {
            /*< iframe width = "560" height = "315" src = "https://www.youtube.com/embed/NmmpXcMxCjY" title = "YouTube video player" frameborder = "0" allow = "accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen ></ iframe >
           https://www.youtube.com/watch?v=NmmpXcMxCjY*/

            

            if (ModelState.IsValid)
            {
                //extracting string after 32 characters
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";

                mergelink += path;

                //model.VideoPath = String.Format(@" < iframe width = ""560"" height = ""315"" src = ""{0}"" frameborder = ""0""  allowfullscreen ></ iframe >",mergelink);
                  model.VideoPath = String.Format(@" <iframe width = ""300"" height = ""200"" src = ""{0}"" frameborder = ""0"" allowfullscreen ></iframe> ", mergelink);

                //now hold the embedded iframe of video in video path

                if (bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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


        public ActionResult VideoList()
        {
            List<VideoDTO> list = new List<VideoDTO>();
            list = bll.GetVideoList();
            return View(list);
        }

        public ActionResult UpdateVideo(int ID)
        {
            VideoDTO video = new VideoDTO();
            video = bll.GetVideoWithID(ID);

            return View(video);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                //extracting string after 32 characters
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";

                mergelink += path;

                model.VideoPath = String.Format(@"< iframe width = ""560"" height = ""315"" src = ""{0}"" frameborder = ""0""  allowfullscreen ></ iframe >", mergelink);

                //now hold the embedded iframe of video in video path
                if (bll.UpdateVideo(model))
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

        public JsonResult DeleteVideo(int ID)
        {
            bll.DeleteVideo(ID);
            return Json("");
        }


    }
}